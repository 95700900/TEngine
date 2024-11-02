
namespace GameLogic.MatchGame
{
    using UnityEngine;
    using DG.Tweening;
    using System.Threading.Tasks;

    public class GridManager : MonoBehaviour
    {
        public MatchManager matchManager;
        public int width = 8;
        public int height = 8;
        public GameObject[] elementPrefabs;
        public GameObject[,] grid;

        void Start()
        {
            grid = new GameObject[width, height];
            GenerateGrid();
        }

        private void GenerateGrid()
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int randomIndex = Random.Range(0, elementPrefabs.Length);
                    GameObject element = Instantiate(elementPrefabs[randomIndex], new Vector3(x, y, 0),
                        Quaternion.identity);
                    grid[x, y] = element;
                    element.transform.localScale = Vector3.zero;
                    element.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);
                }
            }
        }
    }
}