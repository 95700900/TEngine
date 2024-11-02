using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.MatchCore
{
    [System.Serializable]
    public class Monster : CharacterBase, IMonster
    {
      
        public string name;
        public int health;
        public int attack;
        public ElementType weakness;
      
        public string Name { get; private set; }
        public ElementType Weakness { get; private set; }
        public List<ElementType> Resistances { get; private set; }
        public List<MonsterAbility> Abilities { get; private set; }

        public void Initialize()
        {
            base.Initialize();
        }

        public void PerformAction(ICharacter target)
        {
            if (Abilities.Count > 0 && UnityEngine.Random.value < 0.5f)
            {
                var ability = Abilities[UnityEngine.Random.Range(0, Abilities.Count)];
                ability.Perform(target);
            }
            else
            {
                Attack(target);
            }
        }

        public void Attack(ICharacter target)
        {
            target.TakeDamage(target.Attack);
        }
    }
}