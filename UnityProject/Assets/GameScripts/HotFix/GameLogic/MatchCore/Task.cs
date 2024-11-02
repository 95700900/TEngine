using System.Collections.Generic;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class Task
    {
        public string description;
        public TaskType type;
        public int requiredCount;
        public int reward;
        public bool isCompleted;
        public List<ElementType> targetElements; // 新增：目标元素
        public int timeLimit; // 新增：时间限制（秒）
        public int consecutiveCount; // 新增：连续次数要求
    }

  
    public enum TaskType
    {
        EliminateMonsters,
        OpenChests,
        UseProps,
        ClearRows,
        ClearColumns,
        CollectElements, // 新增：收集特定元素
        SurviveTime, // 新增：在限定时间内生存
        ConsecutiveClear, // 新增：连续清除
    }
}