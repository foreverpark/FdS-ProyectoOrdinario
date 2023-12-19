using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class CartaPoker : ICarta
    {
        public FigurasCartasEnum Figura { get; }

        public ValoresCartasEnum Valor { get; }

        public CartaPoker(FigurasCartasEnum figura, ValoresCartasEnum valor)
        {
            Figura = figura;
            Valor = valor;
        }
    }
}
