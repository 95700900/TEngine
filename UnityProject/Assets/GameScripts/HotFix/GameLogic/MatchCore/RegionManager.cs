using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public class RegionManager : MonoBehaviour
    {
 
        [SerializeField] private List<Region> regions;
        [SerializeField] private Transform regionMap;
        [SerializeField] private GameObject regionButtonPrefab;
        [SerializeField] private MazeGenerator mazeGenerator;
        [SerializeField] private List<Monster> monstersPool; // 怪物池

        private int currentRegionIndex = 0;

        private void Start()
        {
            InitializeRegionButtons();
        }

        private void InitializeRegionButtons()
        {
            foreach (var region in regions)
            {
                var button = Instantiate(regionButtonPrefab, regionMap);
                button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnRegionSelected(region));
                button.GetComponentInChildren<UnityEngine.UI.Text>().text = region.name;
            }
        }

        private void OnRegionSelected(Region selectedRegion)
        {
            currentRegionIndex = regions.IndexOf(selectedRegion);
            LoadRegion(selectedRegion).Forget();
        }

        private async UniTask LoadRegion(Region region)
        {
            // 加载地区背景
            Camera.main.GetComponent<SpriteRenderer>().sprite = region.background;

            // 初始化迷宫地图
            await mazeGenerator.GenerateMazeAsync(region.difficulty);

            // 根据地区难度生成怪物
            var monsters = monstersPool.OrderBy(m => UnityEngine.Random.value).Take(region.difficulty * 2).ToList();
            foreach (var monster in monsters)
            {
                // 生成怪物节点
                mazeGenerator.AddMonsterNode(monster);
            }

            // 显示地区信息
            Debug.Log($"Loaded Region: {region.name} with Difficulty: {region.difficulty}");
        }
    }
}