using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.LCU {
    class EOGData {
        public string gameMode;
        public sBlock statsBlock;
    }

    class sBlock {
        public int gameLengthSeconds;
        public Player[] players;
    }

    class Player {
        public string summonerName;
        public int health;
        public int ffaStanding;
        public long playerId;
    }
}
