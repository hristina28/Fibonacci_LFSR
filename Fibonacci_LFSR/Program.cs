using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fibonacci_LFSR
{
    internal class Program
    {
        public static List<int> outputList = new List<int>();
        public static string[] array = new string[8];
        
        public static void LfsrFibonacci()
        {
            GetPolynomial();
            PrintPolynomial();
            int seedValue = GetSeed();
            int lfsrValue = seedValue;
            int inputBit;
            int counter = 0;           
            Console.WriteLine("Seed value: " );
            toBits(seedValue);

            do
            {
                inputBit = lfsrValue >> 0;
                for (int i = 1; i < 8; i++)
                {
                    if (array[i] == "1")
                    {
                        inputBit ^= lfsrValue >> i;
                    }
                }
                inputBit &= 1;
     
                lfsrValue = (lfsrValue >> 1) | (inputBit << 7);
                toBits(lfsrValue);               
                counter++;
            }
            while (lfsrValue != seedValue);


            Console.Write("\nPeriod of the polynomial ");
            Print();
            Console.WriteLine(" -> {0}", counter);
            if(counter == 255)
                Console.WriteLine("The polynomial has full period, so it is primitive.");
            Console.Write("\nGenerated random bit sequence: ");
            outputList.ForEach(Console.Write);            
        }

        public static void GetPolynomial()
        {
            Console.WriteLine("Enter 0 or 1 for the coefficients: ");
            for (int i = 8; i > 0; i--)
            {
                Console.Write("x^{0} = ", i);
                string choice = Console.ReadLine();
                if (choice == "0" || choice == "1")
                {
                    array[-(i - 8)] = choice;
                }
                else
                {
                    Console.WriteLine("Only 0 or 1: ");
                    Console.Write("x^{0} = ", i);
                    choice = Console.ReadLine();
                    array[-(i - 8)] = choice;
                }
                PrintFeedbackPolinomial();
            }
        }

        public static void PrintFeedbackPolinomial()
        {
            Console.Clear();
            for (int i = 8; i > 0; i--)
            {
                Console.Write("{0}*x^{1} + ", array[-(i - 8)], i);

            }
            Console.WriteLine("1");
        }

        public static void PrintPolynomial()
        {
            Console.Write("\n1");
            for (int i = 7; i >= 0; i--)
            {
                Console.Write(" + {0}*x^{1}", array[i], -(i - 8));
            }
            Console.WriteLine("\n");

        }

        public static int GetSeed()
        {
            return Environment.TickCount & byte.MaxValue;
        }


        public static void toBits(int result)
        {
            int value = result;            
            bool b = GetLastBit(value, 0);
            int bit = Convert.ToInt32(b);
            outputList.Add(bit);

            string binary = Convert.ToString(value, 2).PadLeft(8, '0');

            Console.WriteLine("{0}   {1}" ,binary, bit);            
        }

        public static bool GetLastBit(int val, int bitNumber)
        {
            return ((val >> bitNumber) & 1) != 0;
        }

        public static void Print()
        {
            for (int i = 8; i > 0; i--)
            {
                if (array[-(i - 8)] == "1")
                {
                    Console.Write("x^{0} + ", i);
                }                
            }
            Console.Write("1");
        }

        static void Main(string[] args)
        {
            LfsrFibonacci();
            Console.ReadKey();
        }
    }
}
