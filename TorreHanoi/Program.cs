using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TorresHanoi
{
    class Program
    {
        // Declaramos una estructura de datos para representar las torres y el número de discos.
        static Dictionary<char, Stack<int>> torres;
        static int numDiscos = 5;

        static void Main(string[] args)
        {
            InicializarTorres(); // Inicializamos las torres con discos en la torre A
            MostrarTorres(); // Mostramos el estado inicial del juego

            bool juegoCompleto = false;

            do
            {
                Console.Write("Ingrese movimiento (ejemplo: AC para mover de torre A a torre C, 'R' para resolver automáticamente): ");
                string movimiento = Console.ReadLine().ToUpper();

                if (movimiento == "R")
                {
                    EjecutarIA(); // Si el usuario ingresa 'R', ejecutamos la IA para resolver automáticamente
                    juegoCompleto = true; // Marcamos el juego como completo
                }
                else if (movimiento.Length == 2 && torres.ContainsKey(movimiento[0]) && torres.ContainsKey(movimiento[1]))
                {
                    char origen = movimiento[0];
                    char destino = movimiento[1];

                    if (MovimientoValido(origen, destino))
                    {
                        MoverDisco(origen, destino); // Realizamos el movimiento válido
                        MostrarTorres(); // Mostramos el estado actualizado del juego después de cada movimiento

                        if (JuegoCompleto())
                        {
                            juegoCompleto = true; // Verificamos si el juego ha sido completado
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Movimiento no válido. Inténtalo de nuevo.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada incorrecta. Por favor, ingrese un movimiento válido o 'R' para resolver automáticamente.");
                }

            } while (!juegoCompleto);

            if (juegoCompleto)
            {
                Console.WriteLine("¡Has completado el juego!"); // Mostramos un mensaje si el juego ha sido completado
            }

            Console.ReadKey();
        }

        // Inicializa las torres con discos en la torre A de mayor a menor.
        static void InicializarTorres()
        {
            torres = new Dictionary<char, Stack<int>>();

            for (char c = 'A'; c <= 'C'; c++)
            {
                torres[c] = new Stack<int>();
            }

            for (int i = numDiscos; i >= 1; i--)
            {
                torres['A'].Push(i); // Coloca los discos en la torre A de mayor a menor
            }
        }

        // Muestra el estado actual de las torres en la consola.
        static void MostrarTorres()
        {
            Console.Clear();

            Console.WriteLine("Torres de Hanoi");
            Console.WriteLine("---------------");

            for (int i = numDiscos; i >= 1; i--)
            {
                foreach (var torre in torres)
                {
                    char nombreTorre = torre.Key;
                    Stack<int> discos = torre.Value;

                    int disco = discos.Count >= i ? discos.ToArray()[discos.Count - i] : 0;
                    string representacionDisco = new string('X', disco);

                    // Añadir espacios para centrar los discos
                    string discoFormateado = representacionDisco.PadLeft(numDiscos + 2).PadRight(numDiscos * 2 + 2);

                    Console.Write($" {nombreTorre} | {discoFormateado} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  |  B  |  C  ");
        }

        // Verifica si un movimiento es válido.
        static bool MovimientoValido(char origen, char destino)
        {
            if (!torres.ContainsKey(origen) || !torres.ContainsKey(destino))
            {
                return false; // Verifica que las torres sean válidas
            }

            if (torres[origen].Count == 0)
            {
                return false; // Verifica que la torre de origen no esté vacía
            }

            if (torres[destino].Count == 0 || torres[origen].Peek() < torres[destino].Peek())
            {
                return true; // Verifica que el movimiento sea válido
            }

            return false;
        }

        // Realiza un movimiento de disco desde una torre a otra.
        static void MoverDisco(char origen, char destino)
        {
            int discoAMover = torres[origen].Pop();
            torres[destino].Push(discoAMover);
        }

        // Verifica si el juego ha sido completado (todos los discos en la torre C).
        static bool JuegoCompleto()
        {
            return torres['C'].Count == numDiscos; // Verifica si todos los discos están en la última torre (C)
        }

        // Ejecuta la IA para resolver automáticamente el juego.
        static void EjecutarIA()
        {
            Console.WriteLine("Resolviendo automáticamente...");
            ResolverHanoi(numDiscos, 'A', 'C', 'B');
            MostrarTorres();
            Console.WriteLine("¡Resolución automática completada!");
            Console.ReadKey();
        }

        // Función recursiva para resolver automáticamente el juego siguiendo las reglas de las Torres de Hanoi.
        static void ResolverHanoi(int n, char origen, char destino, char auxiliar)
        {
            if (n > 0)
            {
                ResolverHanoi(n - 1, origen, auxiliar, destino);

                if (torres[origen].Count > 0 && (torres[destino].Count == 0 || torres[origen].Peek() < torres[destino].Peek()))
                {
                    MoverDisco(origen, destino); // Realizamos el movimiento válido de la IA
                    Thread.Sleep(500); // Agregamos un pequeño retraso para hacer el movimiento visible
                    MostrarTorres();
                }

                ResolverHanoi(n - 1, auxiliar, destino, origen);
            }
        }
    }
}
