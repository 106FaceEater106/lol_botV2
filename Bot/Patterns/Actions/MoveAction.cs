using LeagueBot.Constants;
using System.Drawing;

namespace LeagueBot.Patterns.Actions {
    public class MoveAction : ClickAction {
        public MoveAction(Point destination, string description, int duration = 0) : base(ClickType.RIGHT, destination, description, duration) {

        }
    }
}
