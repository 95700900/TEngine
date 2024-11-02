namespace GameLogic.MatchGame
{
    using UnityEngine;
    using DG.Tweening;
    using System.Threading.Tasks;

    public class FallManager : MonoBehaviour
    {
        public GridManager gridManager;

        public void FallElements()
        {
            for (int x = 0; x < gridManager.width; x++)
            {
                for (int y = 0; y < gridManager.height; y++)
                {
                    if (gridManager.grid[x, y] == null)
                    {
                        FallFromAbove(x, y);
                    }
                }
            }

            gridManager.matchManager.CheckForMatches();
        }

        private void FallFromAbove(int x, int y)
        {
            for (int i = y; i < gridManager.height; i++)
            {
                if (gridManager.grid[x, i] != null)
                {
                    gridManager.grid[x, i].transform.DOMoveY(i - 1, 0.2f);
                    gridManager.grid[x, i - 1] = gridManager.grid[x, i];
                    gridManager.grid[x, i] = null;
                }
            }

            int randomIndex = Random.Range(0, gridManager.elementPrefabs.Length);
            GameObject newElement = Instantiate(gridManager.elementPrefabs[randomIndex],
                new Vector3(x, gridManager.height - 1, 0), Quaternion.identity);
            gridManager.grid[x, gridManager.height - 1] = newElement;
            newElement.transform.localScale = Vector3.zero;
            newElement.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);
        }
    }
}