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
            InsertMultipleSamurais();
            InsertMultipleDifferentObjets();
            Console.WriteLine("Finsihed!!!!!");
            Console.ReadKey();
        }

        private static void InsertMultipleSamurais()
        {
            var samurai1 = new Samurai() { Name = "Charles" };
            var samurai2 = new Samurai() { Name = "Tom" };

            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai1, samurai2);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleDifferentObjets()
        {
            var samurai = new Samurai() { Name = "Robert" };
            var battle = new Battle()
            {
                Name = "Any battle",
                StartDate = new DateTime(2000, 11, 29),
                EndDate = new DateTime(2001, 12, 24)
            };

            using (var context = new SamuraiContext())
            {
                context.AddRange(samurai, battle);
                context.SaveChanges();
            }
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
