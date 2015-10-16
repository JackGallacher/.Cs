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
        public static void if_else()
        {
            int input = 0;
            Console.WriteLine("Input an Int: ");
            input = Convert.ToInt32(Console.ReadLine());

            if(input < 0)
            {
                input = input - 2;
                Console.WriteLine("Int is negative, -2 taken away, value is: {0}", input);
            }
            else
            {
                input++;
                Console.WriteLine("Int is positive, +1 added, value is: {0}", input);
            }
            Console.ReadLine();
        }
        public static void my_switch()
        {
            int input = 0;
            int season = 0;
            Console.WriteLine("Enter number of the month: ");
            input = Convert.ToInt32(Console.ReadLine());

            if(input > 0 && input <= 3)
            {
                season = 1;
            }
            if (input > 3 && input <= 6)
            {
                season = 2;
            }
            if (input > 6 && input <= 9)
            {
                season = 3;
            }
            if (input > 9 && input <= 12)
            {
                season = 4;
            }
            switch (season)
            {
                case 1:
                    Console.WriteLine("Season is Spring\n");
                    break;
                case 2:
                    Console.WriteLine("Season is Summer\n");
                    break;
                case 3:
                    Console.WriteLine("Season is Autumn\n");
                    break;
                case 4:
                    Console.WriteLine("Season is Winter\n");
                    break;      
                default:
                    Console.WriteLine("Error\n");
                    break;
            }
            Console.ReadLine();
        }           
    }


    class Program
    {
        static void Main(string[] args)
        {
            Tasks.number_checker();
            Tasks.temperature_switch();
            Tasks.logic();
            Tasks.if_else();
            Tasks.my_switch();
        }
    }
}
