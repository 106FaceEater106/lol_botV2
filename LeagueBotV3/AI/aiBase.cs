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

    public enum stoppReson {
        Dead,
        FF,
        BotStopp,
        Safety,

        Unknown
    }

    public abstract class baseAi {

        protected event EventHandler<AllGameData> tickEvent;
        protected double deltaTime { get { return DateTime.Now.Subtract(lastTick).TotalMilliseconds; } }
        protected DateTime lastTick = DateTime.Now;
        protected DateTime aiStart;

        public aiState state = aiState.Loading;
        public Bot bot;
        private int failCount = 0;
        private int maxFailes = 10;
        public stoppReson res { get; protected set; } = stoppReson.Unknown;

        private double secSinceStart {
            get {
                return DateTime.Now.Subtract(aiStart).TotalSeconds;
            }
        }

        protected void ff() {
            throw new NotImplementedException();
        }

        public void execute(Bot bot) {
            aiStart = DateTime.Now;
            while(tic());
            DBG.logIfDbg($"AI done: {res}({secSinceStart}s)");
        }

        protected bool isInShop() {
            Windows.bringWindowToFront(Global.GameProc);
            return true;
        }

        private bool tic() {
            AllGameData gameData = null;
            bool hasWin = Windows.hasWindow(Global.GameProc);
            // wait 30 sec for game window
            if (!bot.isRuning) {
                res = stoppReson.BotStopp;
                return false;
            }
            if (!hasWin && secSinceStart > 30) {
                res = stoppReson.Safety;
                return false;
            } else if (!hasWin) {
                return true;
            }

            try {
                gameData = gameLCU.GetAllGameData();
                if (gameData?.gameData.gameTime > 10 && state == aiState.Loading) {
                    DBG.log("Loading done!");
                    state = aiState.InGame;
                }
            } catch {
                if (secSinceStart > 500) {
                    failCount++;
                }
            }

            if(deltaTime > 2000) {
                DBG.log($"Tic is slow({deltaTime}ms)",MessageLevel.Warning);
            }

            if(failCount < maxFailes) {
                tickEvent?.Invoke(this, gameData);
                lastTick = DateTime.Now;
                return true;
            } else {
                res = stoppReson.Safety;
                return false;
            }
        }

        public virtual bool _testCommand(string[] args) {
            if(args[0] == "E") {
                Windows.MoveMouse(Global.ExitButton,true);
            }
            return true;
        }
    }
}
