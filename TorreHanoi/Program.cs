using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace TorresHanoi
{
    class Program
    {
        static Dictionary<char, Stack<int>> towers; // Declara un diccionario de torres
        static int numDiscos = 5; 
        static void Main(string[] args)
        {
            InitializeTowers(); 
            ShowTowers(); 

            while (!IsGameComplete()) // Realiza accion hasta que termine el juego
            {
                Console.Write("Ingrese movimiento (ejemplo: AC para mover de torre A a torre C): ");
                string move = Console.ReadLine().ToUpper(); 

                if (move.Length == 2 && towers.ContainsKey(move[0]) && towers.ContainsKey(move[1]))
                {
                    char source = move[0]; // Obtiene la torre de origen
                    char destination = move[1]; // Obtiene la torre de destino

                    if (IsValidMove(source, destination)) // Comprueba si el movimiento es válido
                    {
                        MoveDisk(source, destination); // Realiza el movimiento del disco
                        ShowTowers(); // Muestra el estado actualizado de las torres
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

        static void InitializeTowers()
        {
            towers = new Dictionary<char, Stack<int>>(); // Inicializa el diccionario de torres

            for (char c = 'A'; c <= 'C'; c++) // Crea las tres torres (A, B, C) como pilas 
            {
                towers[c] = new Stack<int>();
            }

            for (int i = numDiscos; i >= 1; i--) // Coloca los discos en la primera torre, de mayor a menor
            {
                towers['A'].Push(i);
            }
        }

        static void ShowTowers() //Muestra las torres
        {
            Console.Clear(); 

            Console.WriteLine("-----|-----|-----");
            for (int i = 1; i <= numDiscos; i++)
            {
                foreach (var tower in towers)
                {
                    char towerName = tower.Key; 
                    Stack<int> disks = tower.Value; 
                    int disk = disks.Count >= i ? disks.ToArray()[i - 1] : 0; 
                    string diskDisplay = new string('X', disk); //Representacion del disco (X)

                    Console.Write($"{towerName}: {diskDisplay.PadLeft(numDiscos)} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A  |  B  |  C  "); // Etiquetas de las torres
        }

        static bool IsValidMove(char source, char destination)
        {
            if (!towers.ContainsKey(source) || !towers.ContainsKey(destination)) // Verifica que las torres sean válidas
            {
                return false;
            }

            if (towers[source].Count == 0) // Verifica que la torre de origen no esté vacía
            {
                return false;
            }

            if (towers[destination].Count == 0) // Si la torre de destino está vacía, el movimiento es válido
            {
                return true;
            }

            int sourceTopDisk = towers[source].Peek(); 
            int destinationTopDisk = towers[destination].Peek(); 

            return sourceTopDisk < destinationTopDisk; // Compara los discos para determinar si el movimiento es válido
        }

        static void MoveDisk(char source, char destination)
        {
            int diskToMove = towers[source].Pop(); 
            towers[destination].Push(diskToMove); 
        }

        static bool IsGameComplete()
        {
            return towers['C'].Count == numDiscos; // Verifica si todos los discos están en la última torre (C)
        }
    }
}
