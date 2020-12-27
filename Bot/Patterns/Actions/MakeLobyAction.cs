using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBot.LCU;

namespace LeagueBot.Patterns.Actions {
    class MakeLobyAction : PatternAction {


        public MakeLobyAction() : base("Making tft loby") {
            needWindowHelp = false;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            LCU.clientLCU.CreateLobby(QueTypes.NormalTFT);
        }
    }
}
