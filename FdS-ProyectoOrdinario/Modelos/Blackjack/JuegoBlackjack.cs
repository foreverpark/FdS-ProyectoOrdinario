using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class JuegoBlackjack : IJuego
    {

        private List<IJugador> Jugadores;
        public IDealer Dealer { get; }

        IJugador Dealer_Jugador { get; }

        private IDeckDeCartas DeckDeCartas { get; }

        public bool JuegoTerminado { get; }

        public JuegoBlackjack(IDealer dealer, IJugador dealer_Jugador)
        {
            Jugadores = new List<IJugador>();
            Dealer = dealer;
            Dealer_Jugador = dealer_Jugador;
            JuegoTerminado = false;
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
                jugador.ObtenerCartas(Dealer.RepartirCartas(2));
            }

            Dealer_Jugador.ObtenerCartas(Dealer.RepartirCartas(2));
        }

        public void JugarRonda()
        {
            foreach (var jugador in Jugadores)
            {
                jugador.RealizarJugada();
                Console.ReadKey();
                Console.Clear();
            }

            Dealer_Jugador.RealizarJugada();
            Console.ReadKey();
            Console.Clear();

        }

        public void MostrarGanador()
        {
            List<IJugador> JugadoresGanadores = new List<IJugador>();

            int puntuacionDealer = CalcularPuntuacion(Dealer_Jugador.MostrarCartas());

            foreach (var jugador in Jugadores)
            {
                int puntuacionJugador = CalcularPuntuacion(jugador.MostrarCartas());

                if(puntuacionJugador>=21 && puntuacionDealer > 21) 
                {
                    JugadoresGanadores.Add(jugador);

                }
                else if (puntuacionJugador <= 21 &&puntuacionJugador>puntuacionDealer && puntuacionDealer<=21) 
                {
                  JugadoresGanadores.Add(jugador);
                }
            }

            if(JugadoresGanadores.Count > 0)
            {
                Console.WriteLine("Los ganadores son :\n");
                foreach (var ganador in JugadoresGanadores)
                {
                    Console.WriteLine(((JugadorBlackjack)ganador).Nombre);
                }
            }
            else
            {

                Console.WriteLine("El ganador es el dealer");       
            }   
        }

        private int CalcularPuntuacion(List<ICarta> cartas)
        {
            int puntuacion = 0;
            int ases = 0;

            foreach (var carta in cartas)
            {
                if (carta.Valor == ValoresCartasEnum.As)
                {
                    ases++;
                    puntuacion += 11; //al inicio el as vale 11
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

            //el valor del as cambia si supera el 21
            while (ases > 0 && puntuacion > 21)
            {
                puntuacion -= 10;
                ases--;
            }

            return puntuacion;


        }
    }
}
