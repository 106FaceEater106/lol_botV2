using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

using Microsoft.Win32;

using LeagueBot.DEBUG;

namespace BotUI {
    static class Program {
        [STAThread]
        static void Main() {
            DBGV2.init();
            string dbg = ConfigurationManager.AppSettings.Get("Debug").ToUpper();
            switch(dbg) {
                case "GAY":
                case "YES":
                    DBGV2.rainbow = dbg == "GAY";
                    DBGV2.getConsole();
                    break;
                case "NO":
                    break;

                default:
                    DBGV2.getConsole();
                    DBGV2.log("DBG need to be yes or no");
                    break;
            }

            DBGV2.log("App start",MessageLevel.Info);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
