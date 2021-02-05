using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Windows;
using System;
using System.Threading;

using LeagueBot.LCU;

namespace LeagueBot.Patterns.Actions {
    class WaitForReconnect : PatternAction {
        public WaitForReconnect(string description) : base(description, 0) {
        }

        public override void Apply(Bot bot, Pattern pattern) {
            gameFlowPhase state;
            DateTime start = DateTime.Now;

            do {
                state = clientLCU.GetGamePhase();
                if((DateTime.Now-start).TotalSeconds > 60*5) {
                    bot.Abort("Wait to long for stats",MessageLevel.Critical);
                    break;
                }

            } while(state == gameFlowPhase.WaitingForStats);
            DBG.log($"Waited {(DateTime.Now - start).TotalSeconds}s for stats", MessageLevel.Info);
        }
    }
}
