using LeagueBot.AI;
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System.Threading;

namespace LeagueBot.Patterns {
    public abstract class MapPattern : Pattern {
        public abstract AbstractAI AI {
            get;
        }

        public MapPattern(Bot bot) : base(bot) {
        }


        public override string ProcessName => LeagueConstants.LoL_GAME_PROCESS;

        public override PatternAction[] Actions => new PatternAction[]
        {
        };

        public void StartAI() {
            AI.Start();
        }
        public override void OnProcessClosed() {
            AI.Stop();
            Thread.Sleep(8000);
            base.OnProcessClosed();
        }
    }
}
