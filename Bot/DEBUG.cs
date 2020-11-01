using LeagueBot.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LeagueBot.DEBUG {
    public static class DBG {

        static public Boolean Accept = true;
        static public Boolean AcuatlQue = true;
        static public Boolean NoStuckDust = false;
        static public Boolean LiveLog = true;

        //static private List<string> log_list = new List<string>();

        public static string[] get_static_info() {
            List<string> o = new List<string>();
            Process proc = Process.GetCurrentProcess();

            o.Add($"Proc name: {proc.ProcessName}");
            o.Add($"Computer name: {proc.MachineName}");

            return o.ToArray();

        }

        public static string[] get_usage() {
            List<string> o = new List<string>();
            Process proc = Process.GetCurrentProcess();

            o.Add($"Current mem usage: {proc.PrivateMemorySize64}");
            o.Add($"Peak mem usage: {proc.PeakVirtualMemorySize64}");
            o.Add($"User processor time: {proc.UserProcessorTime}");
            o.Add($"Physical memory: {proc.PeakWorkingSet64}");
            o.Add($"Responding: {proc.Responding}");

            return o.ToArray();
        }

        public static void log(string[] info, DateTime t, String creator = "BOT") {
            foreach(string s in info) {
                log(s,t,creator);
            }
        }

        public static void log(String info, DateTime t, String creator = "BOT") {
            String entry = creator + "(" + t.ToString() + ") - " + info;
            Console.WriteLine(entry);
        }
        /*
        public static void dump_log() {
            Debug.WriteLine(log_to_string());
        }
        
        public static string log_to_string() {
            return string.Join("\n", log_list.ToArray());
        }
        */
        public static void on_game_end(object sender, EndGameData data) {
            Debug.WriteLine("TEST");
            string s = $"GOT {data.place} PLACE IN TFT GAME";
            log(s, DateTime.Now, data.sender);
        }

    }
}
