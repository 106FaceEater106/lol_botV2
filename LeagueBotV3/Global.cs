using System;
using System.Collections.Generic;
using System.IO;

using LeagueBotV3;

namespace LeagueBotV3 {
    public static class Global {

        public static Dictionary<string, string> dict = new();

        public static string GameProc = "League of Legends";
        public static string ClientProc = "LeagueClientUx";

        private static KeyValuePair<string, string>[] defaultKP = {
            new KeyValuePair<string, string>("LOLPATH","C:\\Riot Games"),
            new KeyValuePair<string, string>("DEBUG","1"),
        };

        public static void loadConfig(string path = "./botConf.conf") {
            DBG.log("Loading config file");
            try {
                string data = File.ReadAllText(path);

                int lineCount = 0;
                foreach (string line in data.Split('\n')) {
                    string[] splitLine = line.Split('=');
                    splitLine[0] = splitLine[0].Replace(" ","").ToUpper();
                    if(splitLine.Length != 2) {
                        DBG.log($"Line {lineCount} is invalid", MessageLevel.Warning);
                    } else if(dict.ContainsKey(splitLine[0])) {
                        DBG.log($"key {splitLine[0]} is a duplicate and will be ignored",MessageLevel.Warning);
                    } else {
                        splitLine[1] = splitLine[1].Replace(" ","");
                        dict.Add(splitLine[0], splitLine[1]);
                    }
                }

            } catch(Exception err) {
                DBG.log($"failed to load settings form {path}. using default settings",MessageLevel.Warning);
                DBG.log(err);
            }

            foreach(KeyValuePair<string,string> kp in defaultKP) {
                if(!dict.ContainsKey(kp.Key)) {
                    DBG.log($"No value set for {kp.Key} using {kp.Value}");
                }
            }
        }

    }
}
