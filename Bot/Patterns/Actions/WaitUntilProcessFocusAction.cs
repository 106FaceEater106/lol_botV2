using LeagueBot.Windows;
using System;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    public class WaitUntilProcessFocusAction : PatternAction {
        
        private string ProcessName;
        
        public WaitUntilProcessFocusAction(string processName, string description, double duration = 0) : base(description, duration) {
            this.ProcessName = processName;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            while (!Interop.IsProcessFocused(ProcessName) && !isStoped) {
                Console.WriteLine("Wait focus from " + ProcessName + "...");
                Thread.Sleep(2000);
                try {
                    Interop.BringWindowToFront(ProcessName);
                }
                catch {
                }
            }
        }

        public override void Dispose() {
            ProcessName = null;
        }
    }
}
