using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.CardComponent.AbstractFactory
{
    public abstract class AbstractCardFactory : ICardFactory
    {
        public abstract CardObject CreateCard(string[] MonsterInformation);


    }
}
