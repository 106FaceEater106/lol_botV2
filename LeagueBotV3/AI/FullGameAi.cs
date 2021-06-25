using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBotV3.AI {
    public class FullGameAi : baseAi {
        public override void execute(Bot bot) {
            base.start();

            bool ticRes;
            do {
                ticRes = base.tic();
                if(!ticRes) {
                    base.onClose();
                    return;
                }


                if(state == aiState.InGame) {
                    DBG.log(gameData.activePlayer.ToString());
                }

            } while(ticRes);

        }
    }
}
