using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace GameLogic.MatchCore
{
    public class MazeGenerator : MonoBehaviour
    {
        [System.Serializable]
        public class Node
        {
            public Vector2Int position;
            public NodeType type;
            public Monster monster;
        }

        public enum NodeType
        {
            Monster,
            Campfire,
            Merchant,
            Chest,
            EliteMonster
        }

        [SerializeField] private Transform mazeContainer;
        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private List<GameObject> nodePrefabsByType;

        private List<Node> nodes = new List<Node>();

        public async UniTask GenerateMazeAsync(int difficulty)
        {
            // 生成迷宫节点
            ClearMaze();
            for (int i = 0; i < difficulty * 2; i++)
            {
                var nodeType = (NodeType)UnityEngine.Random.Range(0, 5);
                var newNode = new Node
                {
                    position = new Vector2Int(UnityEngine.Random.Range(0,  5), UnityEngine.Random.Range(0,  8)),
                    type = nodeType
                };
                nodes.Add(newNode);
            }

            // 生成节点对象
            foreach (var node in nodes)
            {
                var nodeObj = Instantiate(nodePrefabsByType[(int)node.type], mazeContainer);
                nodeObj.transform.localPosition = new Vector3(node.position.x, node.position.y, 0);
            }

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f)); // 等待节点生成动画
        }

        public void AddMonsterNode(Monster monster)
        {
            // 为怪物节点添加怪物
            var monsterNode = nodes.FirstOrDefault(n => n.type == NodeType.Monster && n.monster == null);
            if (monsterNode != null)
            {
                monsterNode.monster = monster;
            }
        }

        private void ClearMaze()
        {
            foreach (Transform child in mazeContainer)
            {
                Destroy(child.gameObject);
            }

            nodes.Clear();
        }
    }
}