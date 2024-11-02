using System;
using UnityEngine;

namespace GameLogic.MatchCore
{
    [Serializable]
    public class MonsterAbility
    {
        public string name;
        public int cooldown; // 技能冷却
        public int damage;
        public ElementType elementType;
        public bool canStun;
        public bool canHeal;
        public int stunDuration; // 眩晕持续回合
        public int healAmount; // 治疗量
        public CombatState.StateType stateToApply; // 应用的状态
        public int stateDuration; // 状态持续回合
        public float stateEffectValue; // 状态效果值
        public int remainingCooldown; // 剩余冷却时间

        public MonsterAbility()
        {
            remainingCooldown = cooldown;
        }

        public void Perform(ICharacter target)
        {
            if (remainingCooldown > 0)
            {
                return;
            }

            if (canStun)
            {
                target.AddCombatState(new CombatState(CombatState.StateType.Stunned, stunDuration));
            }

            if (canHeal)
            {
                Heal(target, healAmount);
            }

            if (damage > 0)
            {
                target.TakeDamage(damage);
            }

            if (stateToApply != CombatState.StateType.Normal)
            {
                target.AddCombatState(new CombatState(stateToApply, stateDuration, stateEffectValue));
            }

            remainingCooldown = cooldown;
        }

        public void DecrementCooldown()
        {
            if (remainingCooldown > 0)
            {
                remainingCooldown--;
            }
        }

        private void Heal(ICharacter character, int amount)
        {
            character.Heal(amount);
        }
    }
}