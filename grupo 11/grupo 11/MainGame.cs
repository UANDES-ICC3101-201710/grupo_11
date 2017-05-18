using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace grupo_11
{
    class MainGame
    {
        public Player jugador1;
        public Player jugador2;
        public Boolean GameFinished;

        public MainGame(string playerID1, int heroName1, string dirMazo1,
            string playerID2, int heroName2, string dirMazo2)
        {
            int coinResult = this.FlipCoin();
            if (coinResult == 1)
            {
                this.jugador1 = new Player(playerID1, heroName1, dirMazo1, 1);
                this.jugador2 = new Player(playerID2, heroName2, dirMazo2, 2);
            }
            else
            {
                this.jugador1 = new Player(playerID2, heroName2, dirMazo2, 1);
                this.jugador2 = new Player(playerID1, heroName1, dirMazo1, 2);
            }
            this.GameFinished = true;

        }

        public int FlipCoin()
        {
            Random rand = new Random();
            int resultFlipCoin = rand.Next(0, 2);
            return resultFlipCoin;
        }
        public void PlayTurn(Player jugador, Player jugadorEnemigo)
        {
            this.ShowGameField();
            this.ShowPlayerHand(jugador);
            int actionChoosed = jugador.IChooseAction();
            Console.Beep();
            string value;
            switch (actionChoosed)
                {
                case 1:// Caso en que queremos jugar una carta de la mano al campo.
                    Console.WriteLine("  Selecione la carta de su mano que desea jugar:");
                    value = Console.ReadLine();
                    int selectedCard;
                    // Rebizamos si se entrego un digito y no una letra
                    if (int.TryParse(value, out selectedCard))
                    {
                        jugador.IPlayCardFromHand(selectedCard, jugadorEnemigo);
                    }
                    else
                    {
                        Console.WriteLine("No es posible realizar la acción: 'No existe Minion en la selección '");
                    }

                    break;

                case 2: // Caso en el que queremos atacar al oponente.
                    Console.WriteLine("  ¿Con Quien Desea Atacar?\n (0) Para atacar con el 'Hero'\n (i) seleccione un 'Minion' del campo\n");
                    value = Console.ReadLine();
                    int selected;
                    // Rebizamos si se entrego un digito y no una letra
                    if (int.TryParse(value, out selected))
                    {
                        if (selected == 0)
                        {
                            Console.Beep();
                            // Primero debemos verificar si nuestro hero puede atacar
                            if (jugador.HeroAttackEnable)
                            {
                                // Vamos a atacar con el Hero
                                Console.WriteLine("¿A Quien Desea Atacar?\n (0) Para atacar con al'Hero' enemigo\n (i) seleccione un 'Minion' del campo enemigo\n");
                                int selected2 = Int32.Parse(Console.ReadLine());
                                if (selected2 == 0)
                                {
                                    // Atacamos al Hero oponente
                                    jugador.IAttackOponentPlayer(jugadorEnemigo);
                                    jugador.myField.CheckMinionsLife();
                                    jugadorEnemigo.myField.CheckMinionsLife();
                                }
                                else
                                {
                                    //Atacamos al un minion oponente
                                    Boolean minionExist = jugadorEnemigo.myField.CheckIfSelectedMinionExist(selected2);
                                    if (minionExist)
                                    {
                                        jugador.IAttackOponentMinion(jugadorEnemigo.myField.GetMinionFromField(selected2));
                                        jugador.myField.CheckMinionsLife();
                                        jugadorEnemigo.myField.CheckMinionsLife();
                                    }
                                    else
                                    {
                                        Console.WriteLine("No es posible realizar la acción: 'No existe Minion en la selección.");
                                    }

                                }
                            }
                            else
                            {
                                Console.WriteLine("El Hero No puede Atacar!");
                                Console.ReadLine();
                            }

                        }
                        else
                        {
                            Console.Beep();
                            // Atacamos con un minion nuestro
                            Boolean minionExist = jugador.myField.CheckIfSelectedMinionExist(selected);
                            if (minionExist)
                            {
                                Minion minionSelected = jugador.myField.GetMinionFromField(selected);
                                // Primero debemos verificar si el minion puede atacar
                                if (minionSelected.AttackEnable)
                                {
                                    Console.WriteLine("¿A Quien Desea Atacar?\n (0) Para atacar con al'Hero' enemigo\n (i) seleccione un 'Minion' del campo enemigo\n");
                                    int selected2 = Int32.Parse(Console.ReadLine());
                                    if (selected2 == 0)
                                    {
                                        Console.Beep();
                                        minionSelected.IAttackPlayer(jugadorEnemigo);
                                        jugador.myField.CheckMinionsLife();
                                        jugadorEnemigo.myField.CheckMinionsLife();
                                    }
                                    else
                                    {
                                        Boolean minionExist2 = jugadorEnemigo.myField.CheckIfSelectedMinionExist(selected2);
                                        if (minionExist2)
                                        {
                                            Console.Beep();
                                            Minion enemyMinionSelected = jugadorEnemigo.myField.GetMinionFromField(selected2);
                                            minionSelected.IAttack(enemyMinionSelected);
                                            jugador.myField.CheckMinionsLife();
                                            jugadorEnemigo.myField.CheckMinionsLife();
                                        }
                                        else
                                        {
                                            Console.Beep();
                                            Console.WriteLine("No es posible realizar la acción: 'No existe Minion en la selección '");
                                            Console.ReadLine();
                                        }
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.WriteLine("Opcion Fallida! El Minion No Puede Atacar");
                                    Console.ReadLine();
                                }
                            }
                            else
                            {
                                Console.Beep();
                                Console.WriteLine("No es posible realizar la acción: 'No existe Minion en la selección '");
                                Console.ReadLine();
                            }
                        }

                    }
                    else
                    {
                        Console.Beep();
                        Console.WriteLine("Opcion Fallida! No ingreso un digito");
                        Console.ReadLine();
                    }

                    break;
                case 3: // Caso en el que queremos activar la habilidad del heroe
                    jugador.IActivateHeroAbility(jugadorEnemigo);
                    Console.Beep(); Console.Beep();
                    break;
                case 4: // caso en el cual no queremos continuar el turno.
                    jugador.TurnEnd();
                    Console.Beep();
                    break;
                case 5: // Caso en el que un jugador decide rendirse.
                    jugador.TurnEnd();
                    this.FinishGame(jugadorEnemigo.coinFlip);
                    break;
                default:
                    Console.Beep();
                    Console.WriteLine("Opcion no valida");
                    Console.ReadLine();
                    break;

            }
            this.CheckGame();           
        }
        public void CheckGame()
        {
            int lifePlayer1 = this.jugador1.ActualLifePoints;
            int lifePlayer2 = this.jugador2.ActualLifePoints;
            if (lifePlayer1 <= 0 && lifePlayer2 > 0)
            {
                this.FinishGame(2);
            }
            if (lifePlayer2 <= 0 && lifePlayer1 > 0)
            {
                this.FinishGame(1);
            }
            if (lifePlayer1 <= 0 && lifePlayer2 <= 0)
            {
                this.FinishGame(3);
            }
        }
        public void FinishGame(int option)
        {
            if (option == 1)
            {
                Console.WriteLine("Ganador Jugador: {0}", this.jugador1.PlayerID);
            }
            else if (option == 2)
            {
                Console.WriteLine("Ganador Jugador: {0}", this.jugador2.PlayerID);
            }
            else if (option == 3)
            {
                Console.WriteLine("Es un Empate!!");
            }
            else
            {
                
            }
            this.GameFinished = false;
            SoundPlayer sndPlayer = new SoundPlayer();
            sndPlayer.SoundLocation = @"Sound\Sound_Of_Victory.wav";
            sndPlayer.PlayLooping();
            Console.ReadLine();
        }
        public void ShowGameField()
        {
            Console.Clear();
            string player1ID = this.jugador1.PlayerID;
            string player2ID = this.jugador2.PlayerID;
            string heroname1 = this.jugador1.getHeroName();
            string heroname2 = this.jugador2.getHeroName();
            List<Minion> minionList1 = this.jugador1.myField.MinionList;
            List<Minion> minionList2 = this.jugador2.myField.MinionList;
            Deck deck1 = this.jugador1.myDeck;
            Deck deck2 = this.jugador2.myDeck;

            StringBuilder name1 = new StringBuilder();
            StringBuilder name2 = new StringBuilder();
            StringBuilder fieldminionsNames1 = new StringBuilder();
            StringBuilder fieldminionsNames2 = new StringBuilder();
            StringBuilder attacks1 = new StringBuilder();
            StringBuilder attacks2 = new StringBuilder();
            StringBuilder defense1 = new StringBuilder();
            StringBuilder defense2 = new StringBuilder();
            StringBuilder cost1 = new StringBuilder();
            StringBuilder cost2 = new StringBuilder();

            Console.WriteLine("######################################################################################################################");
            Console.WriteLine("###   ##  ## #####   ##   ######  ###### ##   ##  ###### ######  #####  ###   ##  #####   ######/_______  ############");
            Console.WriteLine("###   ##  ## ##     #  #  ##    #   ##   ##   ## ###       ##   ### ### ####  ##  ##      #####| #######   ###########");
            Console.WriteLine("###   ###### ####  ##  ## ######    ##   ####### #######   ##   ### ### ########  ####    ###### ___|###|  |##########");
            Console.WriteLine("###   ##  ## ##    ###### ##  ##    ##   ##   ##      ##   ##   ### ### ##  ####  ##      #############/  /###########");
            Console.WriteLine("###   ##  ## ##### ##  ## ##   ##   ##   ##   ## ######    ##    #####  ##    ##  #####   #########<_____/############");
            Console.WriteLine("######################################################################################################################");
            Console.WriteLine(" \n \n     Field");
            
            Console.WriteLine("######################################################################################################################");
            Console.WriteLine("      {0}  {1}   [Mazo: {2}]", player1ID, heroname1, deck1.DeckCards.Count);
            Console.WriteLine("                 |A:{0}|D:{1}|L:{2}|M:{3}/{4}|"
                , this.jugador1.HeroAttackPoints,this.jugador1.DefensePoints,this.jugador1.ActualLifePoints
                ,this.jugador1.ActualMana,this.jugador1.MaxManaTurn);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            foreach(Minion minion in minionList1)
            {
                int length = 13;
                string minionName = minion.getCardName();
                name1.Append(minionName); name1.Append("            "); minionName = name1.ToString();
                minionName = minionName != null && minionName.Length > length ? minionName.Substring(0, length) : minionName;
                fieldminionsNames1.Append("["); fieldminionsNames1.Append(minionName); fieldminionsNames1.Append("]");

                attacks1.Append("|A:"); attacks1.Append(minion.AttackPoints.ToString()); attacks1.Append("          |");
                defense1.Append("[L:"); defense1.Append(minion.ActualLifePoints.ToString()); defense1.Append("          ]");
                cost1.Append("|C:"); cost1.Append(minion.getCardCost().ToString()); cost1.Append("          |");
                name1.Clear();

            }
            string fmn1 = fieldminionsNames1.ToString();  string a1 = attacks1.ToString();
            string d1 = defense1.ToString();  string c1 = cost1.ToString();
            Console.WriteLine(fmn1); Console.WriteLine(a1); Console.WriteLine(d1);
            //////////////////////////////////////
            Console.WriteLine("");
            /////////////////////////////////////
            foreach (Minion minion in minionList2)
            {
                int length = 13;
                string minionName = minion.getCardName();
                name2.Append(minionName); name2.Append("            "); minionName = name2.ToString();
                minionName = minionName != null && minionName.Length > length ? minionName.Substring(0, length) : minionName;
                fieldminionsNames2.Append("["); fieldminionsNames2.Append(minionName); fieldminionsNames2.Append("]");

                attacks2.Append("|A:"); attacks2.Append(minion.AttackPoints.ToString()); attacks2.Append("          |");
                defense2.Append("[L:"); defense2.Append(minion.ActualLifePoints.ToString()); defense2.Append("          |");
                cost2.Append("|C:"); cost2.Append(minion.getCardCost().ToString()); cost2.Append("          |");
                name2.Clear();

            }
            string fmn2 = fieldminionsNames2.ToString(); string a2 = attacks2.ToString();
            string d2 = defense2.ToString(); string c2 = cost2.ToString();
            Console.WriteLine(fmn2); Console.WriteLine(a2); Console.WriteLine(d2);


            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("      {0}  {1}   [Mazo: {2}]", player2ID, heroname2, deck2.DeckCards.Count);
            Console.WriteLine("                 |A:{0}|D:{1}|L:{2}|M:{3}/{4}|"
                , this.jugador2.HeroAttackPoints, this.jugador2.DefensePoints, this.jugador2.ActualLifePoints
                , this.jugador2.ActualMana, this.jugador2.MaxManaTurn);
            Console.WriteLine("######################################################################################################################");
            Console.WriteLine("");
        }
        public void ShowPlayerHand(Player jugador)
        {
            string player1ID = jugador.PlayerID;
            string heroname1 = jugador.getHeroName();

            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Turno de '{0}' [A:{1}|D:{2}|L:{3}] ", player1ID, jugador.HeroAttackPoints,jugador.DefensePoints,jugador.ActualLifePoints);
            Console.WriteLine("Cartas en mano: [{0}] ", jugador.myhand.HandCards.Count);
            Console.WriteLine("Mana disponible: [{0}/{1}]", jugador.ActualMana, jugador.MaxManaTurn);
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            foreach (Card card in jugador.myhand.HandCards)
            {
                // debemos verificar que clase es  la carta
                if (card.GetType().Equals(typeof(Minion)))
                {
                    // Hacemos el cast
                    Minion minion = (Minion)card;
                    string minionName = minion.getCardName();
                    Console.WriteLine("[{0}  |Ataque:{1}|Costo:{2}|Vida:{3}]", minion.getCardName(), minion.AttackPoints, minion.getCardCost(), minion.ActualLifePoints);
                }
                else if (card.GetType().Equals(typeof(Spell)))
                {
                    // Hacemos el cast
                    Spell spell = (Spell)card;
                    string spellName = spell.getCardName();
                    Console.WriteLine("[{0}  |Costo:{1}|Efecto: {2} ]", spellName , spell.getCardCost() , spell.Ability);
                }

            }
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");

            //////////////////////////////////////
        }
    }
}
