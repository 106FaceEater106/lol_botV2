
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;

namespace LeagueBot.Patterns {
    class EndGamePatternTFT : Pattern {

        public EndGamePatternTFT(Bot bot) : base(bot) {
        }

        public override string ProcessName => BotConst.LoL_LAUNCHER_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
            new SleepAction(10000),
            new WaitForReconnect("i is stuck on reconnect?"),
            new GetPlacmentTFT("Looking for placement",0),
            new DefinePatternAction(new StartTFTPattern(bot), "Executing Pattern : TFT", 2)
        };
    }
}
