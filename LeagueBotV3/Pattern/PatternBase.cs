using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBotV3.Pattern.Action;

namespace LeagueBotV3.Pattern {
    public abstract class PatternBase {

        public Patterns patternType { get; protected set; }


        public ActionBase[] actions;
    }
}
