using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public class SkillManager : MonoBehaviour
    {

        [SerializeField] public List<Skill> skills;

        public void UseSkill(Skill skill, ElementType target)
        {
            if (skill.SkillEffect.targetType == target || skill.SkillEffect.targetType == ElementType.All)
            {
                // 应用技能效果
                ApplySkillEffect(skill.SkillEffect);
            }
        }

        private void ApplySkillEffect(SkillEffect effect)
        {
            // 根据效果类型应用效果
            if (effect.damage > 0)
            {
                // 造成伤害
                Debug.Log($"Caused {effect.damage} damage to the target.");
            }

            if (effect.freezeTurns > 0)
            {
                // 冰冻目标
                Debug.Log($"Froze the target for {effect.freezeTurns} turns.");
            }

            if (effect.burnTurns > 0)
            {
                // 燃烧目标
                Debug.Log($"Burned the target for {effect.burnTurns} turns.");
            }

            if (effect.extraTurns > 0)
            {
                // 获得额外回合
                Debug.Log($"Gained {effect.extraTurns} extra turns.");
            }

            if (effect.clearRow)
            {
                // 清除一行
                Debug.Log("Cleared one row of elements.");
            }

            if (effect.clearColumn)
            {
                // 清除一列
                Debug.Log("Cleared one column of elements.");
            }

            if (effect.clearAll)
            {
                // 清除所有相同类型的元素
                Debug.Log("Cleared all elements of the same type.");
            }
            // 其他效果
        }
    }
}