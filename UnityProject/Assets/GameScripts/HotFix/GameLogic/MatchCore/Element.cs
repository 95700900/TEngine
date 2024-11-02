using UnityEngine;

namespace GameLogic.MatchCore
{
    using UnityEngine;

    public abstract class Element : MonoBehaviour
    {
        public Vector2Int position; // 元素在面板上的位置
        public Color color; // 元素的颜色
        public bool isLocked; // 是否锁定，锁定的元素不能被移动
        public ElementType elementType;

        public virtual void Setup(Vector2Int pos, Color col,ElementType eType)
        {
            position = pos;
            color = col;
            elementType = eType;
            GetComponent<SpriteRenderer>().color = col;
        }

        public virtual void DestroyElement()
        {
            // 可以在这里添加销毁动画
            Destroy(gameObject);
        }

        public virtual bool CanBeSwappedWith(Element other)
        {
            // 默认情况下，只有非锁定的元素才能被交换
            return !isLocked && !other.isLocked;
        }

        public virtual bool IsMatchableWith(Element other)
        {
            // 默认情况下，颜色相同的元素可以匹配
            return color == other.color;
        }
    }
}