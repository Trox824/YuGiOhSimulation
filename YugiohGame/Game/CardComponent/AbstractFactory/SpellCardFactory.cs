using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.CardComponent.AbstractFactory
{
    public class SpellCardFactory : AbstractCardFactory
    {
        public override CardObject CreateCard(string[] cardInformation)
        {
            switch (cardInformation[1])
            {
                case "Pot of Greed":
                    int cardId;
                    int.TryParse(cardInformation[0], out cardId);
                    return new PotOfGreed(cardId, cardInformation[1], cardInformation[2], cardInformation[3], cardInformation[4]);
                case "SpellCard":
                    return null;

                case "TrapCard":
                    return null;
            }
            return null;
        }
    }
}
