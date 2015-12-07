using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    class Program
    {
        static HashSet<Tuple<int, int>> visitedPositions = new HashSet<Tuple<int, int>>();

        static string ReadInputAtOnce(string fileName)
        {
            FileStream file = File.OpenRead(fileName);
            StreamReader fileReader = new StreamReader(file);
            return fileReader.ReadToEnd();
        }

        static void Main(string[] args)
        {
            // Read in the input
            string input = ReadInputAtOnce("input.txt");
            Santa regularSanta = new Santa();
            Santa roboSanta = new Santa();
            visitedPositions.Add(new Tuple<int, int>(0,0));

            // Start reading the input
            for (int i = 0; i < input.Length; i++)
            {
                char direction = input[i];
                
                // Determine which position to navigate to
                Santa currentSanta;
                if (i%2 == 0)
                {
                    Console.Write("Regu:");
                    currentSanta = regularSanta;
                }
                else
                {
                    Console.Write("Robo:");
                    currentSanta = roboSanta;
                }

                currentSanta.UpdatePosition(direction);
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Total visited positions: {0}", visitedPositions.Count);
            Console.ReadLine();
        }

        

        private static string OutputPosition(Tuple<int, int> position)
        {
            return String.Format("({0},{1})", position.Item1, position.Item2);
        }

        private class Santa
        {
            private Tuple<int, int> currentPosition;

            public Santa()
            {
                currentPosition = new Tuple<int, int>(0,0);
            }

            public void UpdatePosition(char direction)
            {
                // Calculate the next position based on the 
                Tuple<int, int> nextPosition;
                switch (direction)
                {
                    case '^':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1);
                        Console.Write(" N --> {0}", OutputPosition(nextPosition));
                        break;
                    case 'v':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1);
                        Console.Write(" S --> {0}", OutputPosition(nextPosition));
                        break;
                    case '>':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2);
                        Console.Write(" E --> {0}", OutputPosition(nextPosition));
                        break;
                    case '<':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2);
                        Console.Write(" W --> {0}", OutputPosition(nextPosition));
                        break;
                    default:
                        Console.WriteLine("Invalid character {0}", direction);
                        return;
                }

                // Add the position to the list of visited positions
                // Have we been to the next position yet?
                if (visitedPositions.Add(nextPosition))
                {
                    Console.WriteLine(" -- NEW POSITION");
                }
                else
                {
                    Console.WriteLine();
                }

                currentPosition = nextPosition;
            }
        }
    }
}
