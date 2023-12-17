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
        public JuegoBlackjack() 
        {
            Jugadores = new List<IJugador>();
            Dealer=new DealerBlackjack(new DeckDeCartasBlackjack());
            JuegoTerminado=false;
        }

        public void AgregarJugador(IJugador jugador)
        {
            throw new NotImplementedException();
        }

        public void IniciarJuego()
        {
            throw new NotImplementedException();
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
