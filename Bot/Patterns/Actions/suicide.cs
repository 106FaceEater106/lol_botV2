using System;
using System.Threading;
using LeagueBot.Patterns;
using LeagueBot.Patterns.Actions;

namespace Bot.Patterns.Actions {
    class suicide : PatternAction {

        private bool kill;

        public suicide(bool klllThread = true) : base("Killing bot", 0) {
            kill = klllThread;
        }

        public override void Apply(LeagueBot.Bot bot, Pattern pattern) {
            bot.stop("suicide actions",LeagueBot.DEBUG.MessageLevel.Info);
            if(kill) {
                Thread.CurrentThread.Abort();
            }
        }
    }
}
