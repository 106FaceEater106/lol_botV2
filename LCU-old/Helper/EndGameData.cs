using System;

namespace LCU.Helper {

    public delegate void TftGameEndEventHandler(EndGameData data);

    public class EndGameData : EventArgs {
        public int place;
        public string sender;
        public int GameLength;
    }
}
