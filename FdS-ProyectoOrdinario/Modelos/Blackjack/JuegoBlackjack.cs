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

        public bool JuegoTerminado { get; }

        //Constructor
        public JuegoBlackjack(IDealer dealer) 
        {
            Jugadores = new List<IJugador>();
            Dealer = dealer;
            JuegoTerminado=false;
        }

        public void AgregarJugador(IJugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public void IniciarJuego()
        {
            Dealer.BarajearDeck();
            
            foreach(var  jugador in Jugadores) 
            {
                jugador.ObtenerCartas(Dealer.RepartirCartas(2));
            }
        }

        public void JugarRonda()
        {
            throw new NotImplementedException();
        }

        public void MostrarGanador()
        {
            throw new NotImplementedException();
        }
    }
}
