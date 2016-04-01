using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messing
{
    class Animal
    {
        public virtual void Sound()//The method is virtual, meaning it can be overriden in the child classes to alter its implementation.
        {
            Console.WriteLine("This is the generic Animal sound.");
        }
    }
    class Lion : Animal
    {
        public override void Sound()//This overrides the virtual void in the Animal class.
        {
            Console.WriteLine("This is the Lions Sound");
        }
    }
   
    class Node
    {
        public int NodeData;
        public Node NodeNext;
    }
    class LinkedList
    {
        private Node Head = null;//The top of our linked list.

        public void Add(int Data)
        {
            Node newNode = new Node();//Creates a new node that we will add to thise list.
            newNode.NodeData = Data;//Sets the data of this new node.
            newNode.NodeNext = Head;//Sets the next node of this node to the head of the list "linking the list" newNode.NodeNext ---> OriginalHead.
            Head = newNode;//Sets this node to the head of the list. So when we add ANOTHER item, it would look like this. secondnewNode.NodeNext --->newNode.NodeNext ---> OriginalHead.
        }

        public void PrintNodes()
        {
            Node CurrentNode = Head;
            while(CurrentNode != null)
            {
                Console.WriteLine(CurrentNode.NodeData);
                CurrentNode = CurrentNode.NodeNext;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Animal myAnimal = new Lion();
            //Animal myAnimal2 = new Animal();

            //myAnimal.Sound();
            //myAnimal2.Sound();

            //Program myProgram = new Program();
            //myProgram.ReverseString("Hello, my name is Jack!");

            LinkedList myList = new LinkedList();

            myList.Add(5);
            myList.Add(4);
            myList.Add(3);
            myList.Add(2);
            myList.Add(1);

            myList.PrintNodes();

            Console.ReadLine();
        }

        void ReverseString(string myString)
        {
            string ReversedString = "";
            foreach(char myChar in myString)
            {
                ReversedString = myChar + ReversedString;
            }
            Console.WriteLine(ReversedString);
        }

        void FindMatching(int[] ArrayOne, int[] ArrayTwo)
        {
            for(int i = 0; i < ArrayOne.Length; i++)
            {
                for(int j = 0; j < ArrayTwo.Length; i++)
                {

                }
            }
        }




    }
}
