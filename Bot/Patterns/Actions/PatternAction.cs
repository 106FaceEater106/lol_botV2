using System;

namespace LeagueBot.Patterns.Actions {
    public abstract class PatternAction : IDisposable {
        private string Description {
            get;
            set;
        }
        
        [Obsolete("if a action need a deley build it in")]
        public double Duration {
            get;
            set;
        }

        public bool needWindowHelp = true;

        protected bool isStoped = false;

        public virtual void stop() {
            isStoped = true;
        }

        public PatternAction(string description, double duration = 0) {
            Description = description;
            Duration = duration;
            //Status = this.ToString();

        }
        public abstract void Apply(Bot bot, Pattern pattern);

        public override string ToString() {
            return Description;
        }

        public virtual void Dispose() {
            stop();
        }
    }
}
