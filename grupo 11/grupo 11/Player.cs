using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Player : IPlayerAction
    {
        public int MaxLifePoints = 30;
        public int ActualLifePoints;
        public int DefensePoints;
        public int MaxMana = 10;
        public int MaxManaTurn;
        public int ActualMana;
        public int ActualTurn;
        public int HeroName;
        public int HeroAttackPoints;
        public int coinFlip;

        public string PlayerID;

        public Boolean PlayerTurnEnable;
        public Boolean HeropowerEnable;
        public Boolean HeroAttackEnable;
        public Boolean FatigueEnable;
        public int TurnOfFatigue = 0;

        public Hand myhand;
        public Deck myDeck;
        public Field myField;

        public Player(string ID, int heroName, string dirMazo, int coinflip)
        {
            this.PlayerID = ID;
            this.HeroName = heroName;
            this.ActualLifePoints = 30;
            this.ActualMana = 0;
            this.MaxMana = 10;
            this.MaxManaTurn = 0;
            this.ActualTurn = 0;
            this.HeroAttackPoints = 0;
            this.DefensePoints = 0;
            this.coinFlip = coinflip;
            this.myDeck = new Deck(dirMazo);
            this.myDeck.ShuffleDeck();
            this.myhand = new Hand(this.myDeck, coinflip);
            this.myField = new Field();
            this.PlayerTurnEnable = true;
            this.HeropowerEnable = true;
            this.HeroAttackEnable = true;
        }


        public void TurnStart()
        {
            this.PlayerTurnEnable = true;
            // Al empezar el turno, debemos hacer una lista de cosas primero.
            // 1. El contador de turno aumenta en uno
            this.ActualTurn++;
            // 2. El mana del jugador aumenta en uno (Hasta que llegue a 10)
            if (this.MaxManaTurn < this.MaxMana)
            {
                this.MaxManaTurn++;
                this.ActualMana = this.MaxManaTurn;
            }
            // 3. Todos los Minions tienen permiso de atacar este turno.
            foreach (Minion minion in myField.MinionList)
            {
                minion.AttackEnable = true;
            }
            // El Hero tambien puede usar su poder.
            this.HeropowerEnable = true;
            // 4. Sacamos ua carta
            this.Fatigue();
            this.DrawCardFromDeck(this.FatigueEnable);
            // Con esto, concluye el principio del turno de un jugador.

        }
        public void TurnEnd()
        {
            this.PlayerTurnEnable = false;
        }
        public void IActivateHeroAbility(Player enemyPlayer)
        {
            // método que activa el poder del Heroe.
            // Segun el indice que tenga en su HeroName, es la habilidad que tendra:
            // 1. Warrior: Gain 2 of Armor.
            // 2. Hunter: Deal 2 of Damage to the EnemyHero.
            // ...more Classes Coming Soon...

            int heroIndex = this.HeroName;
            if (this.HeropowerEnable)
            {
                // Primero verificamos si tenemos suficiente Mana para activar la abilidad.
                if (this.ActualMana >= 2)
                {
                    this.HeropowerEnable = false;
                    switch (heroIndex)
                    {
                        case 1:
                            this.UseMana(2);
                            this.ChangeDefensePoints(2);
                            break;
                        case 2:
                            this.UseMana(2);
                            int Arrow = 2; // Le puse arrow, ya que la habilidad lanza una flecha al oponente.
                            enemyPlayer.RecibeDamage(Arrow);
                            break;

                    }
                }
            }
            else
            {
                Console.WriteLine("No es posible usar La Abilidad del Heroe");
                Console.ReadLine();
            }
        }
        public void Fatigue()
        {
            int lengthOfDeck = this.myDeck.DeckCards.Count;
            if(lengthOfDeck == 0)
            {
                this.FatigueEnable = true;
                this.TurnOfFatigue++;
            }
        }
        public void DrawCardFromDeck(Boolean fatigue)
        {
            int lengthOfDeck = this.myDeck.DeckCards.Count;
            if(this.FatigueEnable == true)
            {
                // No podemos sacar carta, ya que no hay en el mazo.
                // Perdemos vida.
                int DamageFromFatigue = this.TurnOfFatigue;
                this.RecibeDamage(DamageFromFatigue);
            }
            else
            {
                // Todavia quedan cartas en el mazo, podemos sacar carta.
                this.myhand.DrawCard(myDeck);
            }

        }
        public void IAttackOponentPlayer(Player enemyPlayer)
        {
            // Solo podemos atacar si nuestro heroe tiene puntos de ataque.
            if(this.HeroAttackPoints > 0)
            {
                // Mostramos al usuario quien ataca a quien:
                Console.WriteLine(" '{0}'[A:{1},D{2},L:{3}] a atacado a '{4}'[A:{5},D:{6},L:{7}]",
                        this.PlayerID, this.HeroAttackPoints,this.DefensePoints, this.ActualLifePoints,
                        enemyPlayer.PlayerID, enemyPlayer.HeroAttackPoints, enemyPlayer.DefensePoints,
                        enemyPlayer.ActualLifePoints);

                // El jugador atacado recibe el daño.
                enemyPlayer.RecibeDamage(this.HeroAttackPoints);
                // El jugador que ataca tambien recibe daño
                this.RecibeDamage(enemyPlayer.HeroAttackPoints);


                // Mostramos al usuario el resultado del ataque:
                Console.WriteLine("Resultado");
                Console.WriteLine(" '{0}'[A:{1},D{2},L:{3}] ; '{4}'[A:{5},D:{6},L:{7}]",
                        this.PlayerID, this.HeroAttackPoints, this.DefensePoints, this.ActualLifePoints,
                        enemyPlayer.PlayerID, enemyPlayer.HeroAttackPoints, enemyPlayer.DefensePoints,
                        enemyPlayer.ActualLifePoints);
            }
        }
        public void IAttackOponentMinion(Minion enemyMinion)
        {
            // Solo podemos atacar si nuestro heroe tiene puntos de ataque.
            if (this.HeroAttackPoints > 0)
            {
                // Mostramos al usuario quien ataca a quien:
                Console.WriteLine(" '{0}'[A:{1},D:{2},L:{3}] a atacado a '{4}'[A:{5},L:{6}]",
                        this.PlayerID, this.HeroAttackPoints,this.DefensePoints, this.ActualLifePoints,
                        enemyMinion.getCardName(), enemyMinion.AttackPoints,enemyMinion.ActualLifePoints);

                // El minion atacado recibe el daño.
                enemyMinion.MinionRecibeDamage(this.HeroAttackPoints);
                // El jugador que ataca tambien recibe daño
                this.RecibeDamage(enemyMinion.AttackPoints);


                // Mostramos al usuario el resultado del ataque:
                Console.WriteLine("Resultado");
                Console.WriteLine(" '{0}'[A:{1},D:{2},L:{3}] ; '{4}'[A:{5},L:{6}]",
                        this.PlayerID, this.HeroAttackPoints, this.DefensePoints, this.ActualLifePoints,
                        enemyMinion.getCardName(), enemyMinion.AttackPoints, enemyMinion.ActualLifePoints);
            }
        }
        public void ChangeDefensePoints(int defenseChange)
        {
            this.DefensePoints = this.DefensePoints + defenseChange;
            if(this.DefensePoints < 0)
            {
                this.DefensePoints = 0;
            }
        }
        public void ChangeAttackPoints(int attackChange)
        {
            this.HeroAttackPoints = this.HeroAttackPoints + attackChange;
            if (this.HeroAttackPoints < 0)
            {
                this.HeroAttackPoints = 0;
            }
        }
        public void UseMana(int manaUsed)
        {
            this.ActualMana = this.ActualMana - manaUsed;
        }
        public void RecibeDamage(int damageRecibe)
        {
            // Como su nombre lo indica, este método hace que el ...
            // jugador reciba daño.
            this.ActualLifePoints = this.ActualLifePoints - damageRecibe + this.DefensePoints;
            this.ChangeDefensePoints(-damageRecibe);
            if(this.ActualLifePoints < 0)
            {
                this.ActualLifePoints = 0;
            }
        }
        public int IChooseAction()
        {
            Console.WriteLine("(1) Jugar carta desde la mano \n(2) Atacar \n(3) Activar la habilidad del heroe \n(4) Terminar turno \n(5) Rendirse");
            string value = Console.ReadLine();
            int input;
            int selectedOption;
            // Rebizamos si se entrego un digito y no una letra
            if (int.TryParse(value, out input))
            {
                selectedOption = input;
            }
            else
            {
                selectedOption = 100000;
            }
            return selectedOption;
        }
        public void IPlayCardFromHand(int IndexOfSelectedCard , Player enemyPlayer)
        {
            ////////////////////////////////////////////////////////////////////////
            // Para poder llevar a cabo este proceso se bene cumplir ciertas cosas:
            // 1: Primero se debe tener cartas en la mano.
            // 2: Debemos ver si el indice de la carta seleccionada exite.
            // 3: Luego debemos verificar de que Clase es la carta.
            //
            // Caso de un Minion:
            //      4: Debe haber espacio en el campo para poder jugarlo.
            //      5: Debemos tener suficiente mana para invocarlo.
            //
            // Caso de un Spell:
            //      3: Se debe verificar si 
            ////////////////////////////////////////////////////////////////////////


            // Primero debemos verificar si tenemos cartas en la mano.
            int lengthOfHand = this.myhand.HandCards.Count;
            if(lengthOfHand > 0)
            {
                // Si tenemos cartas, podemos jugar una de la mano.
                // Entonces debemos ver si el indice seleccionado existe:
                if (0 < IndexOfSelectedCard && IndexOfSelectedCard <= lengthOfHand)
                {
                    // Luego debemos ver de que clase es la carta (Minion, Spell...)
                    Card selectedCard = this.myhand.HandCards[IndexOfSelectedCard - 1];
                    if (selectedCard.GetType().Equals(typeof(Minion)))
                    {
                        // Ahora vemos si hay espacio en el campo para invocarlo.
                        if (this.myField.MinionList.Count < this.myField.MaxMinions)
                        {       
                            // Si Es un minion...
                            // Hacemos un cast
                            Minion selectedMinion = (Minion)selectedCard;
                            // Tambien debemos verificar si el mana que disponemos es suficiente para invocarlo.
                            int costoMinion = selectedMinion.getCardCost();
                            if (costoMinion <= this.ActualMana)
                            {
                                // descontamos el mana usado para invocar la carta.
                                this.UseMana(costoMinion);
                                // invocamos el minion al campo:
                                this.myField.InsertMinionToField(this.myhand.IPlayCardToField(IndexOfSelectedCard));

                            }
                        }                        
                    }
                    else if (selectedCard.GetType().Equals(typeof(Spell))) // Verificamos si la carta es un spell
                    {
                        // Si es un Spell
                        // Hacemos un cast
                        Spell selectedSpell = (Spell)selectedCard;
                        // Tambien debemos verificar si el mana que disponemos es suficiente para invocarlo.
                        int costoSpell = selectedSpell.getCardCost();
                        if (costoSpell <= this.ActualMana)
                        {
                            // descontamos el mana usado para invocar la carta.
                            this.UseMana(costoSpell);
                            // invocamos el efecto del spell
                            this.myhand.IPlaySpell(IndexOfSelectedCard);

                            selectedSpell.ActivateAbility(selectedSpell.Ability, this , enemyPlayer);

                        }
                    }
                }
            }
        }
        public string getHeroName()
        {
            int heroName = this.HeroName;
            switch (HeroName)
            {
                case 1:
                    return "Warior";
                case 2:
                    return "Hunter";
                default:
                    return "null";
            }
        }
    }
}
