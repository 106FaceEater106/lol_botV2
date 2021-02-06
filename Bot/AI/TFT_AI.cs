using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using LeagueBot.LCU;
using LeagueBot.DEBUG;
using LeagueBot.Windows;
using LeagueBot.Patterns;
using LeagueBot.Constants;

namespace LeagueBot.AI {
    class TFT_AI : baseAI {

        State state;

        public TFT_AI(Bot bot) : base(bot) {

        }

        private void updateState() {
            state.apiReady = gameLCU.IsApiReady();
            state.gameOpen = Interop.IsProcessOpen(ps_name);

            if(state.apiReady) {
                state.isDead = gameLCU.IsPlayerDead();
            }

            if(state.gameOpen) {
                state.inShop = isInShop();
            }

            state.phase = clientLCU.GetGamePhase();
        }

        public override void Execute() {
            DBG.log("TFT AI START");

            DateTime dt = DateTime.Now;
            dt = dt.AddMinutes(-2);
            updateState();

            while (state.gameOpen) {

                updateState();

                if(state.phase == gameFlowPhase.WaitingForStats) {
                    DBG.log("Game done but failed to close", MessageLevel.Warning);
                    this.OnProcessClosed();
                    return;
                } else if (!state.gameOpen) {
                    DBG.log("Game done");
                    this.OnProcessClosed();
                    return;
                } else if(!bot.working) {
                    return;
                }

                if(state.isDead) {
                    exitGame();
                }


                if (DateTime.Now.Subtract(dt).TotalSeconds > 120 && state.inShop) {
                    buyUnit(0);
                    Thread.Sleep(75);
                    buyUnit(1);
                    Thread.Sleep(75);
                    buyUnit(2);
                    Thread.Sleep(75);
                    buyUnit(3);
                    Thread.Sleep(75);
                    buyUnit(4);
                    Thread.Sleep(75);

                    for(int i = 0; i < 10; i++) {
                        LevelUp();
                    }

                    dt = DateTime.Now;
                } 
                Thread.Sleep(4000);
            }
        }

        public override void OnProcessClosed() {
            bot.nextPattern = new EndGamePatternTFT(bot);
            base.OnProcessClosed();
        }
    }
}
