using LeagueBot.Constants;
using System;
using System.Drawing;

namespace LeagueBot.Patterns.Actions {
    public class ClickAction : PatternAction {
        protected Point Destination {
            get;
            private set;
        }
        protected ClickType Type {
            get;
            private set;
        }
        public ClickAction(ClickType type, Point destination, string description, Double duration = 0) : base(description, duration) {
            this.Destination = destination;
            this.Type = type;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            switch (Type) {
                case ClickType.RIGHT:
                    bot.RightClick(Destination);
                    break;
                case ClickType.LEFT:
                    bot.LeftClick(Destination);
                    break;
            }
        }

        public override void Dispose() {
        }
    }
}
