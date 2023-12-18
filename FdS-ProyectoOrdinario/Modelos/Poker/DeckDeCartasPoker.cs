using FdS_ProyectoOrdinario.Modelos.Blackjack;
using ProyectoOrdinario.Enumeradores;
using ProyectoOrdinario.Interfaces;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class DeckDeCartasPoker : IDeckDeCartas
    {
       
        public List<CartaPoker> Cartas;

        
        public DeckDeCartasPoker()
        {
            Cartas = new List<CartaPoker>();

            foreach (FigurasCartasEnum figuras in Enum.GetValues(typeof(FigurasCartasEnum)))
            {
                foreach (ValoresCartasEnum valores in Enum.GetValues(typeof(ValoresCartasEnum)))
                {
                    Cartas.Add(new CartaPoker(figuras, valores));
                }
            }
        }

        
        public void BarajearDeck()
        {
            Random instanciaRandom = new Random();
            int cartasPorBarajear = Cartas.Count;
            while (cartasPorBarajear > 1)
            {
                cartasPorBarajear--;

                int numeroAleatorio = instanciaRandom.Next(cartasPorBarajear + 1);
                CartaPoker carta = Cartas[numeroAleatorio];

                Cartas[numeroAleatorio] = Cartas[cartasPorBarajear];

                Cartas[cartasPorBarajear] = carta;
            }
        }

        public void MeterCarta(ICarta carta)
        {
            Cartas.Add((CartaPoker)carta);
        }

        public void MeterCarta(List<ICarta> cartas)
        {
            Cartas.AddRange((IEnumerable<CartaPoker>)cartas);
        }

        public ICarta SacarCarta(int indiceCarta)
        {
            CartaPoker cartaSacada = Cartas[indiceCarta];
            Cartas.RemoveAt(indiceCarta);
            return cartaSacada;
        }

        public ICarta VerCarta(int indiceCarta)
        {
            return Cartas[indiceCarta];
        }
    }
}
