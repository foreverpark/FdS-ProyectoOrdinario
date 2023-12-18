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
        private static int ContadorJugadores = -1;
        private List<ICarta> ManoDelJugador;
        public string Nombre;
        private IDealer Dealer;

        //Creamos el constructor

        public JugadorBlackjack(IDealer dealer) 
        {
            ContadorJugadores ++;
            Nombre = $"Jugador {ContadorJugadores}";
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
            Console.WriteLine($"\nTurno de {Nombre}\n");
            Console.WriteLine("Sus cartas son: ");
            foreach(var carta in ManoDelJugador) 
            {
                Console.WriteLine($"{carta.Valor} de {carta.Figura}");
            }



            Random ramdon = new Random();

            while (true)
            {
                int puntuacion = CalcularPuntuacion();

                if (puntuacion>21) 
                {
                    Console.WriteLine("El puntuaje supero el 21 ya no puede pedir otra carta\n");
                    Console.WriteLine($"Su puntaje es de {puntuacion}\n");
                    Console.WriteLine("Sus cartas son: \n");
                    foreach (var carta in ManoDelJugador)
                    {
                        Console.Write($"{carta.Valor} de {carta.Figura} \n");
                    }
                    
                    break;
                }


                int probabilidadPedirCarta=Math.Max(0, 18-puntuacion);
                int descision=ramdon.Next(1,3);

                if (descision<=probabilidadPedirCarta) 
                {
                    Console.WriteLine($"\n{Nombre} decidio pedir otra carta\n");
                    Console.WriteLine($"Su puntaje es de {puntuacion}\n");
                    var nuevaCarta = Dealer.RepartirCartas(1).First();
                    ObtenerCartas(new List<ICarta> { nuevaCarta });
                    Console.WriteLine($"Carta nueva: {nuevaCarta.Valor} de {nuevaCarta.Figura}\n");
                }
                else
                {
                    Console.WriteLine($"{Nombre} decidio no pedir otra carta\n");
                    Console.WriteLine($"Su puntaje es de {puntuacion}\n");
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

                if (carta.Valor == ValoresCartasEnum.As)
                {
                    NumeroDeAses++;
                    puntuacion += 11;
                }
                else if (carta.Valor >= ValoresCartasEnum.Diez && carta.Valor <= ValoresCartasEnum.Rey)
                {
                    puntuacion += 10;
                }
                else
                {
                    puntuacion += (int)carta.Valor;
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
