using System;
using System.Threading;
using System.Windows;

using System.Text.Json;

using LeagueBotV3;
using LeagueBotV3.AI;
using LeagueBotV3.Pattern;
using LeagueBotV3.Pattern.Action;

using LCU;
using LCU.Helper;


namespace testApp {
    class Program {
        static void Main(string[] args) {
            
            DBG.init();

            #if DEBUG
            DBG.getConsole();
            #endif

            Global.loadConfig();
            clientLCU.init(@"G:\lel");
            Bot b = new();
            b.addCliCommands();

            cliManager.start();
        }
    }
}
