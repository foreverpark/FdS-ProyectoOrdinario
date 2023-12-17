using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Blackjack
{
    internal class DealerBlackjack : IDealer
    {

        IDeckDeCartas Deck;
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
            var cartasRepartidas=new List<ICarta>();

            for (int i = 0; i < numeroDeCartas; i++) 
            {
                ICarta carta = Deck.SacarCarta(0);
                cartasRepartidas.Add(carta);
            
            }

            return cartasRepartidas;
        }

        //Constructo
        public DealerBlackjack(IDeckDeCartas deck)
        {
            Deck= deck;
        }


    }
}
