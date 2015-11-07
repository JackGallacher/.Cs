using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop2
{
    public class Function
    {
        public void swap(int first, int second)
        {
            int temp = 0;

            temp = first;
            first = second;
            second = temp;

            Console.WriteLine("\nvalues swapped, first value: " + first + " second value: " + second);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int myInt = 10;
            Console.WriteLine("myInt = {0}", myInt);

            myInt = 10 + 45;
            Console.WriteLine("myInt = {0}", myInt);

            float myFloat = 1.234f;
            Console.WriteLine("myFloat = {0}", myFloat);

            decimal myDecimal = 28.24m;
            Console.WriteLine("myDecimal = {0}", myDecimal);

            decimal cashValue = 0;
            Console.Write("Input the decimal value of variable cashValue: ");
            cashValue = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("The value you entered is: " + "{0:c}", cashValue);

            string my_string;
            int my_int = 0;
            double my_double = 0;

            Console.Write("Input a string value: ");
            my_string = Console.ReadLine();

            Console.Write("Input an int value: ");
            my_int = Convert.ToInt32(Console.ReadLine());

            Console.Write("enter a double value");
            my_double = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("\nvariable 1: name: my_string value: " + my_string + "\nvariable 2: name: my_int value: " + my_int + "\nvariable 3: name: my_double value: " + my_double);

            double celsius = 0;
            Console.Write("Enter temperature in celsius");
            celsius = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine(celsius + "celsius in fahrenheight is:" + ((celsius * 1.8) + 32));

            int first_value = 0;
            int second_value = 0;

            Console.Write("Enter first value: ");
            first_value = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second value: ");
            second_value = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\nfirst value: " + first_value + "\nsecond value: " + second_value);

            Function x = new Function();
            x.swap(first_value, second_value);

            Console.ReadLine();
        }

    }
}
