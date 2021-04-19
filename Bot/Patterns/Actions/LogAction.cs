using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;

namespace LeagueBot.Patterns.Actions {
    class LogAction : PatternAction {

        protected string MSG;

        public LogAction(string log, string description, Double duration = 0) : base(description, duration) {
            MSG = log;
            needWindowHelp = false;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            Console.WriteLine("LOG: " + MSG);
            Debug.WriteLine("LOG: " + MSG);
        }

        public override void Dispose() {
        }
    }
}
