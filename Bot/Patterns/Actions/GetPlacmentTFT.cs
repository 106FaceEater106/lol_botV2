using LeagueBot;
using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Event;
using LeagueBot.Patterns;
using LeagueBot.Patterns.Actions;
using LeagueBot.Windows;
using System;

namespace LeagueBot.Patterns.Actions {
    class GetPlacmentTFT : PatternAction {

        public GetPlacmentTFT(string description, Double duration = 0) : base(description, duration) {
        }

        public override void Apply(Bot bot, Pattern pattern) {

            for (int i = 0; i < 8; i++) {
                pattern.BringProcessToFront();
                pattern.CenterProcessMainWindow();
                var px = Interop.GetPixelColor(PixelsConstants.CHECK_PLACE_TFT[i]);

                if (px.ToArgb() == ColorConstants.CHECK_PLACE_TFT.ToArgb()) {

                    EndGameData data = new EndGameData();
                    data.place = i;
                    data.sender = "TFT_PATTERN";
                    bot.invoke_tft_game_end(this, data);

                    return;
                } else if (i == 7) {
                    DBG.log("GOT ? PLACE IN TFT GAME", DateTime.Now, "TFT_PATTERN");
                }
            }
        }

        public override void Dispose() {
        }
    }
}
