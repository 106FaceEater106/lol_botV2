using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCU.Champs {
    public class Champion {

        public int iKey {
            get {
                return Int32.Parse(key);
            }
        }

        public string key;
        public string id;
        public string title;

        //public Champion(string key, int id) {
        //    this.key = key;
        //    this.id = id;
        //}


        public override string ToString() {
            return $"{key}: {id} {title}";
        }
    }
}
