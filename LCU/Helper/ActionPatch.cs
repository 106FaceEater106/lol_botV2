using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.Helper {
    public class ActionPatch {

        public ActionPatch(int Id, string Type, int actor) {
            id = Id;
            type = Type;
            actorCellId = actor;
        }

        public ActionPatch(Action a) {
            id = a.id;
            type = a.type;
            actorCellId = a.actorCellId;
        }

        public int id;
        public string type;
        public int championId;
        public int actorCellId = 0;
        public bool completed = true;
        public bool isAllyAction = true;
    
    }
}
