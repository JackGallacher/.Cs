using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop10
{
    class Person
    {
        public string firstname;
        public string lastname;
        public string dob;
        public string address;
        public int telno;

        public Person()//constructor.
        {
            Console.WriteLine("Person() default constuctor called.");
        }

        public void setfirst(string first)
        {
            firstname = first;
        }
        public void setlast(string last)
        {
            lastname = last;
        }
        public void setdob(string subdob)
        {
            dob = subdob;
        }
        public void setadd(string add)
        {
            address = add;
        }
        public void setno(int no)
        {
            telno = no;
        }
    }

    class Contacts
    {
        int count = 0;
        public Person[] mycontacts = new Person[5];//an array that can take version of the person class.

        public Contacts()
        {
            Console.WriteLine("Contacts() default contractor called.");
        }
        public void addcontact(Person myperson)//adds person to the array.
        {
            mycontacts[count] = myperson;
            count++;
        }       
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person Person1 = new Person();
            Contacts contact = new Contacts();

            Person1.setfirst("Jack");
            Person1.setlast("Gallacher");
            Person1.setdob("29/04/1993");
            Person1.setadd("Lincoln");
            Person1.setno(11266257);

            contact.addcontact(Person1);//adds person to the array.

            Console.WriteLine(contact.mycontacts[0].firstname.ToString() + "\n");//looks into the array position and pull out the specific peice of data stored within that class in that position.
            Console.WriteLine(contact.mycontacts[0].lastname.ToString() + "\n");
            Console.WriteLine(contact.mycontacts[0].dob.ToString() + "\n");
            Console.WriteLine(contact.mycontacts[0].address.ToString() + "\n");
            Console.WriteLine(contact.mycontacts[0].telno.ToString() + "\n");

            Console.Read();
        }
    }
}
