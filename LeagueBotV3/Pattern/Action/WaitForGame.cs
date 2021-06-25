using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LCU;

namespace LeagueBotV3.Pattern.Action {
    class WaitForGame : ActionBase {

        //max time to wait for game window
        int maxWait { get; init; } = 120;
        
        public override ActionBase Clone() {
            return new WaitForGame();
        }

        protected override ActionResult _run(Bot bot) {
            DateTime start = DateTime.Now;

            while(DateTime.Now.Subtract(start).TotalSeconds <= maxWait) {
                if(Windows.hasWindow(Global.GameProc)) {
                    return ActionResult.Ok;
                } else if(clientLCU.GetGamePhase() != gameFlowPhase.InProgress) {
                    return ActionResult.Restart;
                }
            }

            return ActionResult.TimeOut;

        }
    }
}
