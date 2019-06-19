using App.Data;
using App.Domain;
using System;
using System.Linq;

namespace App.SomeUI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        static void Main(string[] args)
        {
            //InsertSamurai();
            //InsertMultipleSamurais();
            //InsertMultipleDifferentObjets();
            //SimpleSamuraiQuery();
            MoreQueries();
            Console.WriteLine("Finsihed!!!!!");
            Console.ReadKey();
        }

        private static void MoreQueries()
        {
            using (var context = new SamuraiContext())
            {
                var samurais1 = _context.Samurais.Where(s => s.Name == "John").ToList();  // Take a look sql on logger. 'John' is hardcode into the query


                var name = "John";
                var samurais2 = _context.Samurais.Where(s => s.Name == name).ToList();  // Take a look sql on logger. 'John' ISN'T hardcode into the query it is a parameter

                // ...OrDefault methods, it returns null in case not found. The counterpart it will throw an exception

                // Find an DBSet method. It is used to use a key to get a value using the key
                // EF.Functions.Like(s.Name, "J%")



            }
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

        private static void SimpleSamuraiQuery()
        {
            using (var context = new SamuraiContext())
            {
                var samurais = context.Samurais/*.ToList()*/;   // No different noted .ToList vs nothing

                // Don't use 'context.Samurais' in foreach. In some cases, potential performance probles.s Better get results First as we do here
                // Why? Because the connection stays open until last result if fetched
                foreach (var samurai in samurais)
                {
                    Console.WriteLine(samurai.Name);
                }
            }
        }
    }
}
