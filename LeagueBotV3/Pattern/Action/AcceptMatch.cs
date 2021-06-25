using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using LCU;

namespace LeagueBotV3.Pattern.Action {
    class AcceptMatch : ActionBase {
        public override ActionBase Clone() {
            return new AcceptMatch();
        }

        protected override ActionResult _run(Bot bot) {

            gameFlowPhase phase;
            do {
                Thread.Sleep(2000);
                phase = clientLCU.GetGamePhase();

                if(phase == gameFlowPhase.ReadyCheck) {
                    clientLCU.AcceptMatch();
                } else if(phase == gameFlowPhase.Lobby) {
                    clientLCU.StartSearch();
                } else if(phase == gameFlowPhase.InProgress) {
                    return ActionResult.Ok;
                }

            } while(phase == gameFlowPhase.ReadyCheck || phase == gameFlowPhase.Matchmaking || phase == gameFlowPhase.Lobby);

            phase = clientLCU.GetGamePhase();
            return (phase == gameFlowPhase.InProgress) ? ActionResult.Ok : ActionResult.Restart;
        }
    }
}
