using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.SummonStrategy
{
    public class NormalSetSummonStrategy : ISummonStrategy
    {
        public void Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, string mode)
        {
            player.PlayerField.setMonster(monsterCard);
            player.PlayerHand.removeCardFromHand(monsterCard);
            monsterCard.HasSwitchedMode = true;
            monsterCard.Mode = "Defence";
            monsterCard.FaceDown = true;
            player.MonsterSummoned = true;

        }
    }
}