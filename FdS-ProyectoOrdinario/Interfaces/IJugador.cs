using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOrdinario.Interfaces
{
    public interface IJugador
    {
        void RealizarJugada();
        void ObtenerCartas(List<ICarta> cartas);
        ICarta DevolverCarta(int indiceCarta);
        List<ICarta> DevolverTodasLasCartas();
        List<ICarta> MostrarCartas();
        ICarta MostrarCarta(int indiceCarta);
    }
}