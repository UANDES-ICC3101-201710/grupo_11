using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Spell : Card
    {
        public string Ability { get; set; }

        public Spell(string Ability, int CardCost, string CardName , string CardRarity , string CardClass, int CardGamePosition) 
            : base(CardCost, CardName, CardRarity, CardClass, CardGamePosition)
        {
            this.Ability = Ability;
        }

        public void ActivateAbility( string Ability, Player player, Player enemyPlayer)
        {
            // Give1Mana
            if (Ability.Equals("Give1Mana"))
            {
                // Esto aumenta el mana que puede jugar este jugador por un turno, en uno.
                player.ActualMana = player.ActualMana + 1;
            }
        }
    }
}
