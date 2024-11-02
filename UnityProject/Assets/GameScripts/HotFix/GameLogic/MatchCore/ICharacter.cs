using System.Collections.Generic;
using UnityEngine;

namespace GameLogic.MatchCore
{
    public interface ICharacter
    {
        int Health { get; set; }
        int MaxHealth { get; }
        int Attack { get; }
        int Defense { get; }
        int ShieldValue { get; set; }
        bool IsStunned { get; set; }
        bool IsFrozen { get; set; }
        bool IsAlive { get; set; }
        List<CombatState> CombatStates { get; }

        void Initialize();
        void TakeDamage(int amount);
        void Heal(int amount);
        void Die();
        void AddCombatState(CombatState state);
        void RemoveExpiredStates();
        void ApplyCombatStates();
        void UpdateCombatStates();
    }
}