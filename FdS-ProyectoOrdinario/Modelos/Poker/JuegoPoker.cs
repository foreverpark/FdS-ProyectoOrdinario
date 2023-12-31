﻿using ProyectoOrdinario.Interfaces;

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
            int contador = 1;
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
                palosIguales.Add(true);
                int primerPalo = (int)jugador.Mano[0].Figura;
                foreach (ICarta carta in jugador.Mano)
                {
                    if ((int)carta.Figura != primerPalo)
                    {
                        palosIguales[contador] = false;
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
                        return Jugadores[contador];
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
                    int numeroCarta = 0;
                    List<int> valoresCartas = new List<int>();

                    if (valoresCartas.Contains(valorMenor))
                    {
                        break; //Tiene dos valores iguales.
                    }
                    else
                    {
                        valoresCartas.Add((int)carta.Valor);
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
                        if (numeroCarta == 4 && diferencia == 4)
                        {
                            tieneEscalera[contador] = true;
                            if (palosIguales[contador])
                            {
                                tieneEscaleraDeColor[contador] = true;
                            }
                        }
                    }
                    numeroCarta++;
                }

                contador++;
            }

            contador = 0;
            int escaleraDeColorMayor = -1;
            int valorEscaleraDeColorMayor = 0;
            foreach (JugadorPoker jugador in Jugadores)
            {
                if (tieneEscaleraDeColor[contador])
                {
                    if (cartaMayor[contador] > valorEscaleraDeColorMayor)
                    {
                        escaleraDeColorMayor = contador;
                        valorEscaleraDeColorMayor = cartaMayor[contador];
                    }
                }
                contador++;
            }

            if (escaleraDeColorMayor != -1)
            {
                return Jugadores[escaleraDeColorMayor];
            }

            //Four of a kind o Full
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
            int valorEscaleraMayor = 0;
            foreach (bool escalera in tieneEscalera)
            {
                if (tieneEscalera[contador])
                {
                    if (cartaMayor[contador] > valorEscaleraMayor)
                    {
                        escaleraMayor = contador;
                        valorEscaleraMayor = cartaMayor[contador];
                    }
                }
                contador++;
            }

            if (escaleraMayor != -1)
            {
                return Jugadores[escaleraMayor];
            }

            //Trío, pareja o doble pareja
            List<bool> tienePar = new List<bool>();
            List<bool> tieneDosPares = new List<bool>();
            List<int> valorTrioOPar = new List<int>();
            List<bool> tieneTrio = new List<bool>();
            List<List<int>> paresOrdenados = new List<List<int>>();
            contador = 0;
            foreach (JugadorPoker jugador in Jugadores)
            {
                tieneDosPares.Add(false);
                tieneTrio.Add(false);
                tienePar.Add(false);
                valorTrioOPar.Add(0);

                int[] contadorNumeros = new int[14];
                foreach (ICarta carta in jugador.Mano)
                {
                    contadorNumeros[(int)carta.Valor]++;
                    if (contadorNumeros[(int)carta.Valor] == 2)
                    {
                        tienePar[contador] = true;
                    }
                    else if (contadorNumeros[(int)carta.Valor] == 3)
                    {
                        tieneTrio[contador] = true;
                    }
                }

                /*Por alguna razón no tenía manera de conseguir el índice
                 * del valor mayor de la lista, por lo que busqué y encontre
                 * esta cosa horrorosa. Lo que intenté al principio no
                 * funcionó. Tengo sueño.
                 */
                int numeroMasRepetido = contadorNumeros.Select((value, index) => new { Value = value, Index = index })
                              .OrderByDescending(item => item.Value)
                              .First().Index;
                valorTrioOPar[contador] = numeroMasRepetido;

                tieneDosPares[contador] = contadorNumeros.Count(count => count == 1) == 2;
                if (tieneDosPares[contador] == false)
                {
                    tieneDosPares[contador] = contadorNumeros.Count(count => count == 2) == 2;
                    if (tieneDosPares[contador] == false)
                    {
                        tieneDosPares[contador] = contadorNumeros.Count(count => count == 3) == 2;
                        if (tieneDosPares[contador] == false)
                        {
                            tieneDosPares[contador] = contadorNumeros.Count(count => count == 4) == 2;
                            if (tieneDosPares[contador] == false)
                            {
                                tieneDosPares[contador] = contadorNumeros.Count(count => count == 5) == 2;
                                if (tieneDosPares[contador] == false)
                                {
                                    tieneDosPares[contador] = contadorNumeros.Count(count => count == 6) == 2;
                                    if (tieneDosPares[contador] == false)
                                    {
                                        tieneDosPares[contador] = contadorNumeros.Count(count => count == 7) == 2;
                                        if (tieneDosPares[contador] == false)
                                        {
                                            tieneDosPares[contador] = contadorNumeros.Count(count => count == 8) == 2;
                                            if (tieneDosPares[contador] == false)
                                            {
                                                tieneDosPares[contador] = contadorNumeros.Count(count => count == 9) == 2;
                                                if (tieneDosPares[contador] == false)
                                                {
                                                    tieneDosPares[contador] = contadorNumeros.Count(count => count == 10) == 2;
                                                    if (tieneDosPares[contador] == false)
                                                    {
                                                        tieneDosPares[contador] = contadorNumeros.Count(count => count == 11) == 2;
                                                        if (tieneDosPares[contador] == false)
                                                        {
                                                            tieneDosPares[contador] = contadorNumeros.Count(count => count == 12) == 2;
                                                            if (tieneDosPares[contador] == false)
                                                            {
                                                                tieneDosPares[contador] = contadorNumeros.Count(count => count == 13) == 2;

                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (tieneDosPares[contador])
                {
                    List<int> parOrdenados = Enumerable.Range(0, 2).OrderByDescending(numero => contadorNumeros[numero]).ToList();
                    paresOrdenados.Add(parOrdenados);
                }
                else
                {
                    paresOrdenados.Add(null);
                }
                contador++;
            }

            contador = 0;
            int trioMayor = -1;
            int valorTrioMayor = 0;
            foreach (bool trio in tieneTrio)
            {
                if (tieneTrio[contador])
                {
                    if (valorTrioOPar[contador] > valorTrioMayor)
                    {
                        trioMayor = contador;
                        valorTrioMayor = valorTrioOPar[contador];
                    }
                }
                contador++;
            }
            if (trioMayor != -1)
            {
                return Jugadores[trioMayor];
            }


            contador = 0;
            int dobleParMayor = -1;
            int valorParDobleMayor = 0;
            foreach (bool doblePar in tieneDosPares)
            {
                if (paresOrdenados[contador] != null)
                {
                    int sumaPares = paresOrdenados[contador][0] + paresOrdenados[contador][1];
                    if (tieneDosPares[contador])
                    {
                        if (sumaPares > valorParDobleMayor)
                        {
                            valorParDobleMayor = sumaPares;
                            dobleParMayor = contador;
                        }
                    }
                }
                contador++;
            }

            if (dobleParMayor != -1)
            {
                return Jugadores[dobleParMayor];
            }


            contador = 0;
            int parMayor = -1;
            int valorParMayor = 0;
            foreach (bool par in tienePar)
            {
                if (tienePar[contador])
                {
                    if (valorTrioOPar[contador] > valorParMayor)
                    {
                        parMayor = contador;
                        valorParMayor = valorTrioOPar[contador];
                    }
                }
                contador++;
            }
            if (parMayor != -1)
            {
                return Jugadores[parMayor];
            }

            contador = 0;
            int mayorCarta = -1;
            int valorMayorCarta = 0;
            foreach(JugadorPoker jugador in Jugadores)
            {
                foreach (ICarta carta in jugador.Mano)
                {
                    if ((int)carta.Valor > valorMayorCarta)
                    {
                        mayorCarta = contador;
                        valorMayorCarta = (int)carta.Valor;
                    }
                }
                contador++;
            }

            return Jugadores[mayorCarta];
        }
    }
}
