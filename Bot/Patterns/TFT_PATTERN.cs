using LeagueBot.AI;
using LeagueBot.Constants;

namespace LeagueBot.Patterns {
    public class TFT_PATTERN : TFT_MapPattern {
        public TFT_PATTERN(Bot bot) : base(bot) {
        }

        public override AbstractAI AI => new AI_TFT(Bot, this);
    }
}
