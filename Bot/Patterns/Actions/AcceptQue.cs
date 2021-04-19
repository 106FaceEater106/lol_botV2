using LeagueBot.Constants;
using LeagueBot.DEBUG;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using LCU;

namespace LeagueBot.Patterns.Actions {
    class AcceptQue : PatternAction {

        public AcceptQue() : base("Waiting for match") {
            needWindowHelp = false;
        }

        public override void Apply(Bot bot, Pattern pattern) {

            DateTime start = DateTime.Now;
            gameFlowPhase state;

            do {
                state = clientLCU.GetGamePhase();

                if (state == gameFlowPhase.ReadyCheck && !DBG.AcceptQue) {
                    return;
                } else if (state == gameFlowPhase.ReadyCheck) {
                    clientLCU.AcceptMatch();
                    Thread.Sleep(5000);
                } else if(state == gameFlowPhase.Lobby) {
                    DBG.log("Que faild to start, trying to restart que",MessageLevel.Warning);
                    clientLCU.StartSearch();
                    Thread.Sleep(2000);
                } else {
                    Thread.Sleep(500);
                }
            } while(state != gameFlowPhase.InProgress && !isStoped);
            
            DBG.log($"Was in que {(DateTime.Now.Subtract(start)).TotalSeconds}s");
        }

        public override void Dispose() {
        }
    }
}
