using LeagueBot.AI;

namespace LeagueBot.Patterns.Actions {
    public class ExecuteAIAction : PatternAction {
        
        private baseAI AI;

        public override void stop() {
            base.stop();
            AI.stop();
        }

        public ExecuteAIAction(baseAI ai, string description) : base(description) {
            this.AI = ai;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            bot.ai = AI;
            AI.Execute();
            bot.ai = null;
        }

        public override void Dispose() {
            AI.Dispose();
        }
    }
}
