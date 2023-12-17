namespace FdS_ProyectoOrdinario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Adios Anderson");
        }
    }

    public class Deck
    {
        List<Carta_BlackJack> cartas;

        public Deck()
        {
            CrearDeck();
            MezclarDeck();
        }

        private void CrearDeck()
        {
            cartas = new List<Carta_BlackJack>();

            //se hace un foreach a los enumeradores para asignarlos a una carta y este sea añadido a una lista de cartas(deck)
            foreach (Carta_BlackJack.FigurasEnum figuras in Enum.GetValues(typeof(Carta_BlackJack.FigurasEnum)))
            {
                foreach (Carta_BlackJack.ValoresEnum valores in Enum.GetValues(typeof(Carta_BlackJack.ValoresEnum)))
                {
                    cartas.Add(new Carta_BlackJack(figuras, valores));
                }
            }
        }

        private void MezclarDeck()
        {
            Random random = new Random();
            int numeroDeCartas = cartas.Count;
            while (numeroDeCartas > 1) 
            {
                numeroDeCartas--;

                int numeroRandom = random.Next(numeroDeCartas + 1);
                Carta_BlackJack carta = cartas[numeroRandom];

                cartas[numeroRandom] = cartas[numeroDeCartas];

                cartas[numeroDeCartas] = carta; 
            }
        }

        public Carta_BlackJack RepartirCarta()
        {
            Carta_BlackJack carta = cartas[0];
            cartas.RemoveAt(0);
            return carta;
        }

    }
    public class Carta_BlackJack
    {
        public FigurasEnum Figura { get; }

        public ValoresEnum Valor { get; }
        public Carta_BlackJack(FigurasEnum figura, ValoresEnum valor)
        {
            Figura = figura;
            Valor = valor;  
        }

        public enum FigurasEnum
        {
            Corazones,
            Diamantes,
            Treboles,
            Picas
        }
        public enum ValoresEnum
        {
            As = 1,
            Dos = 2,
            Tres = 3,
            Cuatro = 4,
            Cinco = 5,
            Seis = 6,
            Siete = 7,
            Ocho = 8,
            Nueve = 9,
            Diez = 10,
            Jota = 11,
            Reina = 12,
            Rey = 13,
        }

        public void MostrarCarta()
        {
            Console.WriteLine($"{Valor} de {Figura}");
        }
    }
}