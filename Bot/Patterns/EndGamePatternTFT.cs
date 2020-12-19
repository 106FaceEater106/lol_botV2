
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;

namespace LeagueBot.Patterns {
    class EndGamePatternTFT : Pattern {

        public EndGamePatternTFT(Bot bot) : base(bot) {
        }

        public override string ProcessName => LeagueConstants.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new WaitForReconnect("i is stuck on reconnect?"),
            //new AcceptStuff("Accepting loot",5),
            new GetPlacmentTFT("Looking for placement",0),
            //new ClickAction(ClickType.LEFT, PixelsConstants.LEVELUP_BUTTON,"LevelUp!",5),
            //new ClickAction(ClickType.LEFT, PixelsConstants.LEAVE_BUTTON, "Leave Game",3),
            new DefinePatternAction(new StartTFTPattern(Bot), "Executing Pattern : TFT", 2)
        };
    }
}
