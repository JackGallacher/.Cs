using System.ComponentModel.DataAnnotations;
namespace DatabaseAddition
{
    //Defines the colums within our database.
    public class Data
    {       
        [Key]//Our 'Primary Key'.
        public int ValueID { get; set; }

        //Column that we will save our arguments into.
        public int Value { get; set; }

    }
}
