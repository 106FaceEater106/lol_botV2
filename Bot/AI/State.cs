using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LCU;

namespace LeagueBot.AI {
    public class State {
        public int hp { get; set; }
        public int gameTime;
        public DateTime lastAction;
        public gameFlowPhase phase;

        public bool isDead;
        public bool inShop;
        public bool apiReady;
        public bool gameOpen;
    }
}
