namespace YugiohGame.Game.CardComponent.Card
{
    public class NormalMonsterCard : MonsterCard
    {
        public override int Attack { get => base.Attack * 2; set => base.Attack = value; }
        public override int Defence { get => base.Defence * 2; set => base.Defence = value; }
        public NormalMonsterCard(int id, string name, string CardTypes, string desc, int lvl, int atk, int def, string attribute) : base(id, name, CardTypes, desc, lvl, atk, def, attribute)
        {
        }
    }
}
