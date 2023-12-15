using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class CartaPoker : ICarta
    {
        public FigurasCartasEnum Figura => throw new NotImplementedException();

        public ValoresCartasEnum Valor => throw new NotImplementedException();
    }
}
