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
using Bot.Constants;
using LeagueBot.DEBUG;

namespace LeagueBot.LCU {
    public class clientLCU {

        private static string auth;

        private static int Port;
        private static string urlBase => "https://127.0.0.1:" + Port + "/";
        
        #region API_PATHS
        
        private static string CreateLobbyUrl => urlBase + "lol-lobby/v2/lobby";
        private static string AcceptUrl => urlBase + "lol-matchmaking/v1/ready-check/accept";
        private static string GamePhaseUrl => urlBase + "lol-gameflow/v1/gameflow-phase";
        private static string GameflowAvailabilityUrl => urlBase + "lol-gameflow/v1/availability";

        #endregion

        #region SETUP
        public static void init() {
            string path = Path.Combine(BotConf.FilePath, @"League of Legends\lockfile");
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
                DBG.log("Unable to initialize ClientLCU.cs (unable to read Api port from process)", MessageLevel.Critical);
            }

        }

        public static bool IsApiReady() {
            using (HttpRequest request = CreateRequest()) {
                try {
                    DBG.log(GameflowAvailabilityUrl);
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

        #region GAME START
        public static bool CreateLobby(QueTypes queueId) {
            using (var request = CreateRequest()) {
                string response = request.Post(CreateLobbyUrl, $"{{\"queueId\": {(int)queueId} }}", "application/json").StatusCode.ToString();

                DBG.log($"{{\"queueId\": {(int)queueId} }}");
                DBG.log(response.ToString());

                if (response == "OK") {
                    return true;
                } else {
                    return false;
                }
            }
        }

        public static gameFlowPhase GetGamePhase() {
            using (var request = CreateRequest()) {
                var result = request.Get(GamePhaseUrl).ToString();
                result = Regex.Match(result, "\"(.*)\"").Groups[1].Value;
                return (gameFlowPhase)Enum.Parse(typeof(gameFlowPhase), result);
            }
        }

        public static void AcceptMatch() {
            using (HttpRequest req = CreateRequest()) {
                req.Post(AcceptUrl);
            }
        }

        #endregion

        private static HttpRequest CreateRequest() {
            HttpRequest request = new HttpRequest();
            request.IgnoreProtocolErrors = true;
            request.ConnectTimeout = BotConst.HttpRequestTimeout;
            request.ReadWriteTimeout = BotConst.HttpRequestTimeout;
            request.CharacterSet = BotConst.HttpRequestEncoding;
            request.AddHeader("Authorization", "Basic " + auth);
            return request;
        }
    }
}
