using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class JuegoPoker : IJuego
    {
        private List<IJugador> Jugadores { get; }
        public IDealer Dealer { get; }

        public bool JuegoTerminado { get; } 

        public JuegoPoker()
        {
            Dealer = new DealerPoker();
        }

        
        public void AgregarJugador(IJugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public void IniciarJuego()
        {
            Dealer.BarajearDeck();

            foreach (var jugador in Jugadores)
            {
                jugador.ObtenerCartas(Dealer.RepartirCartas(5));
            }

            JugarRonda();
        
        }

        public void JugarRonda()
        {
            foreach (var jugador in Jugadores)
            {
                jugador.RealizarJugada();
                Console.ReadKey();
                Console.Clear();
            }

            int numeroJugador = 1;
            foreach (var jugador in Jugadores)
            {
                Console.WriteLine($"Jugador {numeroJugador}:");
                var Mano = jugador.MostrarCartas();
                int numeroCarta = 1;
                foreach(var carta in Mano)
                {
                    Console.WriteLine($"Carta {numeroCarta}: {carta.Valor} de {carta.Figura}" );
                    numeroCarta += 1;
                }
                numeroJugador += 1;
            }
        }

        public void MostrarGanador()
        {
            throw new NotImplementedException();
        }
    }
}
