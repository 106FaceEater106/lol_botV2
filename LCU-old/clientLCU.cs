using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Reflection;

using Newtonsoft.Json;
using Leaf.xNet;

//using LCU.Event;
using LCU.Helper;


namespace LCU {
    public class clientLCU {

        private static string lolPath = string.Empty;

        public static long accID;

        private static string auth;

        private static int Port;
        private static string urlBase => "https://127.0.0.1:" + Port + "/";

        #region API_PATHS

        private static string CreateLobbyUrl => urlBase + "lol-lobby/v2/lobby";
        private static string AcceptUrl => urlBase + "lol-matchmaking/v1/ready-check/accept";
        private static string GamePhaseUrl => urlBase + "lol-gameflow/v1/gameflow-phase";
        private static string GameflowAvailabilityUrl => urlBase + "lol-gameflow/v1/availability";
        private static string SearchURL => urlBase + "lol-lobby/v2/lobby/matchmaking/search";
        private static string getEndGameDataUrl => urlBase + "lol-end-of-game/v1/gameclient-eog-stats-block";
        private static string skitWaitForStatsUrl => urlBase + "lol-end-of-game/v1/state/dismiss-stats";
        private static string getSessionUrl => urlBase + "lol-login/v1/session";
        private static string getChampSelectSessionUrl => urlBase + "lol-champ-select/v1/session";
        private static string patchActionUrl => urlBase + "lol-champ-select/v1/session/actions/";
        private static string getBannableChampsUrl => urlBase + "lol-champ-select/v1/bannable-champion-ids";

        #endregion

        #region SETUP

        public static bool init(bool loadChamps = false) {
            if(lolPath == string.Empty) {
                throw new Exception("Cant init with no lol path set");
            }

            return init(lolPath);
        }

        public static bool init(string LOLPath) {
            string path = Path.Combine(LOLPath, @"League of Legends\lockfile");
            lolPath = path;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                using (var streamReader = new StreamReader(fileStream, Encoding.Default)) {
                    string line;
                    while ((line = streamReader.ReadLine()) != null) {
                        string[] lines = line.Split(':');
                        Port = int.Parse(lines[2]);
                        string riot_pass = lines[3];
                        auth = Convert.ToBase64String(Encoding.UTF8.GetBytes("riot:" + riot_pass));
                    }
                }
            }


            if (Port == 0) {
                //DBG.log("Unable to initialize ClientLCU.cs (unable to read API port from process)", MessageLevel.Critical);
                Console.WriteLine($"Critical : Unable to initialize ClientLCU.cs (unable to read API port from process)");
                return false;
            }
            
            if(!IsApiReady()) {
                //DBG.log("Unable to initialize ClientLCU.cs (unable to get data from API)", MessageLevel.Critical);
                Console.WriteLine($"Critical : Unable to initialize ClientLCU.cs (unable to read API port from process)");
                return false;
            } else {
                accID = getAccountId();
            }
            return true;
        }

        public static bool IsApiReady() {
            using (HttpRequest request = CreateRequest()) {
                try {
                    var response = request.Get(GameflowAvailabilityUrl);

                    if (response.StatusCode == HttpStatusCode.OK) {
                        dynamic obj = JsonConvert.DeserializeObject(response.ToString());
                        bool ready = obj.isAvailable;
                        return ready;
                    }
                } catch {
                    return false;
                }
            }

            return false;
        }
        #endregion

        #region Champ select

        

    public static void patchAction(ActionPatch patch) {
            string url = patchActionUrl + patch.id;
            string body = JsonConvert.SerializeObject(patch);

            using (HttpRequest req = CreateRequest()) {
                req.Patch(url, body, "application/json");
            }
        
        }
        #endregion
        /// <summary>
        /// this use lol data dragon not client api
        /// </summary>
        /// <returns> string </returns>
        public static string GetVersion() {
            HttpRequest request = new HttpRequest();
            HttpResponse res = request.Get(@"https://ddragon.leagueoflegends.com/api/versions.json");
            
            if(res.StatusCode != HttpStatusCode.OK) {
                throw new Exception("Faild to load version list");
            }

            string[] verList = JsonConvert.DeserializeObject<string[]>(res.ToString());

            return verList[0];
        }

        #region GAME START
        public static bool CreateLobby(QueTypes queueId) {
            using (var request = CreateRequest()) {
                string response = request.Post(CreateLobbyUrl, $"{{\"queueId\": {(int)queueId} }}", "application/json").StatusCode.ToString();

                if (response == "OK") {
                    return true;
                } else {
                    return false;
                }
            }
        }


        public static gameFlowPhase GetGamePhase() {
            try {
                using (var request = CreateRequest()) {
                    var result = request.Get(GamePhaseUrl).ToString();
                    result = Regex.Match(result, "\"(.*)\"").Groups[1].Value;
                    return (gameFlowPhase)Enum.Parse(typeof(gameFlowPhase), result);
                }
            } catch (Exception err) {
                return gameFlowPhase.NoClient;
            }
        }

        public static void AcceptMatch() {
            using (HttpRequest req = CreateRequest()) {
                req.Post(AcceptUrl);
            }
        }

        public static bool StartSearch() {
            using (var request = CreateRequest()) {
                string response = request.Post(SearchURL).ToString();

                if (response == string.Empty) {
                    return true;
                } else {
                    //DBG.log("Failt to start search", MessageLevel.Critical, "clientLCU");
                    return false;
                }
            }
        }

        #endregion

        public static long getAccountId() {
            using (HttpRequest req = CreateRequest()) {
                HttpResponse res = req.Get(getSessionUrl);
                if (res.StatusCode == HttpStatusCode.OK) {
                    dynamic obj = JsonConvert.DeserializeObject(res.ToString());
                    return obj.accountId;
                }
                return -99;
            }
        }

        public static int[] getBannableChamps() {
            using(HttpRequest req = CreateRequest()) {
                HttpResponse res = req.Get(getBannableChampsUrl);
                string result = Regex.Match(res.ToString(), @"\[(.*)\]").Groups[1].Value;
                return result.Split(',').Select(Int32.Parse).ToArray();
            }
        }

        private static T getDeserializData<T>(string url) {
            try {
                using (HttpRequest req = CreateRequest()) {
                    HttpResponse res = req.Get(getChampSelectSessionUrl);
                    if (res.StatusCode == HttpStatusCode.OK) {
                        return JsonConvert.DeserializeObject<T>(res.ToString());
                    } else {
                        return default(T);
                    }
                }
            } catch(HttpException) {
                return default(T);
            }
        }

        public static Session getChampSelectSession() {
            return getDeserializData<Session>(getChampSelectSessionUrl);
        }

        public static EndGameData getEndGameData() {
            EndGameData o = new EndGameData();
            using(HttpRequest req = CreateRequest()) {
                HttpResponse res = req.Get(getEndGameDataUrl);
                if (res.StatusCode == HttpStatusCode.OK) {
                    EOGData obj = JsonConvert.DeserializeObject<EOGData>(res.ToString());

                    int place = -99;

                    foreach(Player p in obj.statsBlock.players) {
                        if(p.playerId == accID) {
                            place = p.ffaStanding;
                        }
                    }

                    o.place = place;
                    o.GameLength = obj.statsBlock.gameLengthSeconds;
                }
            }
            return o;
        }
        
        public static void skipWaitForStats() {
            using (HttpRequest req = CreateRequest()) {
                req.Post(skitWaitForStatsUrl);
            }
        }

        public static HttpRequest CreateRequest() {
            HttpRequest request = new HttpRequest();
            request.IgnoreProtocolErrors = true;
            request.ConnectTimeout = 10 * 1000;
            request.ReadWriteTimeout = 10 * 1000;
            request.CharacterSet = Encoding.UTF8;
            request.AddHeader("Authorization", "Basic " + auth);
            request.AcceptEncoding = "application/json";
            return request;
        }
    }
}
