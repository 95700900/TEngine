using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using TEngine;

namespace GameLogic.MatchCore
{
 
    public class BattleManager : MonoBehaviour, IBattleManager
    {
        private IPlayer _player;
        private List<IMonster> _monsters;
        private IUIManager _uiManager;
        private ISkillManager _skillManager;
        private IPropManager _propManager;

        public void Initialize(IPlayer player, List<IMonster> monsters, IUIManager uiManager,
            ISkillManager skillManager, IPropManager propManager)
        {
            _player = player;
            _monsters = monsters;
            _uiManager = uiManager;
            _skillManager = skillManager;
            _propManager = propManager;
        }

        public void InitializeBattle()
        {
            // 初始化玩家和怪物
            _player.Initialize();
            foreach (var monster in _monsters)
            {
                monster.Initialize();
            }

            // 显示战斗UI
            _uiManager.UpdateBattleUI(_player, _monsters);

            // 启动战斗循环
            StartCoroutine(BattleLoop());
        }

        private IEnumerator BattleLoop()
        {
            while (_player.IsAlive && AnyMonstersAlive())
            {
                yield return HandlePlayerTurn().AsAsyncUnitUniTask();
                if (AnyMonstersAlive())
                {
                    yield return HandleMonsterTurns().AsAsyncUnitUniTask();
                }

                // 更新战斗状态
                _player.UpdateCombatStates();
                foreach (var monster in _monsters)
                {
                    monster.UpdateCombatStates();
                }
            }

            // 结算战斗
            EndBattle();
        }

        public UniTask HandlePlayerTurn()
        {
            UniTask.SwitchToMainThread();
            // 玩家选择行动
            return _uiManager.PromptPlayerAction().ContinueWith(async action =>
            {
                switch (action)
                {
                    case PlayerAction.Attack:
                        _player.Attack(_monsters[0]);
                        BattleEffects.Instance.PlayAttackEffect(((CharacterBase)_player).transform, ((Monster)_monsters[0]).transform);
                        break;
                    case PlayerAction.UseSkill:
                        var skill = await _uiManager.SelectSkill();
                        _skillManager.UseSkill(skill, _monsters[0].Weakness);
                        if (skill.StateToApply != CombatState.StateType.Normal)
                        {
                            _monsters[0].AddCombatState(new CombatState(skill.StateToApply,
                                skill.StateDuration, skill.StateEffectValue));
                            BattleEffects.Instance.PlayBurnEffect(((Monster)_monsters[0]).transform); // 示例特效
                        }

                        break;
                    case PlayerAction.UseProp:
                        var prop = await _uiManager.SelectProp();
                        _propManager.UseProp(prop, _monsters[0].Weakness);
                        break;
                }

                // 更新UI
                _uiManager.UpdateBattleUI(_player, _monsters);
            });
        }

        public async UniTask HandleMonsterTurns()
        {
            var tasks = _monsters.Select(async (monster) =>
            {
                if (monster.IsAlive)
                {
                    // 怪物行动
                    monster.PerformAction(_player);

                    // 更新UI
                    _uiManager.UpdateBattleUI(_player, _monsters);
                    await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                }
            });
            await UniTask.WhenAll(tasks);
        }

        public bool AnyMonstersAlive()
        {
            return _monsters.Any(m => m.IsAlive);
        }

        public void EndBattle()
        {
            // 结算战斗结果
            Log.Debug(_player.IsAlive ? "Player wins!" : "Player loses!");
        }
    }
}