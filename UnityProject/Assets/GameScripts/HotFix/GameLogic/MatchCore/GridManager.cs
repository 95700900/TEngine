using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace GameLogic.MatchCore
{
    public class GridManager : MonoBehaviour
    {
        [SerializeField] private Element[] elementPrefabs; // 元素预制体数组
        [SerializeField] private int width = 7, height = 8; // 游戏面板大小
        [SerializeField] private float fallDuration = 0.5f; // 下落动画持续时间
        [SerializeField] private float refillDelay = 0.1f; // 重填延迟
        [SerializeField] private int defaultIceBlocks = 2; // 默认生成的冰块数量
        private Element[,] elements; // 存储元素的二维数组

        public async UniTask InitializeGridAsync()
        {
            elements = new Element[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var elementPrefab = GetRandomElementPrefab();
                    var element = Instantiate(elementPrefab, transform);
                    element.transform.localPosition = new Vector3(x, y, 0) * 1.0f; // 假设每个元素间距为1单位
                    element.Setup(new Vector2Int(x, y), GetRandomColor(),
                        elementPrefab.GetComponent<Element>().elementType);
                    elements[x, y] = element;
                }
            }

            // 生成默认数量的冰块
            GenerateIceBlocks(defaultIceBlocks);

            await CheckMatchesAndClearAsync();
        }

        private Element GetRandomElementPrefab()
        {
            // 生成随机元素，可以调整概率
            if (UnityEngine.Random.value < 0.1f) // 10% 的几率生成冰块
            {
                return elementPrefabs[1]; // 假设第二个预制体是冰块
            }

            return elementPrefabs[0]; // 第一个预制体是普通元素
        }

        /// <summary>
        /// 生成指定数量的冰块，并将其放置在随机位置。
        /// </summary>
        /// <param name="count">要生成的冰块数量。</param>
        private void GenerateIceBlocks(int count)
        {
            var availablePositions = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    availablePositions.Add(new Vector2Int(x, y));
                }
            }

            for (int i = 0; i < count; i++)
            {
                if (availablePositions.Count == 0)
                {
                    break; // 没有可用的位置了
                }

                int index = UnityEngine.Random.Range(0, availablePositions.Count);
                var position = availablePositions[index];
                availablePositions.RemoveAt(index);

                // 替换为冰块
                var iceElement = Instantiate(elementPrefabs[1], transform);
                iceElement.transform.localPosition = new Vector3(position.x, position.y, 0) * 1.0f;
                iceElement.Setup(position, GetRandomColor(), ElementType.Ice);
                elements[position.x, position.y] = iceElement;
            }
        }

        public async UniTask<bool> TrySwapElements(Vector2Int from, Vector2Int to)
        {
            // 交换两个元素的位置
            if (elements[from.x, from.y]?.CanBeSwappedWith(elements[to.x, to.y]) ?? false)
            {
                (elements[from.x, from.y], elements[to.x, to.y]) = (elements[to.x, to.y], elements[from.x, from.y]);
                (elements[from.x, from.y].position, elements[to.x, to.y].position) = (to, from);

                // 移动元素到新位置
                elements[from.x, from.y].transform.DOMove(elements[to.x, to.y].transform.position, fallDuration)
                    .SetEase(Ease.InOutQuad);
                elements[to.x, to.y].transform.DOMove(elements[from.x, from.y].transform.position, fallDuration)
                    .SetEase(Ease.InOutQuad);

                // 检查是否有匹配
                var hasMatch = await CheckMatchesAndClearAsync();

                if (!hasMatch)
                {
                    // 如果没有匹配，则恢复原始状态
                    (elements[from.x, from.y], elements[to.x, to.y]) = (elements[to.x, to.y], elements[from.x, from.y]);
                    (elements[from.x, from.y].position, elements[to.x, to.y].position) = (from, to);

                    elements[from.x, from.y].transform.DOMove(elements[from.x, from.y].transform.position, fallDuration)
                        .SetEase(Ease.InOutQuad);
                    elements[to.x, to.y].transform.DOMove(elements[to.x, to.y].transform.position, fallDuration)
                        .SetEase(Ease.InOutQuad);
                }

                return hasMatch;
            }

            return false;
        }

        private async UniTask<bool> CheckMatchesAndClearAsync()
        {
            List<Vector2Int> matches = FindAllMatches();
            if (matches.Count == 0)
            {
                return false;
            }

            // 消除匹配的元素
            foreach (var match in matches)
            {
                if (elements[match.x, match.y] is IceElement ice)
                {
                    ice.DestroyElement();
                }
                else
                {
                    elements[match.x, match.y].DestroyElement();
                }

                elements[match.x, match.y] = null;
            }

            await UniTask.Delay(TimeSpan.FromSeconds(refillDelay));

            // 重新填充网格
            await RefillGridAsync();

            return true;
        }

        private List<Vector2Int> FindAllMatches()
        {
            List<Vector2Int> allMatches = new List<Vector2Int>();

            // 检查水平匹配
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height - 2; y++)
                {
                    if (elements[x, y] != null && elements[x, y + 1] != null && elements[x, y + 2] != null &&
                        elements[x, y].IsMatchableWith(elements[x, y + 1]) &&
                        elements[x, y + 1].IsMatchableWith(elements[x, y + 2]))
                    {
                        allMatches.Add(new Vector2Int(x, y));
                        allMatches.Add(new Vector2Int(x, y + 1));
                        allMatches.Add(new Vector2Int(x, y + 2));
                    }
                }
            }

            // 检查垂直匹配
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width - 2; x++)
                {
                    if (elements[x, y] != null && elements[x + 1, y] != null && elements[x + 2, y] != null &&
                        elements[x, y].IsMatchableWith(elements[x + 1, y]) &&
                        elements[x + 1, y].IsMatchableWith(elements[x + 2, y]))
                    {
                        allMatches.Add(new Vector2Int(x, y));
                        allMatches.Add(new Vector2Int(x + 1, y));
                        allMatches.Add(new Vector2Int(x + 2, y));
                    }
                }
            }

            return allMatches.Distinct().ToList(); // 确保匹配点唯一
        }

        private async UniTask RefillGridAsync()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (elements[x, y] == null)
                    {
                        for (int aboveY = y - 1; aboveY >= 0; aboveY--)
                        {
                            if (elements[x, aboveY] != null)
                            {
                                // 将上面的元素下移到当前位置
                                elements[x, aboveY].transform.DOMove(elements[x, y].transform.position, fallDuration)
                                    .SetEase(Ease.InOutQuad);
                                elements[x, y] = elements[x, aboveY];
                                elements[x, aboveY] = null;
                                break;
                            }
                        }

                        if (elements[x, y] == null)
                        {
                            // 如果上方没有元素，则生成新的元素
                            var newElement = Instantiate(GetRandomElementPrefab(), transform);
                            newElement.transform.localPosition = new Vector3(x, y + 1, 0) * 1.0f;
                            newElement.Setup(new Vector2Int(x, y), GetRandomColor(),
                                newElement.GetComponent<Element>().elementType);
                            elements[x, y] = newElement;

                            newElement.transform.DOMove(elements[x, y].transform.position, fallDuration)
                                .SetEase(Ease.InOutQuad);
                        }
                    }
                }
            }

            await UniTask.Delay(TimeSpan.FromSeconds(fallDuration + 0.1f)); // 等待所有元素下落完成
        }

        private Color GetRandomColor() => Random.ColorHSV(0f,  1f, 1f, 1f, 0.5f, 1f); // 随机颜色
    }
}