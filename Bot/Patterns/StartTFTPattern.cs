using System.Drawing;
using System.Diagnostics;

using LeagueBot.DEBUG;
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;

namespace LeagueBot.Patterns {
    public class StartTFTPattern : Pattern {
        public override string ProcessName => BotConst.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new MakeLobyAction(),
            new StartQueAction(),
            new AcceptQue(),
            new DefinePatternAction(new TFT_MapPattern(bot),"Executing Pattern : InGame",0),
        };

        public StartTFTPattern(Bot bot) : base(bot) {
        }
    }
}
