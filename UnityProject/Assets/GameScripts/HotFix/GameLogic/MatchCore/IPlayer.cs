using UnityEngine;

namespace GameLogic.MatchCore
{
    public interface IPlayer : ICharacter
    {
        string Name { get; }
        int Energy { get; set; }
        int MaxEnergy { get; }
    
        void Attack(ICharacter target);
    }
}