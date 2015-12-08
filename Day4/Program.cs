using System;
using System.Text;
using System.Security.Cryptography;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            const string input = "iwrupvqb";
            string successfulValue = null;
            long testNumber = 0;
            while (successfulValue == null)
            {
                string testValue = input + testNumber;
                Console.WriteLine("Testing {0}", testValue);
                string testHash = CalculateHash(testValue);
                if (testHash.StartsWith("000000"))
                {
                    successfulValue = testValue;
                }
                testNumber++;
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Successful value is: {0}", successfulValue);
            Console.ReadLine();
        }

        static string CalculateHash(string input)
        {
            MD5 hasher = new MD5Cng();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = hasher.ComputeHash(inputBytes);
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
