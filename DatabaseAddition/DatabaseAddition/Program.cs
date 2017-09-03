using System;
using System.Linq;
namespace DatabaseAddition
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DataContext())
            {
                int total = 0;

                try
                {
                    //Clears the database so we only deal with values inputted as arguments.
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE Data");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }

                try
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        var entry = new Data { Value = int.Parse(args[i]) };

                        //Adds each element of the array to the LocalDB
                        db.DataSet.Add(entry);
                    }
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }

                //LINQ query to get each element in Data, was specific here but could have used var.
                IQueryable<DatabaseAddition.Data> query = from element in db.DataSet select element;

                try
                {
                    foreach (DatabaseAddition.Data element in query)
                    {
                        Console.WriteLine("id = " + element.ValueID + ", value = " + element.Value);
                        total += element.Value;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    throw;
                }
                Console.WriteLine("total = " + total);

                //Uncomment this if running in visual studio and don't want the command window to close immediatly.
                //Console.ReadLine();
            }
        }
    }
}