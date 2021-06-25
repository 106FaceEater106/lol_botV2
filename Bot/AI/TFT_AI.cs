using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using LCU;
using LeagueBot.DEBUG;
using LeagueBot.Windows;
using LeagueBot.Patterns;
using LeagueBot.Constants;

namespace LeagueBot.AI {
    public class TFT_AI : baseAI {

        private State state = new State();
        private int maxWait = 50; // sec

        public TFT_AI(Bot bot) : base(bot) {
        }

        private void updateState() {
            state.apiReady = gameLCU.IsApiReady();
            //state.gameOpen = Interop.IsProcessOpen(ps_name);
            state.gameOpen = Interop.ProcessHasWindow(ps_name);

            if(state.apiReady) {
                state.isDead = gameLCU.IsPlayerDead();
            }

            if(state.gameOpen) {
                state.inShop = isInShop();
                state.lastSeenGame = DateTime.Now;
            }

            state.phase = clientLCU.GetGamePhase();

            state.gameTime = (int)(DateTime.Now - state.gameStart).TotalSeconds;
        }

        public override void Execute() {
            DBGV2.log("TFT AI START");

            state.gameStart = DateTime.Now;

            DateTime dt = DateTime.Now;
            dt = dt.AddMinutes(-2);
            updateState();

            do {
                updateState();

                if ((DateTime.Now - state.lastSeenGame).TotalSeconds >= maxWait) {
                    DBGV2.log($"Wait for to long for game {(DateTime.Now - state.lastSeenGame).TotalSeconds}s", MessageLevel.Critical);
                    bot.reset();
                    return;
                } else if(state.phase != gameFlowPhase.InProgress) {
                    this.OnProcessClosed();
                    return;
                } else if (state.phase == gameFlowPhase.WaitingForStats) {
                    DBGV2.log("Game done but failed to close", MessageLevel.Warning);
                    this.OnProcessClosed();
                    return;
                } else if (!state.gameOpen) {
                    this.OnProcessClosed();
                    return;
                } else if (!bot.working) {
                    return;
                }

                if (state.isDead) {
                    exitGame();
                } else if(endGameASAP && state.gameTime > 600) {
                    FF();
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

                    for (int i = 0; i < 10; i++) {
                        LevelUp();
                        Thread.Sleep(75);
                    }

                    dt = DateTime.Now;
                }
                Thread.Sleep(4000);
            } while (state.gameOpen);
        }

        public override void OnProcessClosed() {
            bot.nextPattern = new EndGamePatternTFT(bot);
            base.OnProcessClosed();
        }
    }
}
