using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.SummonStrategy
{
    public interface ISummonStrategy
    {
        void Summon(MonsterCard monsterCard, List<MonsterCard> tributeMonsters, Player player, string mode);
    }
}
