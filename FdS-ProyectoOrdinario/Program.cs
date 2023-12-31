﻿using ProyectoOrdinario.Interfaces;
using ProyectoOrdinario.Enumeradores;
using FdS_ProyectoOrdinario.Modelos.Blackjack;
using System.ComponentModel;
using FdS_ProyectoOrdinario.Modelos.Poker;

namespace FdS_ProyectoOrdinario
{
    internal class Program
    {
        static void Main(string[] args)
        {


            bool falloIntento = true;

            while (falloIntento == true)
            {

                try
                {
                    Console.WriteLine("¿Que juego desea jugar?  1) Poker clásico  2) 21 BlackJack");
                    int opcionJuego = int.Parse(Console.ReadLine());

                    switch (opcionJuego)
                    {
                        case 1:
                            {
                                var juegoPoker = new JuegoPoker();
                                Console.WriteLine("Estas jugando Poker Clásico :)");
                                int NumeroJugadores = 0;
                                Console.WriteLine("Ingrese el numero de Jugadores: ");
                                while (true)
                                {
                                    NumeroJugadores = int.Parse(Console.ReadLine());

                                    //validate number
                                    if (NumeroJugadores > 0 && NumeroJugadores < 8)
                                    {
                                        Console.Clear();
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nIngrese un numero mayor que 0 y menor que 8");
                                    }
                                }

                                for (int i = 0; i < NumeroJugadores; i++)
                                {
                                    juegoPoker.AgregarJugador(new JugadorPoker(new DealerPoker()));
                                }

                                juegoPoker.IniciarJuego();
                                falloIntento = false;

                                break;

                            }
                        case 2:
                            {
                                Console.Clear();
                                Console.WriteLine("Estas jugando 21 Black Jack :)\n");

                                Console.WriteLine("Ingrese el numero de Jugadores: ");
                                int numeroJugadores = int.Parse(Console.ReadLine());

                                if (numeroJugadores > 7 || numeroJugadores <= 0)
                                {
                                    throw new Exception("Ingrese un numero mayor a 0 y menor a 7");
                                }
                                Console.Clear();

                                var Deck21BlacKJack = new DeckDeCartasBlackjack();
                                var DealerBlackJack = new DealerBlackjack(Deck21BlacKJack);
                                var Dealer_Jugador = new JugadorBlackjack(DealerBlackJack)
                                {
                                    Nombre = "Dealer"
                                };
                                var juegoBlackJack = new JuegoBlackjack(DealerBlackJack, Dealer_Jugador);


                                for (int i = 0; i < numeroJugadores; i++)
                                {
                                    juegoBlackJack.AgregarJugador(new JugadorBlackjack(DealerBlackJack));
                                }

                                juegoBlackJack.IniciarJuego();
                                juegoBlackJack.JugarRonda();
                                juegoBlackJack.MostrarGanador();
                                falloIntento = false;
                                break;
                            }

                        default:
                            {
                                throw new Exception("Ingrese un numero valido");
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadKey();
                    Console.Clear();
                    falloIntento = true;
                }

            }
        }
    }
}