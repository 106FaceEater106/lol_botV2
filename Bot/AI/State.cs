﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.AI {
    struct State {
        public int hp;
        public int gameTime;
        public DateTime lastAction;

        public bool isDead;
        public bool inShop;
        public bool apiReady;
        public bool gameOpen;
    }
}
