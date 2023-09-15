using System;
using System.Collections.Generic;
using System.Linq;

namespace TorresHanoi
{
    class Program
    {
        static Dictionary<char, Stack<int>> torres;
        static int numDiscos = 5;

        static void Main(string[] args)
        {
            InicializarTorres(); // Inicializa las torres con discos en la torre A
            MostrarTorres(); // Muestra el estado inicial del juego

            while (!JuegoCompleto())
            {
                Console.Write("Ingrese movimiento (ejemplo: AC para mover de torre A a torre C): ");
                string movimiento = Console.ReadLine().ToUpper();

                if (movimiento.Length == 2 && torres.ContainsKey(movimiento[0]) && torres.ContainsKey(movimiento[1]))
                {
                    char origen = movimiento[0];
                    char destino = movimiento[1];

                    if (MovimientoValido(origen, destino))
                    {
                        MoverDisco(origen, destino);
                        MostrarTorres(); // Muestra el estado actualizado del juego después de cada movimiento
                    }
                    else
                    {
                        Console.WriteLine("Movimiento no válido. Inténtalo de nuevo.");
                    }
                }
                else
                {
                    Console.WriteLine("Entrada incorrecta. Por favor, ingrese un movimiento válido.");
                }
            }

            Console.WriteLine("¡Has completado el juego!");
            Console.ReadKey();
        }

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

            if (torres[destino].Count == 0)
            {
                return true; // Si la torre de destino está vacía, el movimiento es válido
            }

            int discoOrigenSuperior = torres[origen].Peek();
            int discoDestinoSuperior = torres[destino].Peek();

            return discoOrigenSuperior < discoDestinoSuperior; // Compara los discos para determinar si el movimiento es válido
        }

        static void MoverDisco(char origen, char destino)
        {
            int discoAMover = torres[origen].Pop();
            torres[destino].Push(discoAMover);
        }

        static bool JuegoCompleto()
        {
            return torres['C'].Count == numDiscos; // Verifica si todos los discos están en la última torre (C)
        }
    }
}
