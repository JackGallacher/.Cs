using System;

namespace Workshop12
{
    public class MyQueue
    {
        //the queue is a first in first out data structure. e.g. the element in the position [0] will always be the first to be removed, as it was the first element entered.
        private int back = 0;
        private int[] my_queue;

        public MyQueue(int x)//override construcor requiring size specification to init queue.
        {
            Console.WriteLine("Creating a Queue with the size of {0}.\n", x);
            my_queue = new int[x];
        }

        public void add(int x)//add an item onto the back of the queue.
        {
            try
            {
                Console.WriteLine("Adding {0} to the back of the queue.", x);
                my_queue[back] = x;
                back++;//increments the queue count when an element is added.
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine("The array is full, please remove the first element to add somthing else to the back of the queue");
                Console.Read();//waits for any key to be pressed.
                Environment.Exit(0);//then closes the application.
            }
        }
        public void remove()//remove an item from the front of the queue.
        {
            Console.WriteLine("Removing the element from the front of the queue, that number is {0}.\n", my_queue[0]);
            my_queue[0] = 0;
            shuffle(my_queue);
            back--;
        }
        public void shuffle(int[] array)//shuffle items in the queue (to move them down one place).
        {
            Console.WriteLine("Shuffling the queue, moving each item down one place in the queue\n");
            for (int i = 0; i < back; i++)//as the count is now 1 less due to it being "--" in the remove function, when myqueue[i] = my_queue[i + 1] hapenns it will be like myqueue[0] = my_queue[1], shfting the values up one space.
            {
                my_queue[i] = my_queue[i + 1];
            }
        }
        public void display()
        {
            Console.WriteLine("Displaying Queue\n");
            for(int i = 0; i < my_queue.Length; i++)
            {
                Console.WriteLine("Queue position [{0}] = {1}\n", i, my_queue[i]);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MyQueue queue = new MyQueue(5);

            queue.add(5);
            queue.add(6);
            queue.add(7);
            queue.display();

            queue.add(8);
            queue.display();

            queue.remove();
            queue.display();

            queue.remove();
            queue.display();

            Console.Read();
        }
    }
}