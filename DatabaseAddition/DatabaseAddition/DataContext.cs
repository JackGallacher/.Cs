using System.Data.Entity;
namespace DatabaseAddition
{
    //Creates our database context.
    public class DataContext : DbContext
    {
        public DbSet<Data> DataSet { get; set; }
    }
}
