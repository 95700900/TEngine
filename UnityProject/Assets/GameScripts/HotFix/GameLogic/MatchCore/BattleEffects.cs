using DG.Tweening;
using UnityEngine;

namespace GameLogic.MatchCore
{
    using DG.Tweening;
    using UnityEngine;

    public class BattleEffects : MonoBehaviour
    {
        private static BattleEffects instance;

        // 单例模式
        public static BattleEffects Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BattleEffects>();
                    if (instance == null)
                    {
                        var obj = new GameObject("BattleEffects");
                        instance = obj.AddComponent<BattleEffects>();
                    }
                }
                return instance;
            }
        }

        // 播放攻击效果
        public void PlayAttackEffect(Transform attacker, Transform target)
        {
            // 假设你有一个攻击效果的预制体
            GameObject attackEffect = Resources.Load<GameObject>("Prefabs/AttackEffect");
            if (attackEffect != null)
            {
                GameObject effectInstance = Instantiate(attackEffect, target.position, Quaternion.identity);
                // 播放动画或效果
                // 例如，你可以在这里添加一些动画或粒子效果
            }
        }

        // 播放燃烧效果
        public void PlayBurnEffect(Transform target)
        {
            // 假设你有一个燃烧效果的预制体
            GameObject burnEffect = Resources.Load<GameObject>("Prefabs/BurnEffect");
            if (burnEffect != null)
            {
                GameObject effectInstance = Instantiate(burnEffect, target.position, Quaternion.identity);

                // // 淡出效果
                // effectInstance.GetComponent<SpriteRenderer>().DOFade(0, 1.0f).OnComplete(() =>
                // {
                //     Destroy(effectInstance);
                // });
                Destroy(effectInstance);
            }
        }

        // 其他效果方法...
    }
}