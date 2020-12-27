using LeagueBot.AI;
using LeagueBot.Constants;
using LeagueBot.Patterns.Actions;
using System;
using System.Security;
using System.Threading;

namespace LeagueBot.Patterns {
    public abstract class MapPattern : Pattern {


        protected baseAI AI;

        public MapPattern(Bot bot) : base(bot) {
        }


        public override string ProcessName => BotConst.LoL_GAME_PROCESS;
        public override PatternAction[] Actions => new PatternAction[] {};

        [Obsolete]
        public void StartAI() {
            AI.Execute();
        }

        public override void OnProcessClosed() {
            AI.stop();
            base.OnProcessClosed();
        }
    }
}
