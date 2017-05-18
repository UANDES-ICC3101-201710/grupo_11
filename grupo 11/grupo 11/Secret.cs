using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Secret
    {
        private string Ability;

        public Secret(string Ability)
        {
            this.Ability = Ability;
        }

        public Boolean CheckCondition(String action, String Condition)
        {
            return true;
        }

        public void ActivateAbility(String Ability)
        {
            this.Ability = Ability;
        }
    }
}
