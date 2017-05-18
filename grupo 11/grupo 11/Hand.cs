using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Hand : IPlayCard
    {
        private int MaxHandcards = 10;
        private int ActualHandCards;
        public List<Card> HandCards;

        public Hand(Deck deck, int doIStart)
        {
            List<Card> myDeck = deck.getDeck;
            // myDeck es el mazo de cartas del jugador.
            // doIStart permite dicernir si uno comienza primero(1) o segundo(2).
            this.HandCards = new List<Card>();
            this.ActualHandCards = 0;

            if (doIStart == 1)
            {
                // recibe 4 cartas del mazo
                for (int i = 0; i < 4; i++)
                {
                    int lengthOfDeck = myDeck.Count;
                    this.HandCards.Add(myDeck[lengthOfDeck-1]);
                    this.ActualHandCards++;
                    myDeck.RemoveAt(lengthOfDeck-1);
                }
            }
            else
            {
                // recibe 5 cartas (y una moneda "en un futuro")
                for (int i = 0; i < 5; i++)
                {
                    int lengthOfDeck = myDeck.Count;
                    this.HandCards.Add(myDeck[lengthOfDeck - 1]);
                    this.ActualHandCards++;
                    myDeck.RemoveAt(lengthOfDeck-1);
                }
                // Ahora Recibimos la carta moneda.
                string lines = System.IO.File.ReadAllText("CoinCard.txt");
                string textoCarta;
                int comaIndex;
                textoCarta = lines; int lengthOfTextCarta = textoCarta.Length;
                string cardIdentifier = textoCarta.Substring(0, 1);

                textoCarta = textoCarta.Substring(2, lengthOfTextCarta - 2);
                lengthOfTextCarta = textoCarta.Length;
                if (cardIdentifier.Equals("s")) // Verificamos si la carta es un spell
                {
                    comaIndex = textoCarta.IndexOf(",");
                    string nombreCarta = textoCarta.Substring(0, comaIndex);
                    textoCarta = textoCarta.Substring(comaIndex + 1, lengthOfTextCarta - comaIndex - 1);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    int costo = Int32.Parse(textoCarta.Substring(0, 1));
                    textoCarta = textoCarta.Substring(comaIndex + 1, lengthOfTextCarta - comaIndex - 1);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    string Ability = textoCarta.Substring(0, comaIndex);
                    textoCarta = textoCarta.Substring(comaIndex + 1, lengthOfTextCarta - comaIndex - 1);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    string clase = "null";
                    string rareza = "null";
                    int posicion = 0;

                    Spell CoinCard = new Spell(Ability, costo, nombreCarta, rareza, clase, posicion);
                    this.HandCards.Add(CoinCard);
                    this.ActualHandCards++;
                }

            }



        }

        public void DrawCard(Deck deck)
        {
            List<Card> myDeck = deck.getDeck;
            // Revisamos cuantas cartas le quedan al mazo
            int lengthOfDeck = myDeck.Count;
            Console.WriteLine("Cartas en el Mazo: {0} , Cartas en la Mano: {1}", lengthOfDeck, this.ActualHandCards);
            if (lengthOfDeck > 0)
            {
                // Si es que quedan cartas, se saca una del mazo
                Card carta = deck.giveCard();
                // Se chequea que haya espacio en la mano para la carta
                if (this.ActualHandCards < this.MaxHandcards)
                {
                    // Si queda espacio, entonces anexamos la carta a la mano.
                    this.HandCards.Add(carta);
                    this.ActualHandCards++;
                }
                else
                {
                    // si es que no queda espacio, la carta se destruye.
                    // (Esto ocurre automaticamente, no hay que escribir codigo)
                }
            }
            else
            {
                // Si no quedan cartas en el mazo, no se puede sacar y se entra en fatiga
                // ¡¡¡ Se debe Activar una alarma !!!
                // (esto ocurre automaticamente, no hay que escribir codigo)
            }
            
            
        }

        public Minion IPlayCardToField(int indexOfSelectedCard)
        {
            // Primero buscamos que carta se selecciono
            Card SelectedCard = this.HandCards[indexOfSelectedCard - 1];
            // Luego se elimina esa carta de la mano
            this.HandCards.RemoveAt(indexOfSelectedCard - 1);
            // Hacemos un Cast
            Minion SelectedMinion = (Minion)SelectedCard;

            return SelectedMinion;
        }

        public Spell IPlaySpell(int indexOfSelectedCard) 
        {
            // Primero buscamos que carta se selecciono
            Card SelectedCard = this.HandCards[indexOfSelectedCard - 1];
            // Luego se elimina esa carta de la mano
            this.HandCards.RemoveAt(indexOfSelectedCard - 1);
            // Hacemos un Cast
            Spell SelectedSpell = (Spell)SelectedCard;

            return SelectedSpell;
        }
    }
}
