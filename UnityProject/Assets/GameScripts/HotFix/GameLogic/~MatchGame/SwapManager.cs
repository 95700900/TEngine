namespace GameLogic.MatchGame
{
    using UnityEngine;
    using DG.Tweening;
    using System.Threading.Tasks;

    public class SwapManager : MonoBehaviour
    {
        public GridManager gridManager;
        private GameObject selectedElement;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null)
                {
                    selectedElement = hit.collider.gameObject;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                if (hit.collider != null && hit.collider.gameObject != selectedElement)
                {
                    SwapElements(selectedElement, hit.collider.gameObject);
                }

                selectedElement = null;
            }
        }

        async void SwapElements(GameObject element1, GameObject element2)
        {
            Vector3 tempPosition = element1.transform.position;
            element1.transform.DOMove(element2.transform.position, 0.5f);
            element2.transform.DOMove(tempPosition, 0.5f);

            // 等待动画完成
            await Task.Delay(500);

            // 交换在grid中的位置
            (gridManager.grid[(int)element1.transform.position.x, (int)element1.transform.position.y],
                    gridManager.grid[(int)element2.transform.position.x, (int)element2.transform.position.y]) =
                (gridManager.grid[(int)element2.transform.position.x, (int)element2.transform.position.y],
                    gridManager.grid[(int)element1.transform.position.x, (int)element1.transform.position.y]);

            // 检查是否有匹配
            // 如果没有匹配，交换回去
            if (!gridManager.matchManager.CheckForMatches())
            {
                SwapElements(element1, element2);
            }
        }
    }
}