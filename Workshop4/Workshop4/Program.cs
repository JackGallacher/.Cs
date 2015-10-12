using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop4
{
    public static class Tasks
    {
        public static void number_checker()
        {
            int input = 0;
            Console.WriteLine("Enter a number: ");
            input = Convert.ToInt32(Console.ReadLine());

            if(input % 2 == 0)
            {
                Console.WriteLine("Your number is even.");
            }
            else
            {
                Console.WriteLine("Your number is odd.");
            }
            Console.ReadLine();
        }
        public static void temperature_switch()
        {
            int input = 0;
            double my_temp = 0;

            Console.WriteLine("1 - Convert F to C\n2 - Convert C to F");
            input = Convert.ToInt32(Console.ReadLine());

            switch(input)
            {
                case 1:
                    Console.WriteLine("\nInput temperature to convert: ");
                    my_temp = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("{0}F to C is: {1}", my_temp, 5 * (my_temp - 32) / 9);
                    break;
                case 2:
                    Console.WriteLine("\nInput temperature to convert: ");
                    my_temp = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("{0}C to F is: {1}", my_temp, (1.8 * my_temp) + 32);
                    break;
                default:
                    Console.WriteLine("Wrong input, try again");
                    temperature_switch();
                    break;
            }
            Console.ReadLine();
        }
        public static void logic()
        {
            int grade = 0;
            Console.WriteLine("Enter Grade: ");
            grade = Convert.ToInt32(Console.ReadLine());

            if(grade == 0 || grade <= 39)
            {
                Console.WriteLine("Fail");
            }
            if (grade == 40 || grade <= 49)
            {
                Console.WriteLine("Third");
            }
            if (grade == 50 || grade <= 59)
            {
                Console.WriteLine("Lower Second");
            }
            if (grade == 60 || grade <= 69)
            {
                Console.WriteLine("Upper Second");
            }
            if (grade == 70 || grade <= 100)
            {
                Console.WriteLine("First");
            }
            Console.ReadLine();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //Tasks.number_checker();
            //Tasks.temperature_switch();
            Tasks.logic();
        }
    }
}
