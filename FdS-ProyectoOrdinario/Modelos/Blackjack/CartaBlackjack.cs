using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class CartaBlackJack : ICarta
    {
        public FigurasCartasEnum Figura { get; }

        public ValoresCartasEnum Valor { get; }
        public CartaBlackJack(FigurasCartasEnum figura, ValoresCartasEnum valor)
        {
            Figura = figura;
            Valor = valor;
        }
    }
}
