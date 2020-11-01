using LeagueBot.Constants;
using LeagueBot.Patterns;
using System;

namespace LeagueBot.AI {
    public abstract class AbstractAI {
        protected DateTime GameStartTime {
            get;
            set;
        }

        protected TimeSpan GameTimespan {
            get {
                return DateTime.Now - GameStartTime;
            }
        }
        protected Bot Bot {
            get;
            private set;
        }
        /*protected Summoner Summoner {
            get;
            private set;
        }*/
        protected MapPattern Pattern {
            get;
            private set;
        }
        protected Side Side {
            get;
            private set;
        }
        public AbstractAI(Bot bot, MapPattern pattern = null) {
            this.Bot = bot;
            this.Pattern = pattern;
            //this.Summoner = new Summoner(bot);
            if (pattern != null) {
                this.Side = Pattern.GetSide();
            } else {
                this.Side = Side.TFT_FFA;
            }
        }
        public virtual void Start() {
            GameStartTime = DateTime.Now;
        }
        public abstract void Stop();
    }
}
