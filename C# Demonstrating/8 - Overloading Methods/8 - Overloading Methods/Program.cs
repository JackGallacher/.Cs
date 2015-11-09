using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop8
{
    class Calculator
    {
        public double add(double x, double y)//add is overloaded. One for double and one for integer, the correct function is chosen at runtime.
        {
            return x + y;
        }
        public double add(int x, int y)
        {
            return x + y;
        }
        public double Args(string[] args, int args_amount)
        {
            double total = 0;
            for (int i = 0; i < args_amount; i++)
            {
                total += Convert.ToInt32(args[i]);
            }
            return total / args_amount;
        }
    }
    class Ring
    {
        public double circleArea(int radius)
        {
            return (Math.PI * radius) * radius;
        }
        public double ringArea(int first_area, int second_area)
        {
            return circleArea(first_area) - circleArea(second_area);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            double first_number = 0;
            double second_number = 0;
            int args_amount = args.Length;

            int first_area = 0;
            int second_area = 0;

            Calculator calculator = new Calculator();
            Ring ring = new Ring();

            //first_number = Convert.ToDouble(args[0]);//cmd values.
            //second_number = Convert.ToDouble(args[1]);//cmd values.

            Console.Write("Enter first number: ");
            first_number = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter second number: ");
            second_number = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Sum is: {0}", calculator.add(first_number, second_number));

            //Console.WriteLine("Average of args is: {0}", calculator.Args(args, args_amount));

            Console.Write("Enter area for the first circle: ");
            first_area = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter area for the second circle: ");
            second_area = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Area in-between is: {0:f3}", ring.ringArea(first_area, second_area));
            Console.ReadLine();
        }
    }
}
