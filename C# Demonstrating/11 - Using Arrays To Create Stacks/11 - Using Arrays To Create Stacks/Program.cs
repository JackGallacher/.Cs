using System;

namespace Workshop11
{
    public class Stack
    {
        private int top = -1;
        private int[] my_stack;//default size of 10;

        public Stack()
        {
            my_stack = new int[10];
            //default constructor.
        }
        public Stack(int size)//override constructor.
        {
            my_stack = new int[size];
        }
        public void push(int x)
        {
            my_stack[++top] = x;//adds data to the position + 1 of the current "top" value. e.g. top = -1 so the first array possition assigned is -1 + 1 = 0.
        }
        public int pop()
        {
            return my_stack[top--];//returns the position in the array below the "top" position, giving the impression of "popping" the stack. e.g. top  = stack[4], stack.pop = stack[3].
        }
        public int get_top()//returns the top object in the stack.
        {
            return my_stack[top];
        }
        public int get_size()//returns the stack size.
        {
            return my_stack.Length;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Stack my_stack = new Stack();
            Console.WriteLine("size of the stack is {0}", my_stack.get_size());

            my_stack.push(1);
            my_stack.push(2);
            my_stack.push(3);

            Console.WriteLine("top of stack is {0}", my_stack.get_top());
            Console.WriteLine("popping the stack, data popped is: {0}", my_stack.pop());
            Console.WriteLine("top of stack is now {0}", my_stack.get_top());

            Console.Read();
        }
    }
}
