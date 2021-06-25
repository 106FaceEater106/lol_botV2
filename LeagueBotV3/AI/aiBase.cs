using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3;

using LCU;
using LCU.Helper;

namespace LeagueBotV3.AI {

    public enum aiState {
        Loading,
        InGame,

    }

    public abstract class baseAi {

        public AllGameData gameData;
        public DateTime aiStart;
        public aiState state = aiState.Loading;


        private int failCount = 0;
        private int maxFailes = 10;

        private double secSinceStart { 
            get {
                return DateTime.Now.Subtract(aiStart).TotalSeconds;
            }
        }

        abstract public void execute(Bot bot);

        protected virtual void start() {
            aiStart = DateTime.Now;
            DBG.log("AI START");
        }

        protected virtual void onClose() {
            DBG.log("AI DONE");
        }

        //return false if the ai need to stop
        protected bool tic() {
            bool hasWin = Windows.hasWindow(Global.GameProc);
            // wait 30 sec for game window
            if(!hasWin && secSinceStart > 30) {
                return false;
            } else if(!hasWin) {
                return true; 
            }

            try {
                gameData = gameLCU.GetAllGameData();
                if(gameData?.gameData.gameTime > 10 && state == aiState.Loading) {
                    DBG.log("Loading done!");
                    state = aiState.InGame;
                }
            } catch {
                if(secSinceStart > 500) {
                    failCount++;
                }
            }

            return failCount < maxFailes;
        }
    }
}
