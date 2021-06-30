using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using LeagueBotV3.Pattern;
using LeagueBotV3.Pattern.Action;
using LeagueBotV3.AI;

namespace LeagueBotV3 {
    public class Bot {

        private LinkedList<ActionBase> actionQue = new();
        private Patterns lastPattern;
        private Thread actionThread;
        private baseAi currentAI;

        public bool isRuning { get; private set; } = false;

        [Obsolete("Need to use a pattern to que so i can restart")]
        public void queueActions(ActionBase[] actions) {
            foreach (ActionBase a in actions) {
                actionQue.AddLast(a);
            }
        }

        public void queueActions(PatternBase pattern) {
            lastPattern = pattern.patternType;
            foreach (ActionBase a in pattern.actions) {
                actionQue.AddLast(a);
            }
        }


        public void runAi(baseAi ai) {
            currentAI = ai;
            try {
                ai.execute(this);
                DBG.log($"AI done: {ai.res}",MessageLevel.Info);
            } catch(Exception err) {
                DBG.log($"{err.GetType()} in {ai.GetType()}", MessageLevel.Critical);
            }
            currentAI = null;
        }

        public Thread threadStart() {
            if (isRuning) {
                DBG.log("trying to start bot 2 times", MessageLevel.Warning);
                return null;
            }
            Thread t = new Thread(Start);
            actionThread = t;
            t.Name = "actionThread";
            t.Start();
            return t;
        }

        public void Start() {

            if (isRuning) {
                DBG.log("trying to start bot 2 times", MessageLevel.Warning);
                return;
            }

            if(actionQue.Count == 0) {
                DBG.log("Start with no patter. using full game",MessageLevel.Warning);
                queueActions(PatternFactory.get(Patterns.FullGame));
            }

            isRuning = true;
            while (isRuning) {
                if (actionQue.Count == 0) break;

                ActionBase action = actionQue.First.Value;
                actionQue.RemoveFirst();

                Task<ActionResult> actionResult = action.runAsync(this);
                DBG.log($"Action: {action.getName()} Res: {actionResult.Result}");
                switch (actionResult.Result) {
                    case ActionResult.Retry:
                        actionQue.AddFirst(action.Clone());
                        break;
                    case ActionResult.Ok:
                        break;

                    case ActionResult.Error:
                        DBG.log($"Action error: {action.GetType()}");
                        break;

                    case ActionResult.Restart:
                        actionQue.Clear();
                        queueActions(PatternFactory.get(lastPattern));
                        break;

                    default:
                        throw new NotImplementedException();
                }

            }
            isRuning = false;
            DBG.log("No more Actions");
        }

        public void stop() {
            isRuning = false;
            if(actionThread != null) {
                if(Thread.CurrentThread.Name == "actionThread") {
                    actionThread.Interrupt();
                }
            }
        }


        #region CLI

        public void addCliCommands() {
            cliManager.addCommand("thread-state", threadStatus, "Get action thread status");
            cliManager.addCommand("logPath", getLogPath, "get path to log file");
            cliManager.addCommand("startBot",threadStart,"Start lol bot in new thread");
            cliManager.addCommand("stopBot",stop,"stop bot asap");
            cliManager.addCommand("lcu-state", LCUstatus,"Get status of the lol client/game api");
            cliManager.addCommand("liveLog", liveLog, "turn live log on or off");
            cliManager.addCommand("initLCU", reInitLCU, "try to init lcu. use initLCU {League of Legends folder placement}");

            cliManager.addCommand("_test",sendToAi);
            cliManager.addCommand("_startAI", testAI);
        }

        public int testAI() {
#if RELEASE
            return 1;
#endif
            isRuning = true;

            baseAi a = new FullGameAi();
            a.bot = this;
            ThreadPool.QueueUserWorkItem((object x) => { runAi(a); });
            

            isRuning = false;
            return 0;
        }

        public int sendToAi(string[] args) {
            if (args.Length > 0 && currentAI != null) {
                currentAI._testCommand(args);
            }
            return 0;
        }

        public void liveLog() {
            DBG.writeToConsole = !DBG.writeToConsole;
            Console.WriteLine($"Live log: {DBG.writeToConsole}");
        }

        public int cliStart(string[] args) {
            threadStart();
            return 0;
        }

        public int reInitLCU(string[] args) {
            if (args.Length == 0) return 1;
            LCU.clientLCU.init(args[0]);
            Thread.Sleep(1000);
            return LCU.clientLCU.IsApiReady() ? 0 : 1;
        }

        public int LCUstatus(string[] args) {
            bool client = LCU.clientLCU.IsApiReady();
            bool game = LCU.gameLCU.IsApiReady();
            
            Console.Write("Client: ");
            if(client) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GOOD!");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BAD!");
            }
            Console.ResetColor();
            Console.Write("Game: ");
            if (game) {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("GOOD!");
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("BAD!");
            }
            Console.ResetColor();
            return 0;
        }

        public int threadStatus(string[] args) {
            if(isRuning) {
                Console.WriteLine($"{actionThread.Name}: {actionThread.IsAlive}");
            } else {
                Console.WriteLine("No action thread");
            }
            return 0;
        }

        public int getLogPath(string[] args) {
            Console.WriteLine($"Log path: {DBG.logFile}");
            return 0;
        }

        #endregion
        //no code under cli

    }
}
