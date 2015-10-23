using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop5
{
    public class arithmetic
    {
        public double calculate_cost(int quantity)
        {
            double cost = 0;

            if (quantity <= 50)
            {
                cost = quantity * 5.0;
            }
            if (quantity >= 51 && quantity <= 80)
            {
                cost = quantity * 4.0;
            }
            if (quantity >= 81 && quantity <= 100 || quantity > 100)
            {
                cost = quantity * 2.5;
            }
            return cost;
        }
    }
    public static class Tasks
    {
        public static void Task_1()
        {
            int counter = 1;
            while(counter <= 5)
            {
                Console.Write("{0}", counter);
                counter++;
            }
        }
        public static void Task_2()
        {
            double price = 0;
            int quantity = 0;
            bool quit = false;

            arithmetic x = new arithmetic();

            for (quantity = 0; quantity <= 100; quantity++)
            {
                if(quantity % 10 == 0)
                {
                    price = x.calculate_cost(quantity);                   
                    Console.WriteLine("{0} = {1:c}", quantity, price);
                }
            }
            while(quit != true)
            {
                string input;

                Console.WriteLine("Input number of Widgets");
                input = Console.ReadLine();

                quantity = Convert.ToInt32(input);
                price = x.calculate_cost(quantity);
                Console.WriteLine("{0} = {1:c}", quantity, price);

                Console.WriteLine("Would you like to continue or quit? Qq/Yy");
                input = Console.ReadLine();

                if (input == "q" || input == "Q")
                {
                    quit = true;
                }
                if (input == "y" || input == "Y")
                {
                    continue;
                }
            }
        } 
        public static void Task_3()
        {
            int fibonacci = 1;
            int previous = 0;

            for(int i = 0; i <= 20; i++)
            {
                fibonacci = fibonacci + previous;
                Console.Write("{0}, ", fibonacci);
                previous = fibonacci - previous;
            }
            Console.ReadLine();
        } 
         
        public static void Task_4()
        {
            decimal total_ran = 10;//initial distance ran.
            int days_ran = 0;//total day ran
            int run_increase_percentage = 4;//percentage increase from initial run

            do
            {
                total_ran += (total_ran / 100) * run_increase_percentage;
                days_ran++;
                Console.WriteLine("Day {0} - {1}", days_ran, Math.Round(total_ran, 1));//outputs day and rounds distance to 1 decimal place.
            } while (total_ran <= 200);

            Console.WriteLine("\n200km reched in {0} days, with a percentage increase of {1}", days_ran, run_increase_percentage);
            Console.ReadLine();
        }   
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Tasks.Task_1();
            //Tasks.Task_2();
            //Tasks.Task_3();
            Tasks.Task_4();
        }
    }
}
