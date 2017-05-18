using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Weapon
    {
        private int Attack, MaxUses, RemainingUses;
        private string WeaponEffect;

        public Weapon(int Attack , int MaxUses, int RemainingUses, string WeaponEffect)
        {
            this.Attack = Attack;
            this.MaxUses = MaxUses;
            this.RemainingUses = RemainingUses;
            this.WeaponEffect = WeaponEffect;
        }
        public void ActivateWeaponEffect(string effect)
        {

        }

        public void SetWeaponEffect(Card minionInField)
        {

        }
    }
}
