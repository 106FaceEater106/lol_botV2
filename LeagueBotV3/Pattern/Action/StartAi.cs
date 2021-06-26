using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3;
using LeagueBotV3.AI;

namespace LeagueBotV3.Pattern.Action {
    class StartAi : ActionBase {

        public baseAi ai { get; init; }
        
        public override ActionBase Clone() {
            throw new NotImplementedException();
        }

        protected override ActionResult _run(Bot bot) {
            try {
                ai.bot = bot;
                bot.runAi(ai);
            } catch {
                throw new NotImplementedException();
            }

            return ActionResult.Ok;
        }
    }
}
