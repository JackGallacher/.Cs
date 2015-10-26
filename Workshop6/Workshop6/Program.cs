using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop6
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
            for(int i = 0; i <= 5; i++)
            {
                Console.Write("{0}", i);
            }
        }
        public static void Task_2()
        {
            double price = 0;
            int quantity = 0;

            arithmetic x = new arithmetic();

            for (quantity = 0; quantity <= 100; quantity++)
            {
                if (quantity % 10 == 0)
                {
                    price = x.calculate_cost(quantity);
                    Console.WriteLine("{0} = {1:c}", quantity, price);
                }
            }
        }
        public static void Task_3()
        {
            string result = " ";
            string input;

            Console.WriteLine("Input a string: ");
            input = Console.ReadLine();

            foreach(char x in input)
            {
                result = x + result;
            }
            for(int y = 0; y < result.Length; y++)
            {
                Console.Write(result[y]);
                if (result[y] != ' ')
                {
                    Console.Write(" ");
                }
                else
                {
                    continue;
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Tasks.Task_1();
            //Tasks.Task_2();
            Tasks.Task_3();
            Console.ReadLine();
        }
    }
}
