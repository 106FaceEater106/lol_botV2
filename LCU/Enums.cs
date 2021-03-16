using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LCU {
    public enum QueTypes : int {
        RankedTFT = 1100,
        NormalTFT = 1090
    }

    public enum gameFlowPhase {
        None,
        Lobby,
        NoClient,
        Reconnect,
        EndOfGame,
        InProgress,
        ReadyCheck,
        ChampSelect,
        Matchmaking,
        PreEndOfGame,
        WaitingForStats,
    }
}
