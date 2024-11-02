namespace GameLogic.MatchCore
{
    public class Player : CharacterBase, IPlayer
    {
        public string Name { get; private set; } = "Player";
        public int Energy { get; set; }
        public int MaxEnergy { get; private set; }

        public void Initialize()
        {
            base.Initialize();
            Energy = MaxEnergy;
        }

        public void Attack(ICharacter target)
        {
            target.TakeDamage(target.Attack);
        }
        
     
    }
}