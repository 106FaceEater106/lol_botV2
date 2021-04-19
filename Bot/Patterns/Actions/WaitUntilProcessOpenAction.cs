using LeagueBot.Windows;
using System;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    public class WaitUntilProcessOpenAction : PatternAction {
        
        private string ProcessName;
        private Action TimeoutCallback;
        private int Timeout;

        public WaitUntilProcessOpenAction(string processName, string description, int timeout, Action timeoutCallback, double duration = 0) : base(description, duration) {
            this.ProcessName = processName;
            this.TimeoutCallback = timeoutCallback;
            this.Timeout = timeout;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            int x = 0;
            while (!Interop.IsProcessOpen(ProcessName) && !isStoped) {
                Console.WriteLine("Wait for " + ProcessName + "...");
                Thread.Sleep(1000);
                x++;
                if (x == Timeout) {
                    TimeoutCallback();
                    break;
                }
            }
        }

        public override void Dispose() {
            TimeoutCallback = null;
        }
    }
}
