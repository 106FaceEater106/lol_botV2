using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using LeagueBot.Event;

namespace LeagueBot.DEBUG {
    public static class DBG {

        static public Boolean Accept = true;
        static public Boolean AcuatlQue = true;
        static public Boolean NoStuckDust = false;
        static public Boolean LiveLog = true;

        static private string file_path = @".";
        static private string file_name = "log.log";
        static private StreamWriter writer;
        static private FileStream ostrm;
        static private TextWriter oldOut;

        static private bool is_init = false;

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        
        static public void init() {
            is_init = true;

            #if DEBUG
                DBG.openConsole();
                return;
            #endif

            ostrm = new FileStream(file_name, FileMode.OpenOrCreate, FileAccess.Write);
            writer = new StreamWriter(ostrm);
            Console.SetOut(writer);
        }

        public static void openConsole() {
            AllocConsole();
        }

        static public void end() {
            try {
                writer.Close();
                ostrm.Close();
            } catch { }
        }

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

        public static void log(string[] info, String creator = "BOT") {
            foreach(string s in info) {
                log(s,creator);
            }
        }

        [Obsolete("Start using log with no date time")]
        public static void log(String info, DateTime t, String creator = "BOT") {
            String entry = creator + "(" + t.ToString() + ") - " + info;
            Console.WriteLine(entry);
        }

        public static void log(String info, String creator = "BOT") {
            DateTime t = DateTime.Now;
            String entry = creator + "(" + t.ToString() + ") - " + info;
            Console.WriteLine(entry);
            Debug.WriteLine(entry);
        }

        public static void on_game_end(object sender, EndGameData data) {
            Debug.WriteLine("TEST");
            string s = $"GOT {data.place} PLACE IN TFT GAME";
            log(s, data.sender);
        }

        public static string getVersion() {
            Version v = Assembly.GetEntryAssembly().GetName().Version;
            return v.ToString();
        }

    }
}
