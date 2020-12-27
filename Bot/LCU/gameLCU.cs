using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using Newtonsoft.Json;
using Leaf.xNet;

using Bot;
using LeagueBot.Constants;

using LeagueBot.Event;
using LeagueBot.DEBUG;
using System.Reflection;

namespace LeagueBot.LCU {
    public static class gameLCU {


        private static string urlBase = @"https://127.0.0.1:" + 2999 + "/liveclientdata/";

        private static string activePlayerUrl => urlBase + "activeplayer";
        private static string gameStatsUrl => urlBase + "gamestats";

        public static bool IsApiReady() {
            HttpResponse res;
            try {
                HttpRequest req = clientLCU.CreateRequest();
                res = req.Get(gameStatsUrl);
            } catch {
                return false;
            }
            return res.StatusCode == HttpStatusCode.OK;
        }

        public static bool IsPlayerDead() {
            return GetStats().currentHealth == 0;
        }

        public static dynamic GetStats() {
            using(HttpRequest req = clientLCU.CreateRequest()) {
                HttpResponse res = req.Get(activePlayerUrl);
                dynamic dyn = JsonConvert.DeserializeObject(res.ToString());
                return dyn.championStats;
            }
        }
    }
}
