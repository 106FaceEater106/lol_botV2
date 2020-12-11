using System;

using LeagueBot;
using LeagueBot.Patterns;

namespace test_app {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("<Start>");

            Bot b = new Bot();
            b.Start(LeagueBot.Constants.AvailableGameType.TEST);
            
        }
    }
}
