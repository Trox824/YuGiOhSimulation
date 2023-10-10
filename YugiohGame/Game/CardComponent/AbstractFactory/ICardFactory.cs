using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.CardComponent.AbstractFactory
{
    internal interface ICardFactory
    {
        CardObject CreateCard(string[] MonsterInformation);
    }
}