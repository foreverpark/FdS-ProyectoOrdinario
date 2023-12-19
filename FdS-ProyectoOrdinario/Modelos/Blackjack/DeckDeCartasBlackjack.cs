using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class DeckDeCartasBlackjack : IDeckDeCartas
    {
        List<ICarta> cartas;

        public DeckDeCartasBlackjack()
        {
            CrearDeck();
            BarajearDeck();
        }

        public void CrearDeck()
        {
            cartas = new List<ICarta>();

            //se hace un foreach a los enumeradores para asignarlos a una carta y este sea añadido a una lista de cartas(deck)
            foreach (FigurasCartasEnum figuras in Enum.GetValues(typeof(FigurasCartasEnum)))
            {
                foreach (ValoresCartasEnum valores in Enum.GetValues(typeof(ValoresCartasEnum)))
                {
                    cartas.Add(new CartaBlackJack(figuras, valores));
                }
            }
        }

        public void BarajearDeck()
        {
            Random random = new Random();
            int numeroDeCartas = cartas.Count;
            while (numeroDeCartas > 1)
            {
                numeroDeCartas--;

                int numeroRandom = random.Next(numeroDeCartas + 1);
                ICarta carta = cartas[numeroRandom];

                cartas[numeroRandom] = cartas[numeroDeCartas];

                cartas[numeroDeCartas] = carta;
            }
        }

        public ICarta VerCarta(int indiceCarta)
        {
            return cartas[indiceCarta];
        }

        public ICarta SacarCarta(int indiceCarta)
        {
            ICarta carta = cartas[indiceCarta];
            cartas.RemoveAt(indiceCarta);
            return carta;
        }

        public void MeterCarta(ICarta carta)
        {
            cartas.Add(carta);
        }

        public void MeterCarta(List<ICarta> Cartas)
        {
            cartas.AddRange(Cartas);
        }
    }
}
