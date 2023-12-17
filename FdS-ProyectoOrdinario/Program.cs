using ProyectoOrdinario.Interfaces;
using ProyectoOrdinario.Enumeradores;
using FdS_ProyectoOrdinario.Modelos.Blackjack;

namespace FdS_ProyectoOrdinario
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("¿Que juego desea jugar?  1) Poker clásico  2) 21 BlackJack");
            int OpcionJuego=int.Parse(Console.ReadLine());

            switch (OpcionJuego)
            {
                case 1: 
                    {
                        Console.WriteLine("Estas jugando Poker Clásico :)");
                        break;
                    
                    
                    }


                case 2: 
                    {
                        Console.WriteLine("\nEstas jugando 21 Black Jack :)\n");
                        int NumeroJugadores = 0;
                        Console.WriteLine("Ingrese el numero de Jugadores: ");
                        while (true) 
                        {
                            NumeroJugadores = int.Parse(Console.ReadLine());

                            if(NumeroJugadores >0 && NumeroJugadores < 8)
                            { 
                                break;
                            }
                            else 
                            {
                                Console.WriteLine("\nIngrese un numero mayor que 0 y menor que 8");
                            }
                        }

                        var juegoBlackJack = new JuegoBlackjack();




                        break;
                    }

                    default: 
                    {

                        Console.WriteLine("Ingrese un numero Valido");
                        break;
                    }
            }
        }
    }
}