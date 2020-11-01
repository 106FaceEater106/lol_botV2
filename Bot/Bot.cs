using InputManager;
using LeagueBot.AI;
using LeagueBot.Constants;
using LeagueBot.DEBUG;
using LeagueBot.Event;
using LeagueBot.Patterns;
using System;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Drawing;

namespace LeagueBot {
    public class Bot {


        public event EventHandler<EndGameData> GameEndEvent;


        private Pattern Pattern;
        public AvailableGameType GameType;
        public String status; //TODO: REMOVE
        public Boolean working;
        public const Int32 buy_delay = 500;
        public bool isEvent = true;

        public Bot() {
            GameEndEvent += on_game_end;
            GameEndEvent += DBG.on_game_end;
        }

        public void ThreadProc() {
            this.Start(GameType);
            return;
        }

        /*
        public void dump_log_to_dbg() {
            DBG.dump_log();
        }
        */

        public void Start(AvailableGameType gameType = AvailableGameType.TFT) {
            GameType = gameType;
            working = true;

            switch (gameType) {
                case AvailableGameType.TFT:
                    ApplyPattern(new StartTFTPattern(this));
                    break;
            }
        }

        /*
        public void dump_log(string path) {
            Debug.WriteLine("<LOG START>");
            StreamWriter f = File.CreateText(path);

            string o = DBG.log_to_string();
            string o_base64 = Base64Encode(o);

            f.WriteLine(o_base64);
            Debug.WriteLine(o);
            Debug.WriteLine("<LOG END>");
            f.Close();
        }
        */

        /*
        public static string Base64Encode(string plainText) {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        */

        public void ApplyPattern(Pattern p, int i = 0) {
            if(Pattern != null) {
                Pattern = null;
            }
            Pattern = p;
            Pattern.Execute(i);
        }

        public void RightClick(Point point) {
            Mouse.Move(point.X, point.Y);
            Mouse.PressButton(Mouse.MouseKeys.Right, 150);
        }
        public void LeftClick(Point point, bool NoPress = false) {
            Mouse.Move(point.X, point.Y);
            if (!NoPress) {
                Mouse.PressButton(Mouse.MouseKeys.Left, 150);
            }
        }
        public void Abort(String stop_reson = "unknown reson") {
            if (Pattern != null) Pattern.Dispose();
            working = false;
            DBG.log("BOT STOPED: " + stop_reson, DateTime.Now, "BOT");
        }

        public void invoke_tft_game_end(object s, EndGameData data) {
            GameEndEvent?.Invoke(s, data);
        }

        private void on_game_end(object sender, EndGameData data) {
            string[] s = DBG.get_usage();
            foreach (string l in s) {
                DBG.log(l, DateTime.Now, "BOT");
            }
        }
    }
}
