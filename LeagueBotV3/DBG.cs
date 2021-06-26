using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using System.Text;

namespace LeagueBotV3 {

    public enum MessageLevel {
        Info,
        Debug,
        Error,
        Critical,
        Warning,
    }

    public static class DBG {

        private static StreamWriter fileWriter = null;
        private static StreamWriter consoleWriter = null;

        private static string logPath = "./";
        private static string logName = "log.log";

        public static string logFile => Path.Join(logPath,logName);
        public static Dictionary<string, bool> dbgValues = new Dictionary<string, bool>();

        public static string dateStr => $"({DateTime.Now})";

        public static bool writeToConsole = true;
        public static bool rainbow = false;
        public static ConsoleColor[] colors = new ConsoleColor[] {
            ConsoleColor.Red,
            ConsoleColor.DarkYellow,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Blue,
            ConsoleColor.Magenta,
        };
        
        public static void init(bool dbg = false) {

            string path = Path.Combine(logPath, logName);

            if (File.Exists(path)) {
                File.Delete(path);
            }

            FileStream f = File.OpenWrite(path);
            fileWriter = new StreamWriter(f);
            fileWriter.AutoFlush = true;

            #if DEBUG
                dbg = true;
            #endif

            dbgValues.Add("DBG", dbg);

            try {
                consoleWriter = new StreamWriter(Console.OpenStandardOutput());
                consoleWriter.AutoFlush = true;
            } catch {
            }
        }

        private static void write(string msg) {

            if(rainbow) {
                Console.ForegroundColor = colors[0];

                ConsoleColor[] consoleColor = new ConsoleColor[colors.Length];
                consoleColor[0] = colors[colors.Length-1];
                Array.Copy(colors,0, consoleColor,1,colors.Length-1);
                colors = consoleColor;
            }

            fileWriter.WriteLine(msg);
            if(writeToConsole) {
                consoleWriter?.WriteLine(msg);
            }
        }


        public static void log(dynamic msg, MessageLevel lvl = MessageLevel.Info) {
            string strMsg;
            try {
                strMsg = (string)msg;
            } catch {
                write($"{dateStr} - ({MessageLevel.Critical}) - Failed to cast msg to string!");
                write(Environment.StackTrace);
                return;
            }

            write($"{dateStr} - ({lvl}) - {strMsg}");
        }

        public static void logIfDbg(dynamic msg) {
            if(dbgValues["DBG"]) {
                log(msg,MessageLevel.Debug);
            }
        }

        public static void getConsole() {
            try {
                AllocConsole();
                consoleWriter = new StreamWriter(Console.OpenStandardOutput());
                consoleWriter.AutoFlush = true;
            } catch(Exception err) {
                log($"Failed to get console\n{err}", MessageLevel.Critical);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
