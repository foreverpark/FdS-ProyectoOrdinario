﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoOrdinario.Interfaces
{
    public interface IDeckDeCartas
    {
        void BarajearDeck();
        ICarta VerCarta(int indiceCarta);
        ICarta SacarCarta(int indiceCarta);
        void MeterCarta(ICarta carta);
        void MeterCarta(List<ICarta> cartas);
    }
}