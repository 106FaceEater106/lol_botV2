namespace LeagueBot.Patterns.Actions {
    public class DefinePatternAction : PatternAction {
        
        private Pattern Pattern;

        public DefinePatternAction(Pattern pattern, string description, double duration = 0) : base(description, duration) {
            this.Pattern = pattern;
            needWindowHelp = false;
        }

        public override void Apply(Bot bot, Pattern pattern) {
            //bot.ApplyPattern(Pattern);
            bot.nextPattern = Pattern;
        }

        public override void Dispose() {
            //Pattern.Dispose();
        }
    }
}
