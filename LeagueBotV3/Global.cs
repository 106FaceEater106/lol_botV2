using System;
using System.Collections.Generic;
using System.IO;

using LeagueBotV3;

namespace LeagueBotV3 {


    public record Point {
        public int X { get; init; }
        public int Y { get; init; }
    }

    public static class Global {

        public static Dictionary<string, string> dict = new();

        public static string GameProc = "League of Legends";
        public static string ClientProc = "LeagueClientUx";

        public static Point ExitButton = new Point { X = 415, Y = 393 };
         

        private static KeyValuePair<string, string>[] defaultKP = {
            new KeyValuePair<string, string>("LOLPATH","C:\\Riot Games"),
            new KeyValuePair<string, string>("DEBUG","1"),
        };

        public static void loadConfig(string path = "./botConf.conf") {
            DBG.logIfDbg($"Loading config file: {path}");
            try {
                string data = File.ReadAllText(path);

                //int lineCount = 0;
                foreach (string line in data.Split('\n')) {
                    string[] splitLine = line.Split('=');
                    splitLine[0] = splitLine[0].Replace(" ","").ToUpper();


                    if (dict.ContainsKey(splitLine[0])) {
                        DBG.log($"key {splitLine[0]} is a duplicate and will be ignored", MessageLevel.Warning);
                    } else {
                        string key = splitLine[0];
                        splitLine[0] = ""; // good code :)
                        string val = string.Join(" ", splitLine).Trim(new char[] { '\r','\n','\t',' ' });
                        dict.Add(key, val);
                        DBG.logIfDbg($"load {key} = {val}");
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
