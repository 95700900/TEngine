using System;
using UnityEngine;

namespace GameLogic.MatchCore
{
    [Serializable]
    public class CombatState
    {
        public enum StateType
        {
            Normal,
            Stunned,
            Frozen,
            Burning,
            Shielded,
            Burn,   // 燃烧
            Freeze, // 冻结
            Poison, // 中毒
            Stun,   // 眩晕
            Shield  // 护盾
        }

        public StateType stateType;
        public int duration; // 状态持续回合数
        public int remainingDuration; // 剩余回合数
        public float effectValue; // 效果值（如燃烧伤害）

        public CombatState(StateType type, int duration, float effectValue = 0)
        {
            this.stateType = type;
            this.duration = duration;
            this.remainingDuration = duration;
            this.effectValue = effectValue;
        }

        public void ApplyEffect(CharacterBase character)
        {
            switch (stateType)
            {
                case StateType.Stunned:
                    character.IsStunned = true;
                    break;
                case StateType.Frozen:
                    character.IsFrozen = true;
                    break;
                case StateType.Burning:
                    character.TakeDamage((int)effectValue);
                    break;
                case StateType.Shielded:
                    character.ShieldValue += (int)effectValue;
                    break;
            }
        }

        public void DecrementDuration()
        {
            remainingDuration--;
        }

        public bool IsExpired()
        {
            return remainingDuration <= 0;
        }
    }
}