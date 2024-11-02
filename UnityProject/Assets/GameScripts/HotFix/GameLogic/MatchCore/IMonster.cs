using System.Collections.Generic;

namespace GameLogic.MatchCore
{
    public interface IMonster : ICharacter
    {
        string Name { get; }
        ElementType Weakness { get; }
        List<ElementType> Resistances { get; }
        List<MonsterAbility> Abilities { get; }

        void PerformAction(ICharacter target);
    }
}