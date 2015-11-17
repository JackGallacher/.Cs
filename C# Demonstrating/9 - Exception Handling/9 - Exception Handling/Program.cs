using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop9
{
    class Program
    {
        static void Main(string[] args)
        {
            tasks x = new tasks();
            //x.try_catch();
            x.random_array();
            Console.ReadLine();
        }
    }
    class tasks
    {
        public void try_catch()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Enter first number: ");
                    int number1 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("Enter second number: ");
                    int number2 = Convert.ToInt32(Console.ReadLine());

                    Console.WriteLine("First number: {0}, Second number: {1}", number1, number2);
                    break;
                }
                catch (FormatException error)//executes is input is incorrect.
                {
                    Console.WriteLine(error);
                    continue;
                }
            }
        }
        public void random_array()
        {
            int average = 0;
            int[] students = new int[30];
            Random rand = new Random();//random seed.

            for (int i = 0; i < students.Length; i++)
            {
                int score = rand.Next(0, 100);//random number between 1 and 100.
                average = average + score;
                Console.WriteLine("Student {0} = {1}", i, score);
            }
            Console.WriteLine("Average is: {0}", get_average(average, students.Length));
        }
        private static double get_average(int total, int size)//resturns average based on total and array size.
        {
            return total / size;
        }
    }
}
