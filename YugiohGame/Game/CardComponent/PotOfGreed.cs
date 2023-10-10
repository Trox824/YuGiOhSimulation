using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YugiohGame.Game.CardComponent.Card;

namespace YugiohGame.Game.CardComponent
{
    public class PotOfGreed : SpellCard
    {
        public PotOfGreed(int id, string name, string CardTypes, string desc, string attribute) : base(id, name, CardTypes, desc, attribute)
        { 
        }
        public override bool CheckCondition(GameMechanic game)
        {
            if (game.Player.PlayerField.Phase == "Main Phase 1" || game.Player.PlayerField.Phase == "Main Phase 2")
                if (game.Player.PlayerHand.HandCards.Contains(game.ChoosenCard) || game.Player.PlayerField.getSpells.Contains(game.ChoosenCard))
                    return true;
            return false;
        }
        public override void ExecuteEffect(GameMechanic game)
        {
            if(CheckCondition(game)) 
            {
                game.Player.DrawCard();
                game.Player.DrawCard();
            }
        }
    }
}
