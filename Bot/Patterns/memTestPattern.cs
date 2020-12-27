using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LeagueBot.Patterns.Actions;

namespace LeagueBot.Patterns {
    public class memTestPattern : Pattern {
        public memTestPattern(Bot bot) : base(bot) {
        }

        public override PatternAction[] Actions => new PatternAction[]
        {
            new LogAction("Test","LOG"),
            new DefinePatternAction(new memTestPattern(bot),"",0),
        };
    }
}
