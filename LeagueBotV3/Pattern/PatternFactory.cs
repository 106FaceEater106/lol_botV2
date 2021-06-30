using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBotV3.Pattern {

    public enum Patterns {
        FullGame
    }

    public static class PatternFactory {
        public static PatternBase get(Patterns p) {
            switch(p) {
                case Patterns.FullGame:
                    return new FullGame();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
