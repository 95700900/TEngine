using UnityEngine;

namespace GameLogic.MatchCore
{
    public class IceElement : Element
    {
        public int requiredHits; // 消除冰块所需的命中次数
        private int hitCount; // 当前命中次数

        public override void Setup(Vector2Int pos, Color col,ElementType eType)
        {
            base.Setup(pos, col,eType);
            requiredHits = 3; // 示例：需要3次命中才能消除
            hitCount = 0;
        }

        public override void DestroyElement()
        {
            // 冰块需要多次命中才能消除
            hitCount++;
            if (hitCount >= requiredHits)
            {
                base.DestroyElement();
            }
        }

        public override bool CanBeSwappedWith(Element other)
        {
            // 冰块不能被移动
            return false;
        }

        public override bool IsMatchableWith(Element other)
        {
            // 冰块不能直接匹配
            return false;
        }
    }
}