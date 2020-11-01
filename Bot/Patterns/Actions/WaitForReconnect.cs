using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Windows;
using System;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    class WaitForReconnect : PatternAction {
        public WaitForReconnect(string description) : base(description, 0) {
        }

        public override void Apply(Bot bot, Pattern pattern) {

            bool stuck = false;
            bool first = true;
            DateTime stuck_start = DateTime.Now;

            do {
                var px = Interop.GetPixelColor(PixelsConstants.STUCK_ON_RECONECT);
                if (px == ColorConstants.STUCK_ON_RECONECT && first) {
                    Console.WriteLine("LOG: Stuck on reconnect");

                    first = false;
                    stuck = true;
                } else if (px == ColorConstants.STUCK_ON_RECONECT || DBG.NoStuckDust) {
                    Thread.Sleep(5000);
                }
            } while (stuck);
            Console.Write("LOG: Was stuck for: ");
            Console.Write(DateTime.Now.Subtract(stuck_start).TotalSeconds);
            Console.WriteLine("S");
        }
    }
}
