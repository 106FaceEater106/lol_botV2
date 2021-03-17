using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.Helper {
    public class Action {

        public int id;
        public int actorCellId;
        
        public string type;

        public bool completed;
        public bool isInProgress;
        public bool isAllyAction;
        
        public int championId;

        public override string ToString() {
            return $"user witch id {actorCellId} need to {type} Done: {completed} ongoing: {isInProgress}";
        }
    }
}
