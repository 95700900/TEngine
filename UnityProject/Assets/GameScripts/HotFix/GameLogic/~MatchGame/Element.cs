namespace GameLogic.MatchGame
{
    using UnityEngine;

    public class Element : MonoBehaviour
    {
        // 标记元素是否需要被消除
        public bool isMarkedForDestruction = false;

        // 标记元素为可消除
        public void MarkForDestruction()
        {
            isMarkedForDestruction = true;
        }

        // 重置标记
        public void ResetMark()
        {
            isMarkedForDestruction = false;
        }
    }
}