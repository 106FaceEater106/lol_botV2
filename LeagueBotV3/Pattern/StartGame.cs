using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3.AI;

using LeagueBotV3.Pattern.Action;

namespace LeagueBotV3.Pattern {
    class StartGame : PatternBase {
        public StartGame() {
            patternType = Patterns.startGame;
            actions = new ActionBase[] {
                new MakeLobby { que = LCU.QueTypes.NormalTFT },
                new AcceptMatch(),
                new WaitForGame(),
                new StartAi { ai = new FullGameAi() },
                new processPostGame()

            };
        }

    }
}
