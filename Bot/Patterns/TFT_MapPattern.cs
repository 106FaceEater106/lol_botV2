using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System.Threading;

namespace LeagueBot.Patterns {
    public abstract class TFT_MapPattern : MapPattern {
        public TFT_MapPattern(Bot bot) : base(bot) {
        }


        public override string ProcessName => LeagueConstants.LoL_GAME_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
           new LogAction("Starting TFT_MapPattern","LOG"),
           new WaitUntilProcessFocusAction(LeagueConstants.LoL_GAME_PROCESS,"Waiting Game Focus..."),
           new WaitUntilColorAction(PixelsConstants.TFT_MAPBORDER,ColorConstants.TFT_MAPBORDER,"Waiting for Game to load..."),
           new ExecuteAIAction(AI,"Launching'IA ("+AI.GetType().Name+")"),
        };

        public override void OnProcessClosed() {
            AI.Stop();
            Thread.Sleep(8000);
            Bot.ApplyPattern(new EndGamePatternTFT(Bot));
            base.OnProcessClosed();
        }
    }
}
