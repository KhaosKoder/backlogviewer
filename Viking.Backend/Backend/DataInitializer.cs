
using System;
using System.Collections.Generic;
using System.Linq;

namespace TheVikingsApp.Backend
{
    public static class DataInitializer
    {
        public static void InitializeDatabase(AppDbContext context)
        {
            // Check if running in Debug mode; if not, return
            #if !DEBUG
                return;
            #endif

            // Check if data already exists
            if (context.Transactions.Any() || context.Authentications.Any())
            {
                return;   // Data already seeded
            }

            // Generate random test data for Transactions
            var rand = new Random();
            var transactions = Enumerable.Range(1, 50).Select(index => new Transaction
            {
                Name = "Name" + index,
                Surname = "Surname" + rand.Next(1, 4),
                EventTime = DateTime.Now.AddSeconds(rand.Next(-10000, 10000)),
            }).ToList();

            context.Transactions.AddRange(transactions);

            // Generate random test data for Authentications
            var authentications = Enumerable.Range(1, 50).Select(index => new Authentication
            {
                Name = "Name" + index,
                Surname = "Surname" + rand.Next(1, 4),
                EventTime = DateTime.Now.AddSeconds(rand.Next(-10000, 10000)),
            }).ToList();

            context.Authentications.AddRange(authentications);

            context.SaveChanges();
        }
    }
}
