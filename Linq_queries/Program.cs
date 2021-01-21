using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;


namespace Linq_queries
{
    public class Book
    {
        public int id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int quantity { get; set; }

    }

    public class library: DbContext
    {
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=.;Database=firstDb;Trusted_Connection=True;");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            using(var context=new library())
            {
                var book = new Book()
                {
                    name = "Fault in our stars",
                    author="John Green",
                    quantity=5
                };
                var book2 = new Book()
                {
                    name="Fourty rules of love",
                    author="Elif",
                    quantity=3
                };

                var book3 = new Book()
                {
                    name="Al-chemist",
                    author= " Paulo Coelho",
                    quantity=2
                };
                /*context.Books.Add(book);
                context.Books.Add(book2);
                context.Books.Add(book3);*/

                context.SaveChanges();


                //=============find item in table======================

                using(var bk =new library())
                {
                    var s = bk.Books.Find(2);
                    Console.WriteLine("Book at second index=" + s.name);
                    Console.WriteLine("Author is=" + s.author);
                    Console.WriteLine("Available quantity is=" + s.quantity);

                    //================first name item===================
                    var name = (from n in bk.Books
                                where n.name == "Al-chemist"
                                select n).FirstOrDefault<Book>();
                    System.Console.WriteLine(name.author);

                    //================order by===========================
                    var item = from j in bk.Books
                               orderby j.name descending
                               select j;
                    foreach (var d in item)
                    {
                        System.Console.WriteLine(d.name+" "+d.author+" "+d.quantity);
                    }
                   
                              
                }
                System.Console.WriteLine("=================================");
                
                List<string> l=new List<string>()
                {
                    "naran","kaghan","murree","hunza","swat"
                };

                var city = from c in l
                           where c.Contains("a")
                           select c;
                foreach (var ci in city)
                {

                    Console.WriteLine("Cities are: " + ci);
                }
                //======================group by============================
                IDictionary<int, string> dic = new Dictionary<int, string>()
                {
                    { 32,"lahore" },{45,"karachi"},{55,"Islamabad"}
                };

                var res = from re in dic
                          group re by re.Key;
                System.Console.WriteLine("=================================");
                foreach (var r in res)
                {

                    Console.Write("Keys are:"+r.Key);
                    foreach(var val in r)
                        Console.WriteLine(" Cities are:" + val.Value);

                }

            }
        }
    }
}
