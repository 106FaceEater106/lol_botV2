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
            foreach(ActionBase a in actions) {
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

        public void threadStart() {

        }

        public void Start() {
            isRuning = true;
            while(isRuning) {
                if (actionQue.Count == 0) break;

                ActionBase action = actionQue.First.Value;
                actionQue.RemoveFirst();

               ActionResult actionResult = action.run(this);

                DBG.log($"Action: {action.getName()} Res: {actionResult}");

                switch(actionResult) {
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


        #region CLI

        public void addCliCommands() {
            cliManager.addCommand("thread-state",threadStatus,"Get action thread status");
            cliManager.addCommand("logPath",getLogPath,"get path to log file");
            
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
