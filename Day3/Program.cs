using System;
using System.Collections.Generic;
using System.IO;

namespace Day3
{
    class Program
    {
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
            Tuple<int, int> currentPosition = new Tuple<int, int>(0, 0);
            HashSet<Tuple<int, int>> visitedPositions = new HashSet<Tuple<int, int>> {currentPosition};

            Console.WriteLine("Starting at {0}", OutputPosition(currentPosition));

            // Start reading the input
            foreach (char direction in input)
            {
                // Determine which position to navigate to
                Tuple<int, int> nextPosition;
                switch (direction)
                {
                    case '^':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 + 1);
                        Console.Write("N --> {0}", OutputPosition(nextPosition));
                        break;
                    case 'v':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1, currentPosition.Item2 - 1);
                        Console.Write("S --> {0}", OutputPosition(nextPosition));
                        break;
                    case '>':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1 + 1, currentPosition.Item2);
                        Console.Write("E --> {0}", OutputPosition(nextPosition));
                        break;
                    case '<':
                        nextPosition = new Tuple<int, int>(currentPosition.Item1 - 1, currentPosition.Item2);
                        Console.Write("W --> {0}", OutputPosition(nextPosition));
                        break;
                    default:
                        Console.WriteLine("Invalid character {0}", direction);
                        continue;
                }

                // Have we been to the next position yet?
                if (!visitedPositions.Add(nextPosition))
                {
                    Console.WriteLine(" -- NEW POSITION");
                }
                else
                {
                    Console.WriteLine();
                }
                currentPosition = nextPosition;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Total visited positions: {0}", visitedPositions.Count);
            Console.ReadLine();
        }

        private static string OutputPosition(Tuple<int, int> position)
        {
            return String.Format("({0},{1})", position.Item1, position.Item2);
        }
    }
}
