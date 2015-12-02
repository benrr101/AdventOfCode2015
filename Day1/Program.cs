using System;
using System.IO;

namespace Day1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Load up the input file
            FileStream inputStream = File.OpenRead("input.txt");
            StreamReader inputReader = new StreamReader(inputStream);
            string input = inputReader.ReadToEnd();

            // Set up the counter
            int floor = 0;
            Console.WriteLine("Starting at 0");
            
            // Start reading the floors
            foreach (char c in input)
            {
                switch (c)
                {
                    case '(':
                        floor++;
                        Console.WriteLine("Going up to {0}", floor);
                        break;
                    case ')':
                        floor--;
                        Console.WriteLine("Going down to {0}", floor);
                        break;
                    default:
                        Console.WriteLine("Invalid character: {0}", c);
                        break;
                }
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Final Floor: {0}", floor);
            Console.ReadLine();
        }
    }
}
