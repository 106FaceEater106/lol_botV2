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

        public static int maxFails = 10;

        public AcceptQue() : base("Waiting for match") {
            needWindowHelp = false;
        }

        public override void Apply(Bot bot, Pattern pattern) {

            DateTime start = DateTime.Now;
            gameFlowPhase state;
            int faileCount = 0;

            Thread.Sleep(2000);

            do {
                state = clientLCU.GetGamePhase();

                if (state == gameFlowPhase.ReadyCheck && !DBG.AcceptQue) {
                    return;
                } else if (state == gameFlowPhase.ReadyCheck) {
                    clientLCU.AcceptMatch();
                    Thread.Sleep(5000);
                } else if(state == gameFlowPhase.Lobby) {

                    if(faileCount >= maxFails) {
                        clientLCU.leavLoby();
                        bot.needRestart = true;
                        bot.stop();
                        return;
                    }

                    faileCount++;
                    DBGV2.log("Que faild to start, trying to restart que",MessageLevel.Warning);
                    clientLCU.StartSearch();
                    Thread.Sleep(5000);
                } else if(state == gameFlowPhase.None) {
                    DBGV2.log("not in loby restarting", MessageLevel.Warning);
                    bot.reset();
                    return;
                }
            } while(state != gameFlowPhase.InProgress && !isStoped);
            
            DBGV2.log($"Was in que {(DateTime.Now.Subtract(start)).TotalSeconds}s");
        }

        public override void Dispose() {
        }
    }
}
