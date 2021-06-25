using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using InputManager;

using LeagueBot.Constants;
using LeagueBot.Windows;
using LeagueBot.DEBUG;

namespace LeagueBot.AI {
    public abstract class baseAI : IDisposable {

        protected string ps_name = BotConst.LoL_GAME_PROCESS;
        protected bool isStoped = false;
        protected Bot bot;

        public bool endGameASAP = false;

        public baseAI(Bot bot) {
            this.bot = bot;
        }

        protected void CenterWindow() {
            Interop.BringWindowToFront(ps_name);
            Interop.CenterProcessWindow(ps_name);
        }

        protected bool isInShop() {
            CenterWindow();
            return Interop.GetPixelColor(PixelsConstants.SHOP_BORDER) == ColorConstants.SHOP_BORDER;
        }

        public abstract void Execute();

        public void stop() {
            isStoped = true;
        }

        public void exitGame() {
            CenterWindow();
            int x = PixelsConstants.DEAT_EXIT_BUTTON.X;
            int y = PixelsConstants.DEAT_EXIT_BUTTON.Y;
            Mouse.Move(x,y);
            Thread.Sleep(75);
            Mouse.PressButton(Mouse.MouseKeys.Left,75);
        }

        public void buyUnit(int unitNr) {
            if(unitNr < 0 || unitNr > 4) {
                throw new Exception($"cant bou uint nr {unitNr}");
            }

            CenterWindow();

            int x = PixelsConstants.BUY_UNIT[unitNr].X;
            int y = PixelsConstants.BUY_UNIT[unitNr].Y;
            Mouse.Move(x,y);
            Thread.Sleep(25);
            Mouse.PressButton(Mouse.MouseKeys.Left,15);
        }

        public void LevelUp() {
            Keyboard.KeyPress(System.Windows.Forms.Keys.F,50);
        }

        public void FF() {
            DBGV2.log("trying to ff");
            Keyboard.KeyPress(System.Windows.Forms.Keys.Enter,100);
            Thread.Sleep(75);
            Keyboard.KeyPress(System.Windows.Forms.Keys.OemBackslash,100);
            Thread.Sleep(75);
            Keyboard.KeyPress(System.Windows.Forms.Keys.F,100);
            Thread.Sleep(75);
            Keyboard.KeyPress(System.Windows.Forms.Keys.F, 100);
            Thread.Sleep(75);
            Keyboard.KeyPress(System.Windows.Forms.Keys.Enter,100);
        }

        public virtual void OnProcessClosed() {
        }

        public virtual void Dispose() {
        }
    }
}
