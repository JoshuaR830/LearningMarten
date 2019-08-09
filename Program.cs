using System;
using Marten;

namespace PracticingMarten
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = DocumentStore.For("host=localhost;database=postgres;password=password;username=username");


            using (var session = store.LightweightSession())
            {
                var user = new UserResource
                {
                    Name = "Name" 
                };

                session.Store(user);

                session.SaveChanges();
            }

            
            


            
            Console.WriteLine("Hello World!");
        }
    }
}
