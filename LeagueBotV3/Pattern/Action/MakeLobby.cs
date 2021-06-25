using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using LCU;

namespace LeagueBotV3.Pattern.Action {
    class MakeLobby : ActionBase {

        public QueTypes que { get; init; }

        public override ActionBase Clone() {
            return new MakeLobby();
        }

        protected override ActionResult _run(Bot bot) {
            gameFlowPhase phase = clientLCU.GetGamePhase();

            if(phase == gameFlowPhase.InProgress) {
                return ActionResult.Ok;
            } else if (phase == gameFlowPhase.Lobby) {
                clientLCU.leavLoby();
                Thread.Sleep(2000);
            }

            clientLCU.CreateLobby(que);
            Thread.Sleep(2000);
            phase = clientLCU.GetGamePhase();
            return (phase == gameFlowPhase.Lobby) ? ActionResult.Ok : ActionResult.Retry;

        }
    }
}
