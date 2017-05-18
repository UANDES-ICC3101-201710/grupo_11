using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace grupo_11
{
    class Program
    {
        static void Main(string[] args)
        {
            SoundPlayer sndPlayer = new SoundPlayer();
            sndPlayer.SoundLocation = @"Sound\Main_Title.wav";
            sndPlayer.PlayLooping();

            Console.WriteLine("######################################################################################################################");
            Console.WriteLine("###   ##  ## #####   ##   ######  ###### ##   ##  ###### ######  #####  ###   ##  #####   ######/_______  ############");
            Console.WriteLine("###   ##  ## ##     #  #  ##    #   ##   ##   ## ###       ##   ### ### ####  ##  ##      #####| #######   ###########");
            Console.WriteLine("###   ###### ####  ##  ## ######    ##   ####### #######   ##   ### ### ########  ####    ###### ___|###|  |##########");
            Console.WriteLine("###   ##  ## ##    ###### ##  ##    ##   ##   ##      ##   ##   ### ### ##  ####  ##      #############/  /###########");
            Console.WriteLine("###   ##  ## ##### ##  ## ##   ##   ##   ##   ## ######    ##    #####  ##    ##  #####   #########<_____/############");
            Console.WriteLine("######################################################################################################################");
            Console.WriteLine(" \n \n                                            Press start");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.ReadKey();
            Console.Beep();

            // Empezamos el juego.
            // Extraemos la dirección de las cartas del mazo.
            string dir = "Cartas.txt";

            // Preguntamos datos a ambos jugadores
            Console.Write("               Jugador 1, ingrese su nombre: ");
            string player1ID = Console.ReadLine();
            Console.WriteLine("");
            Console.Beep();
            int heroName1 = 0;
            Boolean aux = true;
            // Rebizamos si se entrego un digito y no una letra
            while (aux)
            {
                Console.Write("               Elija su Heroe:\n               (1) Warrior \n               (2) Hunter \n               Ingrese su opcion: ");
                string value = Console.ReadLine();
                int selected;
                if (int.TryParse(value, out selected))
                {
                    heroName1 = selected;
                    aux = false;
                }
                Console.WriteLine("");
            }
            Console.Beep();


            Console.Write("               Jugador 2, ingrese su nombre: ");
            string player2ID = Console.ReadLine();
            Console.WriteLine("");
            Console.Beep();
            int heroName2 = 0;
            aux = true;
            // Rebizamos si se entrego un digito y no una letra
            while (aux)
            {
                Console.Write("               Elija su Heroe:\n               (1) Warrior \n               (2) Hunter \n               Ingrese su opcion: ");
                string value = Console.ReadLine();
                int selected;
                if (int.TryParse(value, out selected))
                {
                    heroName2 = selected;
                    aux = false;
                }
                Console.WriteLine("");
            }
            Console.Beep();
            //

            // Creamos el Juego con los jugadores
            MainGame newGame = new MainGame(player1ID, heroName1, dir, player2ID, heroName2, dir);

            Player Jugador1 = newGame.jugador1;
            Player Jugador2 = newGame.jugador2;

            while (newGame.GameFinished == true)
            {
                newGame.ShowGameField();
                newGame.jugador1.TurnStart();
                while (newGame.jugador1.PlayerTurnEnable && newGame.GameFinished == true)
                {
                    newGame.PlayTurn(Jugador1, Jugador2);
                }

                newGame.ShowGameField();
                newGame.jugador2.TurnStart();
                while (newGame.jugador2.PlayerTurnEnable && newGame.GameFinished == true)
                {
                    newGame.PlayTurn(Jugador2, Jugador1);
                }


            }
        }
    }
}
