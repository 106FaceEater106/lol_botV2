using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using LCU;

namespace LCU.Helper {

    public class Session {

        //public long id = clientLCU.accID; 
        public bool valid = true;
        public Action[][] actions;
        public int localPlayerCellId;
        public int lockedEventIndex;

        public Action getCurrent(bool justMe) {
            for(int i = 0; i < actions.Length; i++) {
                for(int j = 0; j < actions[i].Length; j++) {
                    if(actions[i][j].isInProgress) {
                        if(actions[i][j].actorCellId == localPlayerCellId || !justMe) {
                            return actions[i][j];
                        }
                    }
                }
            }
            return null;
        }
    }
}
