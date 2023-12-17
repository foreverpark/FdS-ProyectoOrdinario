using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class JugadorBlackjack : IJugador
    {
        //Creamos las propiedades
        private static int ContadorJugadores = 1;
        private List<ICarta> ManoDelJugador;
        public string Nombre;
        private IDealer Dealer;

        //Creamos el constructor

        public JugadorBlackjack(IDealer dealer) 
        {
            ContadorJugadores ++;
            Nombre = $"Jugardor {ContadorJugadores}";
            ManoDelJugador = new List<ICarta>();
            Dealer = dealer;
        }

        public ICarta DevolverCarta(int indiceCarta)
        {
            ICarta carta = ManoDelJugador[indiceCarta];
            ManoDelJugador.RemoveAt(indiceCarta);
            return carta;
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
           List<ICarta> todasLasCartas= new List<ICarta>();
            ManoDelJugador.Clear();
            return todasLasCartas;
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
            return ManoDelJugador[indiceCarta] ;
        }

        public List<ICarta> MostrarCartas()
        {
            
            return new List<ICarta>(ManoDelJugador) ;

        }

        public void ObtenerCartas(List<ICarta> cartas)
        {
           ManoDelJugador.AddRange(cartas);
        }

        public void RealizarJugada()
        {
            Console.WriteLine($"Turno de {Nombre}\n");
            Console.WriteLine("Sus cartas son: ");
            foreach(var carta in ManoDelJugador) 
            {
                Console.WriteLine($"{carta.Valor} de {carta.Figura}\n");
            }

            Random ramdon = new Random();

            while (true)
            {
                int puntuacion = CalcularPuntuacion();

                if (puntuacion>21) 
                {
                    Console.WriteLine("Ya no puede pedir mas cartas\n");
                    Console.WriteLine("Sus cartas son: \n");
                    foreach (var carta in ManoDelJugador)
                    {
                        Console.Write($"{carta.Valor} de {carta.Figura}\n");
                    }
                    break;
                }
                int numeroAleatoria=ramdon.Next(1,3);

                if (numeroAleatoria == 1) 
                {
                    Console.WriteLine($"{Nombre} decidio pedir otra carta\n");
                    var nuevaCarta = Dealer.RepartirCartas(1).First();
                    ObtenerCartas(new List<ICarta> { nuevaCarta });
                    Console.WriteLine($"Carta nueva: {nuevaCarta.Valor} de {nuevaCarta.Figura}\n");
                }
                else
                {
                    Console.WriteLine($"{Nombre} decidio no pedir otra carta\n");
                    Console.WriteLine("Sus cartas son: \n");
                    foreach (var carta in ManoDelJugador)
                    {
                        Console.Write($"{carta.Valor} de {carta.Figura}\n");
                    }
                    break;
                }
            }

        }

        public int CalcularPuntuacion() 
        {

            int puntuacion = 0;
            int NumeroDeAses = 0;

            foreach(var carta in ManoDelJugador) 
            {

                puntuacion += (int)carta.Valor;

                if(carta.Valor == ValoresCartasEnum.As) 
                {
                    NumeroDeAses++;
                
                }
            }

            while(puntuacion>21 && NumeroDeAses > 0) 
            {

                puntuacion -= 10;
                NumeroDeAses--;
            }

            return puntuacion;
        }
    }
}
