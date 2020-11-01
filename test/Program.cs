using System;
using System.Threading;

using LeagueBot;

namespace lol_bot_ligth {
    class Program {
        public static int Main() {

            bool to_exit = false;
            Bot bot = new Bot();
            Thread work_thread = null;


            while(!to_exit) {
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

                }

            }

            return 0;
        }
    }
}
