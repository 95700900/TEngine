using UnityEngine;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class Prop
    {
        public string name;
        public Sprite icon;
        public PropEffect effect;
        public int rarity; // 新增：稀有度
        public int quantity; // 新增：数量
    }
}