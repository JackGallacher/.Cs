using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop3
{
    public static class arithmetic
    {
        public static void arithmatic_program()
        {
            string firstNumber, secondNumber;
            int number1, number2;

            Console.Write("Please enter your first integer: ");
            firstNumber = Console.ReadLine();

            Console.Write("\nPlease enter your second integer: ");
            secondNumber = Console.ReadLine();

            number1 = Convert.ToInt32(firstNumber);
            number2 = Convert.ToInt32(secondNumber);

            int sum = number1 + number2;
            int difference = number1 - number2;
            int multiply = number1 * number2;
            int division = number1 / number2;
            int mod = number1 % number2;
            int average = (number1 + number2) / 2;

            Console.WriteLine("\n{0} + {1} = {2}.", number1, number2, sum);
            Console.WriteLine("\n{0} - {1} = {2}.", number1, number2, difference);
            Console.WriteLine("\n{0} * {1} = {2}.", number1, number2, multiply);
            Console.WriteLine("\n{0} / {1} = {2}.", number1, number2, division);
            Console.WriteLine("\n{0} % {1} = {2}.", number1, number2, mod);
            Console.WriteLine("\nAverage of {0} and {1} = {2}.", number1, number2, average);

            if (number1 > number2)
            {
                Console.WriteLine("\nnumber1 is greater than number2");
            }
            if (number2 > number1)
            {
                Console.WriteLine("\nnumber2 is greater than number1");
            }
            if (number1 == number2)
            {
                Console.WriteLine("\nnumber1 and number2 are the same");
            }

            Console.ReadLine();
        }
    }
    public static class workshop3
    {
        public static void workshop3_program()
        {
            double property_value = 0;
            int stamp_duty_percentage = 0;
            double stamp_duty_payment = 0;

            Console.Write("Enter property value: ");
            property_value = Convert.ToDouble(Console.ReadLine());

            if(property_value <= 150000)
            {
                stamp_duty_percentage = 0;
            }
            if (property_value > 150000 && property_value <= 250000)
            {
                stamp_duty_percentage = 1;
                stamp_duty_payment = (property_value / 100) * 1;
            }
            if (property_value > 250000 && property_value <= 500000)
            {
                stamp_duty_percentage = 3;
                stamp_duty_payment = (property_value / 100) * 3;
            }
            if (property_value > 500000)
            {
                stamp_duty_percentage = 4;
                stamp_duty_payment = (property_value / 100) * 4;
            }
            Console.WriteLine("\nProperty Value: {0:c}\nStamp Duty Rate: {1}%\n\nThe amount of duty to pay on this property is: {2:c}", property_value, stamp_duty_percentage, stamp_duty_payment);
            Console.ReadLine();
        }
    }
    public static class VAT
    {
        public static void VAT_program()
        {
            double item_cost = 0;
            double vat_rate = 0;
            double vat_tax = 0;
            double final_cost = 0;

            Console.Write("Enter item cost: ");
            item_cost = Convert.ToDouble(Console.ReadLine());

            Console.Write("\nEnter vat rate: ");
            vat_rate = Convert.ToDouble(Console.ReadLine());

            vat_tax = item_cost * vat_rate;
            final_cost = item_cost + vat_tax;

            Console.WriteLine("Final price plus vat is: {0:c}", final_cost);
            Console.ReadLine();
        }
    }
    public static class final_marks
    {
        public static void final_marks_calculator()
        {
            int assignment1_marks = 0;
            int assignment2_marks = 0;
            double weighted_mark1 = 0;
            double weighted_mark2 = 0;
            string input;

            Console.WriteLine("Enter marks for Assignment 1: ");
            assignment1_marks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Is a penalty applied to Assignment 1?");
            input = Console.ReadLine();
            if(input == "yes")
            {
                Console.WriteLine("10% Penalty Applied");
                assignment1_marks = assignment1_marks - (assignment1_marks / 100) * 10;
            }
          
            Console.WriteLine("Enter marks for Assignment 2: ");
            assignment2_marks = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Is a penalty applied to Assignment 2?");
            input = Console.ReadLine();
            if (input == "yes")
            {
                Console.WriteLine("10% Penalty Applied");
                assignment2_marks = assignment2_marks - (assignment2_marks / 100) * 10;
            }

            weighted_mark1 = 0.3 * assignment1_marks;
            weighted_mark2 = 0.7 * assignment2_marks;

            Console.WriteLine("The final mark is: {0}", weighted_mark1 + weighted_mark2);
            Console.ReadLine();
        }
    }
    
    public class Program
    {
        static void Main(string[] args)
        {
            //arithmetic.arithmatic_program();  
            //workshop3.workshop3_program();   
            //VAT.VAT_program();     
            //final_marks.final_marks_calculator();   
        }
    }
}
