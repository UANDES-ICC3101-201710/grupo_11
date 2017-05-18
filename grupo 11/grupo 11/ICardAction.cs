using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    interface ICardAction
    {
        void IAttack(Minion attackedMinion);
        void IAttackPlayer(Player enemyPlayer);
        void IActivateAbility();
        void IShowCardData();
    }
}
