using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.MatchGame
{
    public class MatchManager : MonoBehaviour
    {
        public GridManager gridManager;
        public FallManager fallManager; // 添加这个引用

        public bool  CheckForMatches()
        {
            List<GameObject> matches = new List<GameObject>();

            for (int x = 0; x < gridManager.width; x++)
            {
                for (int y = 0; y < gridManager.height; y++)
                {
                    GameObject currentElement = gridManager.grid[x, y];
                    if (currentElement != null)
                    {
                        // 检查水平匹配
                        if (x < gridManager.width - 2)
                        {
                            GameObject element1 = gridManager.grid[x + 1, y];
                            GameObject element2 = gridManager.grid[x + 2, y];
                            if (element1 != null && element2 != null && currentElement.tag == element1.tag &&
                                currentElement.tag == element2.tag)
                            {
                                matches.Add(currentElement);
                                matches.Add(element1);
                                matches.Add(element2);
                            }
                        }

                        // 检查垂直匹配
                        if (y < gridManager.height - 2)
                        {
                            GameObject element1 = gridManager.grid[x, y + 1];
                            GameObject element2 = gridManager.grid[x, y + 2];
                            if (element1 != null && element2 != null && currentElement.tag == element1.tag &&
                                currentElement.tag == element2.tag)
                            {
                                matches.Add(currentElement);
                                matches.Add(element1);
                                matches.Add(element2);
                            }
                        }
                    }
                }
            }

            if (matches.Count > 0)
            {
                foreach (GameObject match in matches)
                {
                    match.GetComponent<Element>().MarkForDestruction();
                }

                fallManager.FallElements(); // 在这里调用 FallElements 方法
                return true;
            }

            return false;
        }
    }
}