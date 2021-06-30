using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

using LCU.Helper;

namespace LeagueBotV3.AI {
    public class FullGameAi : baseAi {
    
        public FullGameAi() {
            tickEvent += tic;
        }

        private void tic(object sender, AllGameData gameData) {

            if(gameData == null) {
                return;
            }

            if(gameData.activePlayer.championStats.currentHealth <= 0) {
                Windows.bringWindowToFront(Global.GameProc);
                Point p = Windows.lolToScreenSpace(Global.GameProc,Global.ExitButton);
                Windows.MoveMouse(p,true);
                res = stoppReson.Dead;
                Thread.Sleep(1000);
            }
        }
    
    }
}
