using InputManager;
using LeagueBot.DEBUG;
using LeagueBot.Patterns.Actions;
using LeagueBot.Windows;
using System;
using System.Threading;
using System.Diagnostics;

using System.Data;

namespace LeagueBot.Patterns {
    public abstract class Pattern : IDisposable {
        
        public virtual string ProcessName {
            get;
        }

        public virtual PatternAction[] Actions { get; set; }
        
        protected Bot bot { get; private set; }

        protected bool Disposed { get; private set; }
        private bool isStoped = false;

        public Pattern(Bot bot) {
            this.bot = bot;
        }

        public virtual void Initialize() {
            CenterWindow();
        }
        public void CenterWindow() {
            Interop.BringWindowToFront(ProcessName);
            Interop.CenterProcessWindow(ProcessName);
        }

        public virtual void OnProcessClosed() {

        }

        [Obsolete]
        public void Execute(int start_index = 0) {
            int i = start_index;
            while (i < Actions.Length) {
                if (Disposed) {
                    return;
                }
                if (!Interop.IsProcessOpen(ProcessName)) {
                    OnProcessClosed();
                }
                try {

                    CenterWindow();

                    DBG.log("(" + this.GetType().Name + "): " + Actions[i].ToString());
                    Actions[i].Apply(bot, this);
                    Thread.Sleep((int)(Actions[i].Duration * 1000));
                    Actions[i] = null;
                    i++;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
        }

        public void ExecuteV2() {
            int actionIndex = 0;

            while(actionIndex < Actions.Length) {
                
                if(isStoped) {
                    return;
                }

                if(Disposed) {
                    DBGV2.log("Patter is disposed while runing", MessageLevel.Warning);
                    return;
                }

                if(!Interop.IsProcessOpen(ProcessName)) {
                    this.OnProcessClosed();
                }
                
                PatternAction action = Actions[actionIndex];
                DBGV2.log("(" + this.GetType().Name + "): " + action.ToString());
                
                if(action.needWindowHelp) {
                    try {
                        CenterWindow();
                    } catch {
                        DBGV2.log("Faild to center window", MessageLevel.Warning);
                    }
                }
                bot.setCurrentAction(action,this);
                action.Apply(bot, this);
                actionIndex++;
            }
        }

        public void stop() {
            isStoped = true;
        }

        public virtual void Dispose() {
            Disposed = true;
            foreach(PatternAction a in Actions) {
                a.Dispose();
            }
        }
    }
}
