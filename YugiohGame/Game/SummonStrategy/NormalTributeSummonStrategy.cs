using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.SummonStrategy
{
    public class NormalTributeSummonStrategy : ISummonStrategy
    {
        public void Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, string mode)
        {
            for (int i = 0; i < tributeMonsters.Count; i++)
            {
                player.PlayerField.RemoveMonster(tributeMonsters[i]);
                player.PlayerField.AddToGraveyard(tributeMonsters[i]);
            }
            monsterCard.HasSwitchedMode = true;
            player.PlayerField.setMonster(monsterCard);
            monsterCard.Mode = "Attack";
            player.MonsterSummoned = true;

        }
    }
}
