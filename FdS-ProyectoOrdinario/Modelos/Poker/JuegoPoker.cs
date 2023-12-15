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
        public IDealer Dealer => throw new NotImplementedException();

        public bool JuegoTerminado => throw new NotImplementedException();

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
