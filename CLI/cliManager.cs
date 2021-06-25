using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI {

    /*
     * returns
     * 0 - Ok
     * 1 - print help
     */
    public delegate int commandCB(string[] arg);

    struct command {
        public string name { get; init; }
        public commandCB cb { get; init; }
        public string helpTxt { get; init; }
    }


    public class cliManager {

        private commandCB exitCb;
        private Dictionary<string, command> commands = new();
        private bool runing = false;

        public cliManager(commandCB exitCb = null) {
            #region default commands
            addCommand("help", help, "Get command help text. help {command}. use dump to se all commands");
            addCommand("dump", dumpCommands, "show a list with all commands");
            addCommand("clear", clear, "Clear terminal");
            addCommand("exit", exit, "stop the cli");
            #endregion

            this.exitCb = exitCb;
        }

        public void start() {

            runing = true;
            while (runing) {
                Console.Write("-> ");
                string com = Console.ReadLine();

                try {
                    runCommand(com);
                } catch (Exception err) {
                    Console.WriteLine(err);
                    Console.WriteLine($"command failed to run");
                }
            }
        }

        public void addCommand(string command, commandCB cb, string helpTxt = "No help text :(") {

            if (command.Contains(" ")) {
                throw new Exception("Command cant have space");
            }

            string key = command.ToUpper();
            if (commands.ContainsKey(key)) {
                throw new Exception($"Command {command} already existing");
            }

            commands.Add(
                key,
                new command { cb = cb, helpTxt = helpTxt, name = command }
                );
        }

        public void runCommand(string rawCommand) => runCommand(rawCommand.Split(" "));
        public void runCommand(string[] splitCommand) {
            string key = splitCommand[0].ToUpper();
            if (commands.ContainsKey(key)) {
                command com = commands[key];

                string[] args;
                if (splitCommand.Length > 1) {
                    args = new string[splitCommand.Length - 1];
                    Array.Copy(splitCommand, 1, args, 0, args.Length);
                } else {
                    args = new string[] { };
                }
                if (com.cb(args) == 1) {
                    Console.WriteLine(com.helpTxt);
                }
            } else {
                Console.WriteLine($"No command named {splitCommand[0]}");
            }
        }

        private int help(string[] args) {
            if (args.Length == 0) {
                return 1;
            } else {
                string key = args[0].ToUpper();
                if (commands.ContainsKey(key)) {
                    Console.WriteLine(commands[key].helpTxt);
                } else {
                    Console.WriteLine($"No command {args[0]}");
                }
            }
            return 0;
        }

        private int dumpCommands(string[] args) {
            foreach (KeyValuePair<string, command> com in commands) {
                Console.WriteLine($"Command: {com.Value.name}");
            }
            Console.WriteLine($"\n{commands.Count} commands");
            Console.WriteLine($"use \"help {{command}}\" to get more help");
            return 0;
        }

        private int clear(string[] args) {
            Console.Clear();
            return 0;
        }

        public int exit(string[] args) {
            runing = false;
            exitCb(args);
            return 0;
        }
    }
}
