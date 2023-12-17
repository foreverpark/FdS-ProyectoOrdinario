using ProyectoOrdinario.Interfaces;
using ProyectoOrdinario.Enumeradores;

namespace FdS_ProyectoOrdinario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();

            var carta = deck.SacarCarta(0);

            Console.WriteLine($"{carta.Valor} de {carta.Figura}"); 
        }
    }

    public class Deck: IDeckDeCartas
    {
        List<ICarta> cartas;

        public Deck()
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
                    cartas.Add(new Carta_BlackJack(figuras, valores));
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
    public class Carta_BlackJack: ICarta
    {
        public FigurasCartasEnum Figura { get; }

        public ValoresCartasEnum Valor { get; }
        public Carta_BlackJack(FigurasCartasEnum figura, ValoresCartasEnum valor)
        {
            Figura = figura;
            Valor = valor;  
        }
    }
}