using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBotV3.Pattern {

    public enum Patterns {
        startGame
    }

    public static class PatternFactory {
        public static PatternBase get(Patterns p) {
            switch(p) {
                case Patterns.startGame:
                    return new StartGame();

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
