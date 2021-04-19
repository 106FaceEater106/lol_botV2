using LeagueBot.Windows;
using System.Drawing;
using System.Threading;

namespace LeagueBot.Patterns.Actions {
    public class WaitUntilColorAction : PatternAction {
        private Point Point {
            get;
            set;
        }
        private Color Color {
            get;
            set;
        }

        private int accuracy {
            get;
            set;
        }

        public WaitUntilColorAction(Point point, Color color, string description, double duration = 0, int accuracy = 1) : base(description, duration) {
            this.Point = point;
            this.Color = color;
            this.accuracy = accuracy;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            bool valid = false;
            while (!valid && !isStoped) {
                pattern.CenterWindow();
                var px = Interop.GetPixelColor(Point);

                if (px.R > Color.R - accuracy && px.R < Color.R + accuracy ||
                    px.G > Color.G - accuracy && px.G < Color.G + accuracy ||
                    px.B > Color.B - accuracy && px.B < Color.B + accuracy) {
                    valid = true;
                } else {

                    Thread.Sleep(2000);
                }
            }

        }
    }
}
