using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class DealerPoker : IDealer
    {
        IDeckDeCartas Deck;

        public DealerPoker()
        {
            Deck = new DeckDeCartasPoker();
        }

        public void BarajearDeck()
        {
            Deck.BarajearDeck();
        }

        public void RecogerCartas(List<ICarta> cartas)
        {
            Deck.MeterCarta(cartas);
        }

        public List<ICarta> RepartirCartas(int numeroDeCartas)
        {
            List<ICarta> CartasParaRepartir = new List<ICarta>();
            for (int i = 0; i < numeroDeCartas; i++)
            {
                CartasParaRepartir.Add(Deck.SacarCarta(0));
            }
            return CartasParaRepartir;
        }
    }
}
