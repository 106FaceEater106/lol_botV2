using LeagueBot.AI;

namespace LeagueBot.Patterns.Actions {
    public class ExecuteAIAction : PatternAction {
        
        private AbstractAI AI;

        public ExecuteAIAction(AbstractAI ai, string description, double duration = 0) : base(description, duration) {
            this.AI = ai;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            var inGamePattern = ((MapPattern)pattern);
            inGamePattern.StartAI();
        }

        public override void Dispose() {
            AI.Dispose();
        }
    }
}
