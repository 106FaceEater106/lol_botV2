using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3.Pattern;

namespace LeagueBotV3.Pattern.Action {
    public class QueuePattern : ActionBase {
        public Patterns pattern { get; init; }

        public override ActionBase Clone() {
            throw new NotImplementedException();
        }

        protected override ActionResult _run(Bot bot) {
            bot.queueActions(PatternFactory.get(pattern));
            return ActionResult.Ok;
        }
    }
}
