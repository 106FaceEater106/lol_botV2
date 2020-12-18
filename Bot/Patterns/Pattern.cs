﻿using InputManager;
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
        public virtual PatternAction[] Actions {
            get;
            set;
        }
        private int I {
            get;
            set;
        }
        protected Bot Bot {
            get;
            private set;
        }
        public Pattern(Bot bot) {
            this.Bot = bot;
        }

        protected bool Disposed {
            get;
            private set;
        }
        public virtual void Initialize() {
            BringProcessToFront();
        }
        public void BringProcessToFront() {
            Interop.BringWindowToFront(ProcessName);
        }
        public void CenterProcessMainWindow() {
            Interop.CenterProcessWindow(ProcessName);
        }

        public virtual void OnProcessClosed() {

        }

        public void Execute(int i = 0) {
            I = i;
            while (I < Actions.Length) {
                if (Disposed) {
                    return;
                }
                if (!Interop.IsProcessOpen(ProcessName)) {
                    OnProcessClosed();
                    return;
                }
                try {
                    BringProcessToFront();
                    CenterProcessMainWindow();
                    DBG.log("(" + this.GetType().Name + "): " + Actions[I].ToString());
                    Actions[I].Apply(Bot, this);
                    Thread.Sleep((int)(Actions[I].Duration * 1000));
                    Actions[I] = null;
                    I++;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex);
                }
            }
            I = 0;
        }

        public virtual void Dispose() {
            Disposed = true;
            foreach(PatternAction a in Actions) {
                a.Dispose();
            }
        }
    }
}
