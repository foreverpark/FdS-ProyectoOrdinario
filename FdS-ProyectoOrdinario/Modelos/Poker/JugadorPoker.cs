using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class JugadorPoker : IJugador
    {
        //Propiedades
        public List<CartaPoker> Mano;

        //Constructor
        public JugadorPoker()
        {
            Mano = new List<CartaPoker>();
        }

        //Métodos
        public ICarta DevolverCarta(int indiceCarta)
        {
            var CartaDevuelta = Mano[indiceCarta];
            Mano.RemoveAt(indiceCarta);
            return CartaDevuelta;
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
            List<ICarta> CartasDevueltas = new(Mano);
            Mano.Clear();
            return CartasDevueltas;
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
            return Mano[indiceCarta];
        }

        public List<ICarta> MostrarCartas()
        {
            return new List<ICarta>(Mano);
        }

        public void ObtenerCartas(List<ICarta> cartas)
        {
            foreach (var carta in cartas)
            {
                Mano.Add((CartaPoker)carta);
            }
        }

        public void RealizarJugada()
        {
            throw new NotImplementedException();
        }
    }
}
