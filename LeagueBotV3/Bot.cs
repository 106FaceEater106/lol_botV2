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
            ai.execute(this);
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
                queueActions(PatternFactory.get(Patterns.startGame));
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
        }

        public void liveLog() {
            DBG.writeToConsole = !DBG.writeToConsole;
            Console.WriteLine($"Live log: {DBG.writeToConsole}");
        }

        public int cliStart(string[] args) {
            threadStart();
            return 0;
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
