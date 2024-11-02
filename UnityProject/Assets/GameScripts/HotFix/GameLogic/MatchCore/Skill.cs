using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class Skill
    {
        public string Name { get; private set; }
        public int Cooldown { get; private set; }
        public int Damage { get; private set; }
        public ElementType ElementType { get; private set; }
        public CombatState.StateType StateToApply { get; private set; } // 状态类型
        public int StateDuration { get; private set; } // 状态持续的回合数
        public float StateEffectValue { get; private set; } // 状态效果的值
        public SkillEffect SkillEffect { get; private set; } 

        // 构造函数
        public Skill(string name, int cooldown, int damage, ElementType elementType, 
            CombatState.StateType stateToApply, int stateDuration, float stateEffectValue,SkillEffect skillEffect)
        {
            Name = name;
            Cooldown = cooldown;
            Damage = damage;
            ElementType = elementType;
            StateToApply = stateToApply; // 设置状态类型
            StateDuration = stateDuration;
            StateEffectValue = stateEffectValue;
            SkillEffect = skillEffect;
        }

        // 执行技能
        public void Perform(ICharacter target)
        {
            // 根据元素类型计算伤害
            if (target is IMonster monster && monster.Resistances.Contains(ElementType))
            {
                // 如果目标对这个元素有抗性，则减少伤害
                target.TakeDamage(Damage / 2);
            }
            else if (target is IMonster monster1 && monster1.Weakness == ElementType)
            {
                // 如果目标对这个元素有弱点，则增加伤害
                target.TakeDamage(Damage * 2);
            }
            else
            {
                target.TakeDamage(Damage);
            }

            // 应用状态效果
            if (StateToApply != CombatState.StateType.Normal)
            {
                target.AddCombatState(new CombatState(StateToApply, StateDuration, StateEffectValue));
            }
        }
    }
}