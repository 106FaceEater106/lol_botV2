using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LCU;

namespace LeagueBot.AI {
    public class State {
        public int hp { get; set; }
        public gameFlowPhase phase;

        public bool isDead;
        public bool inShop;
        public bool apiReady;
        public bool gameOpen;

        public int gameTime = 0;
        public DateTime gameStart;
        public DateTime lastAction;
        public DateTime lastSeenGame = DateTime.Now;
    }
}
