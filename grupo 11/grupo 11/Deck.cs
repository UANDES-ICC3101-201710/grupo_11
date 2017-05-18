using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace grupo_11
{
    class Deck
    {
        public List<Card> DeckCards;
        public Deck(string dirListaCartasdelMazo)
        {

            this.DeckCards = new List<Card>();
            // Leemos el archivo con las cartas disponibles del mazo.
            string[] lines = System.IO.File.ReadAllLines(dirListaCartasdelMazo);
            string textoCarta;
            int comaIndex;

            for (int i = 0; i < 30; i++)
            {
                textoCarta = lines[i]; int lengthOfTextCarta = textoCarta.Length;
                string cardIdentifier = textoCarta.Substring(0, 1);

                textoCarta = textoCarta.Substring(2, lengthOfTextCarta - 2 );
                lengthOfTextCarta = textoCarta.Length;

                // Verificamos si la carta es un minion
                if(cardIdentifier.Equals("m"))
                {
                    comaIndex = textoCarta.IndexOf(",");
                    string nombreCarta = textoCarta.Substring(0, comaIndex);
                    int costo = Int32.Parse(textoCarta.Substring(comaIndex + 1, 1));
                    int ataque = Int32.Parse(textoCarta.Substring(comaIndex + 3, 1));
                    int vida = Int32.Parse(textoCarta.Substring(comaIndex + 5, 1));
                    string clase = "null";
                    string efecto = "null";
                    string status = "null";
                    string rareza = "null";
                    int posicion = 0;
                    Minion newMinion = new Minion(vida, vida, ataque, false, efecto, status, costo, nombreCarta, rareza, clase, posicion);
                    this.DeckCards.Add(newMinion);
                }
                else if (cardIdentifier.Equals("s")) // Verificamos si la carta es un spell
                {
                    comaIndex = textoCarta.IndexOf(",");
                    string nombreCarta = textoCarta.Substring(0, comaIndex);
                    textoCarta = textoCarta.Substring(comaIndex, lengthOfTextCarta - comaIndex);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    int costo = Int32.Parse(textoCarta.Substring(0, 1));
                    textoCarta = textoCarta.Substring(comaIndex, lengthOfTextCarta - comaIndex);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    string Ability = textoCarta.Substring(0, comaIndex);
                    textoCarta = textoCarta.Substring(comaIndex, lengthOfTextCarta - comaIndex);
                    lengthOfTextCarta = textoCarta.Length;
                    comaIndex = textoCarta.IndexOf(",");

                    string clase = "null";
                    string rareza = "null";
                    int posicion = 0;

                    Spell newSpell = new Spell(Ability, costo, nombreCarta, rareza, clase, posicion);
                    this.DeckCards.Add(newSpell);
                }
            }
        }
        public List<Card> getDeck
        {
            get { return this.DeckCards; }
        }
        public void showCardsInDeck()
        {
            Console.WriteLine("Name of cards in deck");
            foreach (Minion carta in this.DeckCards)
            {
                carta.IShowCardData();
            }
        }
        public void ShuffleDeck()
        {
            Random rand = new Random();
            for(int i = 0; i <= rand.Next(2, 10); i++)
            {
                Random rng = new Random();
                int n = this.DeckCards.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    Card value = this.DeckCards[k];
                    this.DeckCards[k] = this.DeckCards[n];
                    this.DeckCards[n] = value;
                }
            }
            
        }
        public Card giveCard()
        {
            // Este método asume inmediatamente que existen cartas en el mazo
            // Para evitar el problema de cuando no haya, hay otro método el cual
            // se encarga de verificar si hay, y si llamar o no a este.
            int lengthOfDeck = this.DeckCards.Count;
            Card carta = this.DeckCards[lengthOfDeck-1];
            this.DeckCards.RemoveAt(lengthOfDeck-1);
            return carta;
        }        
        public int FindCard(Card[] DeckCards, String NameofCard)
        {
            // Estamos trabajando para usted...
            return 0;
        }
    }
}
