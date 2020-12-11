using System;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;


using hotKey;
using LeagueBot;
using System.Reflection;

namespace lol_bot_ligth {
    class Program {


        static Bot bot = new Bot();
        static Thread work_thread = null;
        static bool to_exit = false;

        public static int Main() {

            Assembly a = Assembly.LoadFrom("Bot.dll");
            Version v = a.GetName().Version;

            Console.WriteLine($"Client version: {LeagueBot.DEBUG.DBG.getVersion()}");
            Console.WriteLine($"Bot version: {v.Major}.{v.Minor}.{v.Build}");


            #region SETUP_HOTKEY
            HotKeyManager.RegisterHotKey(Keys.F10, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += new EventHandler<HotKeyEventArgs>(stop_key_pressed);
            Console.WriteLine("Use ALT+F10 to stop the bot");
            #endregion

            Console.WriteLine("use help to get help");

            while (!to_exit) {
                Console.Write("> ");
                string com = Console.ReadLine().ToString().ToUpper();

                switch (com) {
                    case "EXIT":
                        Console.WriteLine("stoping");
                        return 0;

                    case "SET EVENT":
                        bool valid = false;
                        while (!valid) {
                            Console.Write("Is there RGM (y/n): ");
                            string inn = Console.ReadLine().ToUpper();

                            if(inn == "Y") {
                                bot.isEvent = true;
                                valid = true;
                            } else if(inn == "N") {
                                bot.isEvent = false;
                                valid = true;
                            }
                            
                            if(valid) {
                                Console.WriteLine($"Event set to {bot.isEvent}");
                            }
                        }
                        break;
                    case "START":
                        work_thread?.Abort();
                        work_thread = null;
                        work_thread = new Thread(bot.ThreadProc);
                        work_thread.Start();
                        break;
                    case "END":
                        if(work_thread != null) {
                            work_thread.Abort();
                            work_thread = null;
                        }
                        break;

                    case "EGG":
                    case "SAI":
                    case "TOBMAN":
                    case "BRAUM":
                        for(int i = 0; i < 50; i++) {
                            Console.WriteLine("http://www.randomkittengenerator.com/");
                        }
                        break;

                    default:
                        Console.WriteLine($"{com} is not a command");
                        break;

                }
            }

            return 0;
        }

        static void  stop_key_pressed(object sender, HotKeyEventArgs e) {
            Console.WriteLine("STOPING BOT");
            if (work_thread != null) {
                work_thread.Abort();
                work_thread = null;
            }
        }

    }
}
