using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace GameLogic.MatchCore
{

    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Text regionNameText;
        [SerializeField] private Text taskDescriptionText;
        [SerializeField] private Button[] skillButtons;
        [SerializeField] private Button[] propButtons;
        [SerializeField] private Text taskProgressText;
        [SerializeField] private Text rewardText;

        private TaskManager taskManager;
        private SkillManager skillManager;
        private PropManager propManager;

        private void Start()
        {
            taskManager = FindObjectOfType<TaskManager>();
            skillManager = FindObjectOfType<SkillManager>();
            propManager = FindObjectOfType<PropManager>();

            // 初始化UI
            UpdateRegionName("初始地区");
            UpdateTaskDescription("完成10次消除");
            UpdateSkillButtons();
            UpdatePropButtons();
        }

        public void UpdateRegionName(string name)
        {
            regionNameText.text = name;
        }

        public void UpdateTaskDescription(string description)
        {
            taskDescriptionText.text = description;
        }

        public void UpdateTaskProgress(Task task)
        {
            taskProgressText.text = $"{task.requiredCount} / {task.reward}";
        }

        public void UpdateReward(int reward)
        {
            rewardText.text = $"Reward: {reward}";
        }

        private void UpdateSkillButtons()
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                if (i < skillManager.skills.Count)
                {
                    var skill = skillManager.skills[i];
                    skillButtons[i].GetComponentInChildren<Text>().text = skill.Name;
                    skillButtons[i].onClick.AddListener(() => UseSkill(skill));
                }
                else
                {
                    skillButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void UpdatePropButtons()
        {
            for (int i = 0; i < propButtons.Length; i++)
            {
                if (i < propManager.props.Count)
                {
                    var prop = propManager.props[i];
                    propButtons[i].GetComponentInChildren<Text>().text = prop.name;
                    propButtons[i].onClick.AddListener(() => UseProp(prop));
                }
                else
                {
                    propButtons[i].gameObject.SetActive(false);
                }
            }
        }

        private void UseSkill(Skill skill)
        {
            // 使用技能
            skillManager.UseSkill(skill, ElementType.All);
        }

        private void UseProp(Prop prop)
        {
            // 使用道具
            propManager.UseProp(prop, ElementType.All);
        }
    }
}