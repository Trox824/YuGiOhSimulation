namespace YugiohGame.Game.CardComponent.Card
{
    public abstract class EffectMonsterCard : MonsterCard, IHaveEffect
    {
        public EffectMonsterCard(int id, string name, string CardTypes, string desc, int lvl, int atk, int def, string attribute) : base(id, name, CardTypes, desc, lvl, atk, def, attribute)
        {
        }
        public abstract bool CheckCondition(GameMechanic game);
        public abstract void ExecuteEffect(GameMechanic game);
    }
}
