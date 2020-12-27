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

            int WaitForStats = 0;

            do {
                state = clientLCU.GetGamePhase();
                Thread.Sleep(1000);

                if (WaitForStats >= 10) {
                    DBG.log("Wait to long for stats");
                    break;
                } else if(state == gameFlowPhase.WaitingForStats) {
                    WaitForStats++;
                    Thread.Sleep(1000);
                }

            } while(state == gameFlowPhase.WaitingForStats);
            
        }
    }
}
