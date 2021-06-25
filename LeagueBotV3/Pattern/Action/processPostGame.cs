using System;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LCU;
using LCU.Helper;

namespace LeagueBotV3.Pattern.Action {
    public class processPostGame : ActionBase {
        public override ActionBase Clone() {
            return new processPostGame();
        }

        protected override ActionResult _run(Bot bot) {

            DateTime start = DateTime.Now;
            while(DateTime.Now.Subtract(start).TotalSeconds < 120) {
                gameFlowPhase phase = clientLCU.GetGamePhase();

                switch(phase) {

                    case gameFlowPhase.EndOfGame:
                        EndGameData data = clientLCU.getEndGameData();
                        DBG.log($"Got {data.place}");
                        return ActionResult.Restart;

                    case gameFlowPhase.WaitingForStats:
                        if(DateTime.Now.Subtract(start).TotalSeconds >= 60) {
                            clientLCU.skipWaitForStats();
                            DBG.log("Wait for stats for to long");
                        }
                        break;

                    case gameFlowPhase.InProgress:
                        if (DateTime.Now.Subtract(start).TotalSeconds >= 60) {
                            return ActionResult.Error;
                        }
                        break;

                    default:
                        DBG.log($"no post action for {phase}!!!", MessageLevel.Warning);
                        break;
                }
            }
            return ActionResult.Restart;
        }
    }
}
