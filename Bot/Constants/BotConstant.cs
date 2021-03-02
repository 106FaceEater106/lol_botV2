using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeagueBot.Constants {
    public static class BotConst {
        
        [Obsolete]
        public const int HttpRequestTimeout = 10 * 1000;
        [Obsolete]
        public static Encoding HttpRequestEncoding = Encoding.UTF8;

        public static string summonerName;
        public static long accountId;

        public const string LoL_LAUNCHER_PROCESS = "LeagueClientUx";
        public const string LoL_GAME_PROCESS = "League of Legends";

        public const int gameApiPort = 2999;

    }
}
