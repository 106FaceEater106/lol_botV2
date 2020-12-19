using LeagueBot;
using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Event;
using LeagueBot.Patterns;
using LeagueBot.Patterns.Actions;
using LeagueBot.Windows;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeagueBot.LCU;

namespace LeagueBot.Patterns.Actions {
    public class GetPlacmentTFT : PatternAction {

        public GetPlacmentTFT(string description, Double duration = 0) : base(description, duration) {
        }

        public override void Apply(Bot bot, Pattern pattern = null) {
            EndGameData data = clientLCU.getEndGameData();
            bot.invoke_tft_game_end(this,data);
        }

        public override void Dispose() {
        }
    }
}
