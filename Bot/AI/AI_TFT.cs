using InputManager;

using LeagueBot.Constants;
using LeagueBot.Patterns;
using LeagueBot.Windows;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace LeagueBot.AI {
    public class AI_TFT : AbstractAI {

        public AI_TFT(Bot bot, TFT_MapPattern pattern) : base(bot, pattern) {

        }

        public override void Stop() {

        }

        public override void Start() {
            Console.WriteLine("LOG: <TFT AI START>");
            //base.Start();
            Thread.Sleep(3000);
            int mode = 99;
            DateTime lastBuy = DateTime.Now;
            while (true) {
                Color px;
                Thread.Sleep(5000);
                Pattern.BringProcessToFront();
                Pattern.CenterProcessMainWindow();

                px = Interop.GetPixelColor(PixelsConstants.DEAT_EXIT_BUTTON);
                if (px == ColorConstants.DEAT_EXIT_BUTTON) {
                    Bot.LeftClick(PixelsConstants.DEAT_EXIT_BUTTON);
                    Thread.Sleep(2000);
                }



                switch (mode) {
                    case 0:
                        px = Interop.GetPixelColor(PixelsConstants.TFT_SURRENDER);

                        if (px == ColorConstants.TFT_SURRENDER) {
                            mode = 1; //buy mode;
                            Keyboard.KeyPress(Keys.Escape);
                            Console.WriteLine("LOG: BUING UNITS!");
                        }
                        break;
                    case 2:
                        Thread.Sleep(500);
                        //look for death
                        if (DateTime.Now.Subtract(lastBuy).TotalSeconds > Bot.buy_delay) {
                            mode = 1;
                        }
                        break;
                    case 1:
                    case 99:
                        px = Interop.GetPixelColor(PixelsConstants.SHOP_BORDER);
                        if (px == ColorConstants.SHOP_BORDER) {
                            for (int i = 0; i < PixelsConstants.BUY_UNIT.Length; i++) {
                                Bot.LeftClick(PixelsConstants.BUY_UNIT[i]);
                                Thread.Sleep(10);
                            }
                            if (mode == 99) {
                                mode = 0;
                                Keyboard.KeyPress(Keys.Escape);
                            } else {
                                mode = 2;
                            }

                            lastBuy = DateTime.Now;
                            for (int i = 0; i <= 50; i++) {
                                Keyboard.KeyPress(Keys.F);
                                Thread.Sleep(10);
                            }
                        } else {
                            Console.WriteLine("NOT IN STORE");
                            Debug.WriteLine(px.ToArgb());
                        }
                        break;
                }

                Thread.Sleep(5000);
            }


        }
    }
}
