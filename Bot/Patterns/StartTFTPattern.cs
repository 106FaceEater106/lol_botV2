using System.Drawing;
using System.Diagnostics;

using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;

namespace LeagueBot.Patterns {

    static class big_brain {
        public static Point get_point(Bot bot) {
            Debug.WriteLine($"ask for event");
            if(bot.isEvent) {
                return PixelsConstants.TFT_EVENT;
            } else {
                return PixelsConstants.TFT_NO_EVENT;
            }
        }
    }

    public class StartTFTPattern : Pattern {
        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new ClickAction(ClickType.LEFT,PixelsConstants.PLAY_BUTTON,"START PLAY",1),
            new ClickAction(ClickType.LEFT,PixelsConstants.PVP_MBUTTON,"START PLAY",1),
            new ClickAction(ClickType.LEFT,big_brain.get_point(Bot),"CREATE TFT LOBY",1),// TODO: opt log ffs
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"GO TO LOBY",2),
            new ClickAction(ClickType.LEFT,PixelsConstants.CONFIRM_BUTTON,"START QUE",1),
            new AcceptQue(AvailableGameType.TFT,"WAIT FOR GAME"),
            new DefinePatternAction(new TFT_PATTERN(Bot),"Executing Pattern : InGame",0),
        };

        public StartTFTPattern(Bot bot) : base(bot) {
        }



        public override void OnProcessClosed() {

        }
    }
}
