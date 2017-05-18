using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    interface IPlayerAction
    {
        int IChooseAction();
        void IAttackOponentPlayer(Player enemyplayer);
        void IAttackOponentMinion(Minion enemyMinion);
        void IActivateHeroAbility(Player enemyPlayer);
        void IPlayCardFromHand(int IndexOfSelectedCard, Player enemyPlayer);
    }
}
