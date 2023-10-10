using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.CardComponent.AbstractFactory
{
    public class MonsterCardFactory : AbstractCardFactory
    {
        public override CardObject CreateCard(string[] MonsterInformation)
        {
            switch (MonsterInformation[2])
            {
                case "NormalMonster":
                    int cardId;
                    int.TryParse(MonsterInformation[0], out cardId);

                    int level;
                    int.TryParse(MonsterInformation[4], out level);

                    int attack;
                    int.TryParse(MonsterInformation[5], out attack);

                    int defence;
                    int.TryParse(MonsterInformation[6], out defence);
                    return new NormalMonsterCard(cardId, MonsterInformation[1], MonsterInformation[2], MonsterInformation[3], level, attack, defence, MonsterInformation[7]);
                case "SpellCard":
                    return null;

                case "TrapCard":
                    return null;
            }
            return null;
        }
    }
}
