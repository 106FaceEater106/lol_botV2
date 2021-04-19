using LeagueBot.Constants;
using LeagueBot.Windows;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    class AcceptStuff : PatternAction {
        public AcceptStuff(string description, double duration = 0) : base(description, duration) {
        }

        public override void Apply(Bot bot, Pattern pattern) {

            bool loop = true;

            do {

                pattern.CenterWindow();
                var px = Interop.GetPixelColor(PixelsConstants.ACCEPT_LOOT);
                if (px == ColorConstants.ACCEPT_LOOT) {
                    bot.LeftClick(PixelsConstants.ACCEPT_LOOT);
                    Thread.Sleep(2000);
                } else {
                    loop = false;
                }

            } while (loop && !isStoped);

        }
    }
}
