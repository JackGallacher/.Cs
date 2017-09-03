using System;
namespace XmlAddition
{
    class Program
    {
        static void Main(string[] args)
        {
            //no point in running if there is no arguments.
            if (args.Length > 0)
            {
                try
                {
                    XmlLogic logic = new XmlLogic();

                    //looping through args means we can input multiple filepaths.
                    foreach (String xmlPath in args)
                    {
                        int total = logic.sumXML(xmlPath);
                        Console.WriteLine("\n" + total);
                    }
                }

                /*I have a question here - Is it best to generically catch execeptions, log them and rethrow them or catch specific exeptions? 
                    * Like here as I am importing a file, and want to check it it exists. That means this code could throw a FileNotFoundException but here I am just catching any Exception and reporting its message*/
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
            }
            else
            {
                Console.WriteLine("No xml path added as an argument. Exiting.");
            }

            //If you are running this is Visual Studio and not command line, uncommenting this stops the window from opening and closing right away if no argumants are passed.
            //Console.ReadLine();
        }
    }
}
