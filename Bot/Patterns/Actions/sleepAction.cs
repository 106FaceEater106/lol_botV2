using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    class SleepAction : PatternAction {

        private int t;

        public SleepAction(int time) : base($"Sleep for {time}ms") {
            t = time;
        }
        
        public override void Apply(Bot bot, Pattern pattern) {
            Thread.Sleep(t);
        }
    }
}
