using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Card : ICardAction
    {
        protected int CardCost;
        protected string CardName;
        protected string CardRarity;
        protected string CardClass;
        protected int CardGamePosition;

        public int getCardCost() { return this.CardCost; }
        public Card(int CardCost, string CardName, string CardRarity, string CardClass, int CardGamePosition)
        {
            this.CardCost = CardCost;
            this.CardName = CardName;
            this.CardRarity = CardRarity;
            this.CardClass = CardClass;
            this.CardGamePosition = CardGamePosition;
        }
        public virtual void IActivateAbility()
        {
            throw new NotImplementedException();
        }
        public virtual void IAttack(Minion AttackedMinion)
        {
            
        }
         public virtual string getCardName()
        {
            return this.CardName;
        }
        public void IShowCardData()
        {
            Console.WriteLine("{0}", this.CardName);
        }
        public virtual void IAttackPlayer(Player enemyPlayer) { }
    }
}
