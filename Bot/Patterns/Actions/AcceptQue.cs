using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Windows;
using System;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    class AcceptQue : PatternAction {

        public AvailableGameType mode;

        public AcceptQue(AvailableGameType mode, string description) : base(description, 0) {
            this.mode = mode;
        }

        public override void Apply(Bot bot, Pattern pattern) {

            bool que_done = false;
            DateTime start = DateTime.Now;

            while (!que_done) {
                pattern.BringProcessToFront();
                pattern.CenterProcessMainWindow();
                var px = Interop.GetPixelColor(PixelsConstants.ACCEPT_MATCH_BUTTON);

                double que_time_sec = DateTime.Now.Subtract(start).TotalSeconds;

                if (que_time_sec >= 500) {
                    bot.Abort("To long que time");
                    return;
                }

                if (px == ColorConstants.ACC_BUTTON && DBG.Accept) {
                    bot.LeftClick(PixelsConstants.ACCEPT_MATCH_BUTTON);
                    bot.LeftClick(PixelsConstants.PLAY_BUTTON, true);
                    Thread.Sleep(1000);

                } else {
                    Thread.Sleep(1000);
                }

                que_done = Interop.IsProcessOpen(LeagueConstants.LoL_GAME_PROCESS);

            }
        }

        public override void Dispose() {
        }
    }
}
