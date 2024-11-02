using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public interface IBattleManager
    {
        void InitializeBattle();
        UniTask HandlePlayerTurn();
        UniTask HandleMonsterTurns();
        bool AnyMonstersAlive();
        void EndBattle();
    }
}