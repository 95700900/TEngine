namespace GameLogic.MatchCore
{
    using UnityEngine.Events;

    public static class CombatEvent
    {
        public static UnityEvent<ICharacter, CombatState> OnCombatStateChanged = new UnityEvent<ICharacter, CombatState>();
        public static UnityEvent<ICharacter> OnCharacterDied = new UnityEvent<ICharacter>();
    }
}