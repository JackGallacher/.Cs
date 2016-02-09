using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop13
{
    class LinkedList
    {
        private int data;//what we will save to each node
        private LinkedList next;//the value of the "next" node within each node

        public LinkedList(int value)//override constructor
        {
            data = value;//sets the data for this node to the passed value.
            next = null;//when this constuctor is called, it will be the only node in the list so far, therefore we set the "next" node for this firstto null. If we add new nodes, this will change
        }

        public LinkedList InsertNext(int value)//this function is called by the node we went to add onto. this is called by node1 to add a node2 e.g. --> node1.Insertnext(2);
        {
            LinkedList node = new LinkedList(value);//creates the node that we will add onto the node that has called this function.

            //"this" refers to the node we have called the function on. so if we went node1.Insertnext(2) "this" would refer to the data values of node1.
            if (this.next == null)//checks if the current node is connected to another node.
            {
                node.next = null;//sets the "next" variable of the new node we are creating to null, as it is now the next item in the linked list and will not be pointing to anything until we call this function on it.
                this.next = node;//sets the next varaible of the node we called this function on to the node we have just created "linking" the two. e.g. node1.next == node (where node is equal to a new LinkedList object)
            }
            else//if the node we called to add a new node next alreadt has a node next to it, we add the new node in between those two nodes to make it the next node.
            {
                LinkedList temp = this.next;//createsa temp node to save the next varible of out current node.
                node.next = temp;//sets the node we are creating in this function to be the "next" value of the node we called this function on.
                this.next = node;//sets the next node of the node we called this function on to point toward the node we are creating in the function.
            }
            return node;//returns a newly created node.
        }

        public int DeleteNext()//this function takes the next value of the node we call this on and deletes it, then add the next node to the node. e.g. node1 ---> delete node2 ---> add node3 to be node1's next value.
        {
            if(this.next == null)//checks if the node actually has a next.
            {
                return 0;
            }

            LinkedList node = this.next;//creates a new node that equals the next value of the node we want to delete the node next to it. e.g. node1.DeleteNext ---> node = node2
            this.next = this.next.next;//sets the value of the node we called this function on the the node after the node we are removing. e.g. this = node 1, this.next = node2, this.next.next = node3
            node = null;//removes the node reference that we created ready for the next call.
            return 1;//if completed, return true.
        }

        public void Traverse(LinkedList node)//traverses the linked list starting from the node value we pass into this function.
        {
            if(node == null)
            {
                node = this;//if the "node" value we passed into this function is null, we set the node varailbe to the node we called this function on. e.g. node1.Traverse(null) ---> node = node1
            }
            Console.WriteLine("\n\nTraversing the list in a forward direction\n\n");

            while(node != null)//loops until the "next" value of the current node in the loop is equal to null. e.g. the last node in the linked list.
            {
                Console.WriteLine(node.data);//prints out the data variable being sored in the current node
                node = node.next;//sets the node variable to the next node in the list using the current nodes' "next" varaible value.
            }
        }
            
    }
    class Program
    {
        static void Main(string[] args)
        {
            LinkedList node1 = new LinkedList(1);
            LinkedList node2 = node1.InsertNext(2);//creates a new LinkedList object called node2 and adds it onto the "next" variable of node1 through calling "InsertNext" on node 1.
            LinkedList node3 = node2.InsertNext(3);
            LinkedList node4 = node3.InsertNext(4);
            LinkedList node5 = node4.InsertNext(5);

            node1.Traverse(null);
            Console.ReadLine();
        }
    }
}
