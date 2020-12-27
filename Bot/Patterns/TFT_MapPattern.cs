using LeagueBot.AI;
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System.Threading;

namespace LeagueBot.Patterns {
    public class TFT_MapPattern : MapPattern {


        public TFT_MapPattern(Bot bot) : base(bot) {
            AI = new TFT_AI(bot);
        }


        public override string ProcessName => BotConst.LoL_GAME_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
           new LogAction("Starting TFT_MapPattern","LOG"),
           new WaitUntilProcessFocusAction(BotConst.LoL_GAME_PROCESS,"Waiting Game Focus..."),
           new WaitUntilColorAction(PixelsConstants.TFT_MAPBORDER,ColorConstants.TFT_MAPBORDER,"Waiting for Game to load..."),
           new ExecuteAIAction(AI,"Launching'IA ("+AI.GetType().Name+")"),
        };


        public override void OnProcessClosed() {
            Thread.Sleep(8000);
            // bot.ApplyPattern(new EndGamePatternTFT(bot));
            base.OnProcessClosed();
        }
    }
}
