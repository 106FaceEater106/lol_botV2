using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3;

namespace LeagueBotV3.Pattern.Action {

    public enum ActionResult {
        Ok,
        Retry,
        Restart,
        Error,
        Stoped,
        TimeOut
    }

    public abstract class ActionBase {
        
        public ActionResult run(Bot bot) => _run(bot);
        public Task<ActionResult> runAsync(Bot bot) {
            Task<ActionResult> t = new Task<ActionResult>(() => {
                return _run(bot);
            });
            t.Start();
            return t;
        }

        public virtual bool canRun() {
            return true;
        }

        protected abstract ActionResult _run(Bot bot);
        public abstract ActionBase Clone();

        public string getName() {
            return this.GetType().ToString().Split(".").Last();
        }
    }
}
