using App.Data;
using App.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
            //InsertBattle();
            //QueryAndUpdateBattle_Disconnected();
            //AddSomeMoreSamurais();
            //DeleteWhileTracked();
            //DeleteWhileNotTracked();
            //DeleteMany();
            //DeleteUsingId(3);   // Make sure there is a valid id on database

            //InsertNewPKFkGraph();
            //InsertNewPkFkGraphMultipleChildren();
            //AddChildToExistingObjectWhileTracked();
            // AddChildToExistingObjectWhileNotTracked();  // This method will not work.
            //AddChildToExistingObjectWhileNotTracked(5); // Id of samurai. So, make sure id already exists in db
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            FilteringWithRelatedData();
            Console.WriteLine("Finsihed!!!!!");
            Console.ReadKey();
        }

        private static void FilteringWithRelatedData()
        {
            var happySamurais = _context.Samurais
                                    .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                                    .ToList();
        }

        // Define the shape of the query results (Query projections)
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();

            var somePropertiesWithQuotes = _context.Samurais.Select(s => new { s.Id, s.Name, s.Quotes }).ToList();

            var somePropertesWithHappyQuotes = _context.Samurais.Select(s => new { s.Id, s.Name, HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) }).ToList();

            var somePropertesWithHappySamuraiAndQuotes = _context.Samurais.Select(s => new { Samurai = s, Quotes = s.Quotes.Where(q => q.Text.Contains("happy")) }).ToList();   // Course said this line will not work, but it does.

            var samurais = _context.Samurais.ToList();          // Right way, do it in 2 parts, this is the first one
            var happyQuotes = _context.Quotes.Where(q => q.Text.Contains("happy")).ToList();    // This is the second one
        }

        
        // Include related objects in query (Eager Loading)
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();

            // Ways to use Include
            _context.Samurais.Include(s => s.Quotes);   // Include chid objects

            // _context.Samurais.Include(s => s.Quotes).ThenInclude(q => q.Translations);   // This is just an example (Include children and grand-children)

            // _context.Samurais.Include(s => s.Quotes.Translations);      // Include just grandchildren

            _context.Samurais.Include(s => s.Quotes).Include(s => s.SecretIdentity);    // Include different children
        }

        // This is the right method, use FK
        private static void AddChildToExistingObjectWhileNotTracked(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?",
                SamuraiId = samuraiId
            };

            using (var newContext = new SamuraiContext())
            {
                newContext.Quotes.Add(quote);
                newContext.SaveChanges();
            }
        }


        // This method will not work as we expect
        // New context doesn't know how to match Samurais and Quotes
        // Also the samurai it's already in database
        private static void AddChildToExistingObjectWhileNotTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });

            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Add(samurai);   // Nope.
                newContext.SaveChanges();
            }
        }

        private static void InsertNewPKFkGraph()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you" }
                }
            };

            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void InsertNewPkFkGraphMultipleChildren()
        {
            var samurai = new Samurai
                {
                    Name = "Kyuzo",
                    Quotes = new List<Quote>
                    {
                        new Quote {Text="Watch out for my sharp sword!"},
                        new Quote {Text="I told you watch out for the sharp sword! Oh well!"}
                    }
                };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }

        private static void AddChildToExistingObjectWhileTracked()
        {
            var samurai = _context.Samurais.First();
            samurai.Quotes.Add(new Quote
                {
                    Text = "I bet you're happy that I've saved you!"
                });
            _context.SaveChanges();
        }

        private static void DeleteUsingId(int samuraiId)
        {
            // Only we need the id if we need delete an object
            // But this is not efficient, 2 trips to database, but right now this is the only way
            // You can use an store procedure
            // _context.Database.ExecuteSqlCommand("exec DeleteById {0}", samuraiId)	
            var samurai = _context.Samurais.Find(samuraiId);
            _context.Remove(samurai);
            _context.SaveChanges();
        }

        private static void DeleteMany()
        {
            var samurais = _context.Samurais.Where(s => s.Name.StartsWith("K"));
            _context.Samurais.RemoveRange(samurais);
            _context.SaveChanges();
}

        private static void DeleteWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Gorobei");
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }

        private static void DeleteWhileNotTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name == "Heihachi");
            using (var contextNewAppInstance = new SamuraiContext())
            {
                contextNewAppInstance.Samurais.Remove(samurai);
                contextNewAppInstance.SaveChanges();
            }
        }

        private static void AddSomeMoreSamurais()
        {
            _context.AddRange(
                new Samurai() { Name = "Kambei" },
                new Samurai() { Name = "Shichiroji" },
                new Samurai() { Name = "Katsushiro" },
                new Samurai() { Name = "Heihachi" },
                new Samurai() { Name = "Kyuzo" },
                new Samurai() { Name = "Gorobei" }
                );
            _context.SaveChanges();
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
