using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YugiohGame.Game.CardComponent
{
    public interface IHaveEffect
    {
        bool CheckCondition(GameMechanic game);
        void ExecuteEffect(GameMechanic game);
    }
}
