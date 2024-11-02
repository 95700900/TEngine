using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.MatchCore
{
    public abstract class CharacterBase : MonoBehaviour, ICharacter
    {
        public int Health { get; set; }
        public int MaxHealth { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int ShieldValue { get; set; }
        public bool IsStunned { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsAlive { get; set; }

        public List<CombatState> CombatStates { get; private set; }

        public virtual void Initialize()
        {
            Health = MaxHealth;
            CombatStates = new List<CombatState>();
        }

        public virtual void TakeDamage(int amount)
        {
            if (ShieldValue > 0)
            {
                ShieldValue -= amount;
                if (ShieldValue < 0)
                {
                    amount = -ShieldValue;
                    ShieldValue = 0;
                }
                else
                {
                    amount = 0;
                }
            }

            Health -= Mathf.Max(0, amount - Defense);
            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void Heal(int amount)
        {
            Health = Mathf.Min(MaxHealth, Health + amount);
        }
 
        public void RemoveExpiredStates()
        {
            CombatStates.RemoveAll(state => state.IsExpired());
        }

        public void ApplyCombatStates()
        {
            foreach (var state in CombatStates)
            {
                state.ApplyEffect(this);
            }
        }

        public void UpdateCombatStates()
        {
            foreach (var state in CombatStates)
            {
                state.DecrementDuration();
            }

            RemoveExpiredStates();
            ApplyCombatStates();
        }
        
        public virtual void Die()
        {
            // 死亡逻辑
            Debug.Log($"{name} has died.");
            CombatEvent.OnCharacterDied.Invoke(this);
        }

        public void AddCombatState(CombatState state)
        {
            CombatStates.Add(state);
            CombatEvent.OnCombatStateChanged.Invoke(this, state);
        }
    }
}