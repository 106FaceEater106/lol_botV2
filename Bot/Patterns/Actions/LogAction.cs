using System;
using System.Diagnostics;

namespace LeagueBot.Patterns.Actions {
    class LogAction : PatternAction {

        protected string MSG {
            get;
            private set;
        }

        public LogAction(string log, string description, Double duration = 0) : base(description, duration) {
            MSG = log;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            Console.WriteLine("LOG: " + MSG);
            Debug.WriteLine("LOG: " + MSG);
        }
    }
}
