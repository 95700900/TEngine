using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public class PropManager : MonoBehaviour
    {
    
        [SerializeField] public List<Prop> props;

        public void UseProp(Prop prop, ElementType target)
        {
            if (prop.effect.targetType == target || prop.effect.targetType == ElementType.All)
            {
                // 应用道具效果
                ApplyPropEffect(prop.effect);
            }
        }

        private void ApplyPropEffect(PropEffect effect)
        {
            // 根据效果类型应用效果
            if (effect.damage > 0)
            {
                // 造成伤害
                Debug.Log($"Caused {effect.damage} damage to the target.");
            }

            if (effect.heal > 0)
            {
                // 恢复生命
                Debug.Log($"Healed {effect.heal} points.");
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