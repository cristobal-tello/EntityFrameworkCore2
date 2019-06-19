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
            //MoreQueries();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            InsertBattle();
            QueryAndUpdateBattle_Disconnected();
            Console.WriteLine("Finsihed!!!!!");
            Console.ReadKey();
        }

        private static void QueryAndUpdateBattle_Disconnected()
        {
            // Simulates a disconnected context
            var battle = _context.Battles.FirstOrDefault();
            battle.EndDate = new DateTime(1888, 11, 28);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);  // There is also updateRange as we did using AddRange
                newContextInstance.SaveChanges();
            }
        }

        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle()
            {
                Name = "Battle of Ozuma",
                StartDate = new DateTime(1560, 12, 31),
                EndDate = new DateTime(1561, 08, 24)
            });
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }

        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
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
