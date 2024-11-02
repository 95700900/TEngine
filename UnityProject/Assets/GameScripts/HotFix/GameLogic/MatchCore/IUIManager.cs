using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public interface IUIManager
    {
        void UpdateBattleUI(IPlayer player, List<IMonster> monsters);
        UniTask<PlayerAction> PromptPlayerAction();
        UniTask<Skill> SelectSkill();
        UniTask<Prop> SelectProp();
    }
}