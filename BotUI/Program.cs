using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;

using LeagueBot.DEBUG;

namespace BotUI {
    static class Program {
        [STAThread]
        static void Main() {

            DBGV2.init();

            string dbg = ConfigurationManager.AppSettings.Get("Debug");

            if (dbg.ToUpper() == "YES") {
                DBGV2.getConsole();
            } else if (dbg.ToUpper() == "GAY") {
                DBGV2.rainbow = true;
                DBGV2.getConsole();
            } else if (dbg.ToUpper() != "NO") {
                DBGV2.getConsole();
                DBGV2.log("Debug need to be yes or no. stupid");
            }

            DBGV2.log("App start",MessageLevel.Info);

            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
