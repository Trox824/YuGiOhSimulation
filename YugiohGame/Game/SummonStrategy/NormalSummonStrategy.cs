using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.SummonStrategy
{
    public class NormalSummonStrategy : ISummonStrategy
    {
        public void Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, string mode)
        {
            player.PlayerField.setMonster(monsterCard);
            player.PlayerHand.removeCardFromHand(monsterCard);
            monsterCard.HasSwitchedMode = true;
            player.MonsterSummoned = true;
        }
    }
}