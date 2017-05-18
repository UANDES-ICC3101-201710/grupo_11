using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    interface IPlayCard
    {
        Minion IPlayCardToField(int indexOfCard);

        Spell IPlaySpell(int indexOfCard);
    }
}
