using LeagueBot.AI;

namespace LeagueBot.Patterns.Actions {
    public class ExecuteAIAction : PatternAction {
        
        private baseAI AI;

        public ExecuteAIAction(baseAI ai, string description) : base(description) {
            this.AI = ai;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            AI.Execute();
        }

        public override void Dispose() {
            AI.Dispose();
        }
    }
}
