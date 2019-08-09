using System;
using System.Collections.Generic;
using System.Linq;
using Marten;

namespace PracticingMarten
{
    class Program
    {

        private static DocumentStore store = DocumentStore.For("host=localhost;database=postgres;password=password;username=username");

        static void Main(string[] args)
        {
             using (var session = store.LightweightSession())
            {
                AddToDatabase(session);
                SimpleReadFromDatabase(session);
                FilterReadFromDatabase(session);
                Updater(session);
            }

            System.Console.WriteLine("------------------------------------");

            Console.WriteLine("Hello World!");
        }

        // This writes to the database
        // A new object is created an populated
        // the new user is then stored in the database
        // This must be saved in the database
        static void AddToDatabase(IDocumentSession session)
        {
                var user = new UserResource
                {
                    FirstName = "Walter",
                    LastName = "WokStar",
                    Age = 18,
                    FavouriteWokCompany = "Wok On!" 
                };

                session.Store(user);
                session.SaveChanges();
            
        }

        // This queries the database that is currently being accessed
        // The Query looks for the objects that have been saved of that type - in this case it is looking for UserResource objects
        // Provides output so you can see that it works
        static void SimpleReadFromDatabase(IDocumentSession session)
        {
            System.Console.WriteLine("------------------------------------");
            var targets = session.Query<UserResource>().ToArray();

            // Loop through and print out the names
            foreach(var user in targets)
            {
                System.Console.WriteLine(user.FirstName);
            }
        }


        // This queries the database and filters by name
        // outputs the different globally unique identifiers assigned to the users
        static void FilterReadFromDatabase(IDocumentSession session)
        {
            System.Console.WriteLine("------------------------------------");

            var queriedOutput = session.Query<UserResource>()
            .Where(
                c => c.FirstName == "Walter"
            )
            .ToArray();

            foreach(var user in queriedOutput)
            {
                System.Console.WriteLine("Favourite wok company: " + user.FavouriteWokCompany);
            }
        }


        // Update an existing row by taking existing data and updating it to be consistent
        static void Updater(IDocumentSession session)
        {
            var defaultWokCompany = "Wok On!"; 

            var results = session.Query<UserResource>()
            .Where(
                c => c.FirstName == null
            )
            .ToArray();

            foreach(var result in results)
            {
                System.Console.WriteLine("Renamed from " + result.Name);
                var splitName = result.Name.Split(" ");
                
                result.FirstName = splitName[0];

                if(result.Name.Contains(" "))
                    result.LastName = splitName[1];

                if (result.FavouriteWokCompany == null)
                {
                    result.FavouriteWokCompany = defaultWokCompany;
                }

                session.Store(result);
            }

            session.SaveChanges();

        }
    }
}
