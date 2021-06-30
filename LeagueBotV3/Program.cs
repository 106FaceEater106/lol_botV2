using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using LCU;

using WindowsInput.Native;

/* Luanch param:
 *  -c {Config file}
 *  -DBG
 *  -lol {lolPath}
 *  -f {script}
 */

namespace LeagueBotV3 {
    class Program {
        public static void Main(string[] args) {

            DBG.init();
            if (Array.IndexOf(args,"-DBG") != -1) {
                DBG.getConsole();
                DBG.writeToConsole = true;
                DBG.consoleLogLvl = MessageLevel.Info;
                DBG.dbgValues["DBG"] = true;
            }
            DBG.log($"Init dbg done - {DBG.dbgValues["DBG"]}");


            int lolPathArg = Array.IndexOf(args, "-lol");
            int configArg = Array.IndexOf(args, "-c");
            int scriptArg = Array.IndexOf(args, "-f");

            DBG.log("Init config",MessageLevel.Info);
            if(configArg != -1 && configArg != args.Length-1) {
                Global.loadConfig(args[configArg+1]);
            } else {
                Global.loadConfig();
            }

            try {

                DBG.log("Init LCU", MessageLevel.Info);
                if (lolPathArg != -1 && lolPathArg != args.Length - 1) {
                    DBG.logIfDbg($"LOL path: \"{args[lolPathArg + 1]}\"");
                    clientLCU.init(args[lolPathArg + 1]);
                } else {
                    DBG.logIfDbg($"LOL path: {Global.dict["LOLPATH"]}");
                    clientLCU.init(Global.dict["LOLPATH"]);
                }

            } catch(Exception err) {
                DBG.log($"Faild to init luc. {err.GetType()}");
            }

            DBG.logIfDbg("Make bot and cli");
            Bot b = new();
            b.addCliCommands();

            if (scriptArg != -1 && scriptArg != args.Length - 1) {
                DBG.log("load script", MessageLevel.Info);
                string[] lines = File.ReadAllLines(args[scriptArg+1]);
                bool force = Array.IndexOf(args,"-force") != -1;
                cliManager.startScript(lines,force);
            }

            cliManager.start();
        }
    }
}
