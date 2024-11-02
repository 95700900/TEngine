using System.Collections.Generic;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class SkillEffect
    {
        public ElementType targetType;
        public int damage;
        public int freezeTurns;
        public int burnTurns;
        public int extraTurns;
        public bool clearRow;
        public bool clearColumn;
        public bool clearAll;
        public List<ElementType> convertTo; // 新增：转换为其他元素
        public bool shieldPlayer; // 新增：给玩家一个护盾
        
    }
}