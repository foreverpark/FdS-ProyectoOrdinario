using ProyectoOrdinario.Interfaces;
using System.Collections.Generic;

namespace FdS_ProyectoOrdinario.Modelos.Poker
{
    internal class JuegoPoker : IJuego
    {
        private List<IJugador> Jugadores { get; }
        public IDealer Dealer { get; }

        public bool JuegoTerminado { get; }

        public JuegoPoker()
        {
            Dealer = new DealerPoker();
            Jugadores = new List<IJugador>();
        }


        public void AgregarJugador(IJugador jugador)
        {
            Jugadores.Add(jugador);
        }

        public void IniciarJuego()
        {
            Dealer.BarajearDeck();

            foreach (var jugador in Jugadores)
            {
                jugador.ObtenerCartas(Dealer.RepartirCartas(5));
            }

            int numeroJugador = 1;
            foreach (var jugador in Jugadores)
            {
                Console.WriteLine($"Jugador {numeroJugador}:");
                var Mano = jugador.MostrarCartas();
                int numeroCarta = 1;
                foreach (var carta in Mano)
                {
                    Console.WriteLine($"Carta {numeroCarta}: {carta.Valor} de {carta.Figura}");
                    numeroCarta += 1;
                }
                numeroJugador += 1;
                Console.WriteLine();
            }
            Console.ReadKey();
            Console.Clear();

            JugarRonda();
        }

        public void JugarRonda()
        {
            int numeroJugador = 1;
            foreach (var jugador in Jugadores)
            {
                Console.WriteLine($"Turno del Jugador {numeroJugador}");
                jugador.RealizarJugada();
                Console.ReadKey();
                Console.Clear();
                numeroJugador++;
            }

            numeroJugador = 1;
            foreach (var jugador in Jugadores)
            {
                Console.WriteLine($"Jugador {numeroJugador}:");
                var Mano = jugador.MostrarCartas();
                int numeroCarta = 1;
                foreach (var carta in Mano)
                {
                    Console.WriteLine($"Carta {numeroCarta}: {carta.Valor} de {carta.Figura}");
                    numeroCarta += 1;
                }
                numeroJugador += 1;
                Console.WriteLine();
            }

            MostrarGanador();
            Console.ReadKey();
        }

        public void MostrarGanador()
        {
            var JugadorGanador = ConseguirGanador();
            int contador = 0;
            int ganador = 0;
            foreach (var jugador in Jugadores)
            {
                if (JugadorGanador == jugador)
                {
                    ganador = contador;
                }
                contador++;
            }

            Console.WriteLine($"Ganó el Jugador {ganador}");
        }

        public IJugador ConseguirGanador()
        {

            //Escalera Real
            int contador = 0;
            List<bool> palosIguales = new List<bool>();
            foreach (JugadorPoker jugador in Jugadores)
            {
                palosIguales.Add(false);
                int primerPalo = (int)jugador.Mano[0].Figura;
                foreach (ICarta carta in jugador.Mano)
                {
                    if ((int)carta.Figura == primerPalo)
                    {
                        palosIguales[contador] = true;
                    }
                }

                if (palosIguales[contador])
                {
                    bool tieneAs = false;
                    bool tieneDiez = false;
                    bool tieneJota = false;
                    bool tieneReina = false;
                    bool tieneRey = false;

                    foreach (ICarta carta in jugador.Mano)
                    {
                        switch ((int)carta.Valor)
                        {
                            case 1:
                                tieneAs = true;
                                break;
                            case 10:
                                tieneDiez = true;
                                break;
                            case 11:
                                tieneJota = true;
                                break;
                            case 12:
                                tieneReina = true;
                                break;
                            case 13:
                                tieneRey = true;
                                break;
                            default:
                                break;
                        }
                    }

                    if (tieneAs && tieneDiez && tieneJota && tieneReina && tieneRey)
                    {
                        return jugador;
                    }
                }
                contador++;
            }

            //Escalera de color
            contador = 0;
            List<bool> tieneEscalera = new List<bool>();
            List<bool> tieneEscaleraDeColor = new List<bool>();
            List<int> cartaMayor = new List<int>();
            foreach (JugadorPoker jugador in Jugadores)
            {
                tieneEscalera.Add(false);
                tieneEscaleraDeColor.Add(false);
                cartaMayor.Add(0);

                int valorMayor = 0;
                int valorMenor = 14;
                foreach (ICarta carta in jugador.Mano)
                {
                    List<int> valoresCartas = new List<int>();

                    if (valoresCartas.Contains(valorMenor))
                    {
                        //Tiene dos valores iguales.
                    }
                    else
                    {
                        if ((int)carta.Valor > valorMayor)
                        {
                            valorMayor = (int)carta.Valor;
                        }

                        if ((int)carta.Valor < valorMenor)
                        {
                            valorMenor = (int)carta.Valor;
                        }
                        cartaMayor[contador] = valorMayor;

                        int diferencia = valorMayor - valorMenor;
                        if (diferencia == 4)
                        {
                            tieneEscalera[contador] = true;
                            if (palosIguales[contador])
                            {
                                tieneEscaleraDeColor[contador] = true;
                            }
                        }
                    }
                }
                
                contador++;
            }

            contador = 0;
            int escaleraDeColorMayor = -1;
            foreach (JugadorPoker jugador in Jugadores)
            {
                int valorMayor = 0;
                if (tieneEscaleraDeColor[contador])
                {
                    if (cartaMayor[contador] > valorMayor)
                    {
                        escaleraDeColorMayor = contador;
                        valorMayor = cartaMayor[contador];
                    }
                }
                contador++;
            }

            if (escaleraDeColorMayor != -1)
            {
                return Jugadores[escaleraDeColorMayor];
            }

            //Four of a kind y Full
            List<bool> tieneFourOfAKind = new List<bool>();
            List<bool> tieneFull = new List<bool>();
            List<int> valorFourOfAKind = new List<int>();
            List<int> valorFull = new List<int>();
            contador = 0;

            foreach (JugadorPoker jugador in Jugadores)
            {
                valorFull.Add(0);
                valorFourOfAKind.Add(0);
                tieneFourOfAKind.Add(false);
                tieneFull.Add(false);
                List<int> valores1 = new List<int>();
                List<int> valores2 = new List<int>();
                int valorInicial1 = (int)jugador.Mano[0].Valor;
                int valorInicial2 = (int)jugador.Mano[1].Valor;

                foreach (ICarta carta in jugador.Mano)
                {
                    if (valorInicial1 == (int)carta.Valor)
                    {
                        valores1.Add((int)carta.Valor);
                    }
                    else if (valorInicial2 == (int)carta.Valor)
                    {
                        valores2.Add((int)carta.Valor);
                    }

                    //Si es Four of a Kind
                    if (valores1.Count() == 4 || valores2.Count() == 4)
                    {
                        tieneFourOfAKind[contador] = true;

                        if (valores1.Count() == 4)
                        {
                            valorFourOfAKind[contador] = valores1[0];
                        }
                        else
                        {
                            valorFourOfAKind[contador] = valores2[0];
                        }
                    }

                    //Si es Full
                    if ((valores1.Count() == 3 && valores2.Count() == 2)
                        || (valores1.Count() == 2 && valores2.Count() == 3))
                    {
                        tieneFull[contador] = true;
                        if (valores1.Count() == 3)
                        {
                            valorFull[contador] = valores1[0];
                        }
                        else
                        {
                            valorFull[contador] = valores2[0];
                        }
                    }
                }
                contador++;
            }

            contador = 0;
            int kindMayor = -1;
            int valorKindMayor = 0;

            foreach (bool fourofAKind in tieneFourOfAKind)
            {

                if (fourofAKind)
                {
                    if (valorFourOfAKind[contador] > valorKindMayor)
                    {
                        kindMayor = contador;
                        valorKindMayor = valorFourOfAKind[contador];
                    }
                }
                contador++;
            }

            if (kindMayor != -1)
            {
                return Jugadores[kindMayor];
            }


            contador = 0;
            int fullMayor = -1;
            int valorFullMayor = 0;

            foreach (bool full in tieneFull)
            {

                if (full)
                {
                    if (valorFull[contador] > valorFullMayor)
                    {
                        fullMayor = contador;
                        valorFullMayor = valorFull[contador];
                    }
                }
                contador++;
            }

            if (fullMayor != -1)
            {
                return Jugadores[fullMayor];
            }

            //Color
            contador = 0;
            int valorColorMayor = 0;
            int colorMayor = -1;
            foreach (bool palo in palosIguales)
            {
                if (palo)
                {
                    if (cartaMayor[contador] > valorColorMayor)
                    {
                        colorMayor = contador;
                        valorColorMayor = cartaMayor[contador];
                    }
                }
                contador++;
            }

            if (colorMayor != -1)
            {
                return Jugadores[colorMayor];
            }


            contador = 0;
            int escaleraMayor = -1;
            foreach (bool escalera in tieneEscalera)
            {
                int valorMayor = 0;
                if (tieneEscalera[contador])
                {
                    if (cartaMayor[contador] > valorMayor)
                    {
                        escaleraMayor = contador;
                        valorMayor = cartaMayor[contador];
                    }
                }
                contador++;
            }

            if (escaleraMayor != -1)
            {
                return Jugadores[escaleraMayor];
            }


            

            return null;
        }
    }
}
