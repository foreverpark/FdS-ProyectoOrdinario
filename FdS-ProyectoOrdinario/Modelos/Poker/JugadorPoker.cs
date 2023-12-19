using ProyectoOrdinario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class JugadorPoker : IJugador
    {
        //Propiedades
        public List<ICarta> Mano;
        private IDealer Dealer;

        //Constructor
        public JugadorPoker(IDealer dealer)
        {
            Mano = new List<ICarta>();
            Dealer = dealer;
        }

        //Métodos
        public ICarta DevolverCarta(int indiceCarta)
        {
            var CartaDevuelta = Mano[indiceCarta];
            Mano.RemoveAt(indiceCarta);
            return CartaDevuelta;
        }

        public List<ICarta> DevolverTodasLasCartas()
        {
            List<ICarta> CartasDevueltas = new(Mano);
            Mano.Clear();
            return CartasDevueltas;
        }

        public ICarta MostrarCarta(int indiceCarta)
        {
            return Mano[indiceCarta];
        }

        public List<ICarta> MostrarCartas()
        {
            return new List<ICarta>(Mano);
        }

        public void ObtenerCartas(List<ICarta> cartas)
        {
            foreach (var carta in cartas)
            {
                Mano.Add(carta);
            }
        }

        public void RealizarJugada()
        {
            //throw new NotImplementedException();
            Random instanciaRandom = new();
            if (instanciaRandom.Next(11) > 5)
            {
                List<ICarta> cartasParaDevolver = new List<ICarta>();
                int numeroCartasDevueltas = 1;
                var cartaElegida = MostrarCarta(instanciaRandom.Next(5));
                cartasParaDevolver.Add(cartaElegida);
                Mano.Remove(cartaElegida);
                Console.WriteLine($"Regresa {cartaElegida.Valor} de {cartaElegida.Figura}");

                if (instanciaRandom.Next(10) > 5)
                {
                    numeroCartasDevueltas++;
                    cartaElegida = MostrarCarta(instanciaRandom.Next(4));
                    cartasParaDevolver.Add(cartaElegida);
                    Mano.Remove(cartaElegida);
                    Console.WriteLine($"Regresa {cartaElegida.Valor} de {cartaElegida.Figura}");

                    if (instanciaRandom.Next(9) > 5)
                    {
                        numeroCartasDevueltas++;
                        cartaElegida = MostrarCarta(instanciaRandom.Next(3));
                        cartasParaDevolver.Add(cartaElegida);
                        Mano.Remove(cartaElegida);
                        Console.WriteLine($"Regresa {cartaElegida.Valor} de {cartaElegida.Figura}");

                        if (instanciaRandom.Next(8) > 5)
                        {
                            numeroCartasDevueltas++;
                            cartaElegida = MostrarCarta(instanciaRandom.Next(2));
                            cartasParaDevolver.Add(cartaElegida);
                            Mano.Remove(cartaElegida);
                            Console.WriteLine($"Regresa {cartaElegida.Valor} de {cartaElegida.Figura}");

                            if (instanciaRandom.Next(7) > 5)
                            {
                                numeroCartasDevueltas++;
                                cartaElegida = MostrarCarta(0);
                                cartasParaDevolver.Add(cartaElegida);
                                Mano.Remove(cartaElegida);
                                Console.WriteLine($"Regresa {cartaElegida.Valor} de {cartaElegida.Figura}");
                            }
                        }

                    }
                }

                Dealer.RecogerCartas(cartasParaDevolver);
                ObtenerCartas(Dealer.RepartirCartas(numeroCartasDevueltas));
            } else
            {
                Console.WriteLine("No regresa ninguna carta.");
            }

        }
    }
}
