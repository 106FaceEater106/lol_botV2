using System;

namespace LeagueBot.Patterns.Actions {
    class SleepAction : PatternAction {
        public SleepAction(string description, Double duration = 0) : base(description, duration) {
        }

        public override void Apply(Bot bot, Pattern pattern) {
        }

    }
}
