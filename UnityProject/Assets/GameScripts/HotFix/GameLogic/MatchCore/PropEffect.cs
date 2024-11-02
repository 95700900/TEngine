using System.Collections.Generic;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class PropEffect
    {
        public ElementType targetType;
        public int damage;
        public int heal;
        public int extraTurns;
        public bool clearRow;
        public bool clearColumn;
        public bool clearAll;
        public List<ElementType> convertTo; // 新增：转换为其他元素
        public bool revivePlayer; // 新增：复活玩家
    }
}