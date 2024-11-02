using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace GameLogic.MatchCore
{
    public class TaskManager : MonoBehaviour
    {
        [System.Serializable]
        public class Task
        {
            public string description;
            public TaskType type;
            public int requiredCount;
            public int reward;
            public bool isCompleted;
        }

        [System.Serializable]
        public enum TaskType
        {
            EliminateMonsters,
            OpenChests,
            UseProps
        }

        [SerializeField] private List<Task> dailyTasks;
        [SerializeField] private List<Task> mainTasks;

        public void CompleteTask(Task task, int count)
        {
            task.requiredCount -= count;
            if (task.requiredCount <= 0)
            {
                task.isCompleted = true;
                // 发放奖励
                GrantReward(task.reward);
            }
        }

        private void GrantReward(int reward)
        {
            // 发放奖励
            Debug.Log($"Granted Reward: {reward}");
        }
    }
}