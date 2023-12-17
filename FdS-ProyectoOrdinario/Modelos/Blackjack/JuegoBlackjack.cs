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

        IJugador Dealer_Jugador;
        public IDealer Dealer { get; }

        private IDeckDeCartas DeckDeCartas { get; }

        public bool JuegoTerminado { get; }

        //Constructor
<<<<<<< Updated upstream
        public JuegoBlackjack(IDealer dealer) 
        {
            Jugadores = new List<IJugador>();
            Dealer = dealer;
            JuegoTerminado=false;
=======
        public JuegoBlackjack()
        {
            Jugadores = new List<IJugador>();
            Dealer = new DealerBlackjack(new DeckDeCartasBlackjack());
            JuegoTerminado = false;
>>>>>>> Stashed changes
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
        }

        public void JugarRonda()
        {
            foreach (var jugador in Jugadores)
            {
                jugador.RealizarJugada();
            }

            Dealer_Jugador.RealizarJugada();
            MostrarGanador();

        }

        public void MostrarGanador()
        {
            IJugador ganador = null;

            int puntuacionDeGanador = 0;

            foreach (var jugador in Jugadores)
            {
                int puntuacionJugador = CalcularPuntuacion(jugador.MostrarCartas());

                if (puntuacionJugador <= 21 && puntuacionJugador > puntuacionDeGanador)
                {
                    ganador = jugador;
                    puntuacionDeGanador = puntuacionJugador;
                }
            }

            int puntuacionDealer = CalcularPuntuacion(Dealer_Jugador.MostrarCartas());

            if (puntuacionDealer <= 21 && puntuacionDealer > puntuacionDeGanador)
            {
                ganador = null;
            }

            Console.WriteLine("El ganador es: " + (ganador != null ? "Jugador" : "Dealer"));
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
