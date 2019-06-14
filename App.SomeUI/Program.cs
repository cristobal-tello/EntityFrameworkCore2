using App.Data;
using App.Domain;
using System;

namespace App.SomeUI
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertSamurai();

            Console.WriteLine("Finsihed!!!!!");
            Console.ReadKey();
        }

        private static void InsertSamurai()
        {
            var samurai = new Samurai() { Name = "John" };

            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }
    }
}
