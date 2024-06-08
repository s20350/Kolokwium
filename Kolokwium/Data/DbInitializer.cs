using Kolokwium.Models;

namespace Kolokwium.Data;

public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Clients.Any())
        {
            return;
        }

        var clients = new Client[]
        {
            new Client { FirstName = "test1", LastName = "test1", Email = "test1@gmail.com", Phone = "111111111" },
            new Client { FirstName = "test2", LastName = "test2", Email = "test2@gmail.com", Phone = "222222222" }
        };

        foreach (Client c in clients)
        {
            context.Clients.Add(c);
        }

        var subscriptions = new Subscription[]
        {
            new Subscription { Name = "AAA", RenewalPeriod = 1, EndTime = new DateTime(2024, 12, 31), Price = 100.0m },
            new Subscription { Name = "BBB", RenewalPeriod = 3, EndTime = new DateTime(2024, 12, 31), Price = 250.0m }
        };

        foreach (Subscription s in subscriptions)
        {
            context.Subscriptions.Add(s);
        }

        var sales = new Sale[]
        {
            new Sale { Client = clients[0], Subscription = subscriptions[0], CreatedAt = new DateTime(2023, 1, 1) },
            new Sale { Client = clients[1], Subscription = subscriptions[1], CreatedAt = new DateTime(2023, 2, 1) }
        };

        foreach (Sale s in sales)
        {
            context.Sales.Add(s);
        }

        var discounts = new Discount[]
        {
            new Discount { Value = 10, Subscription = subscriptions[0], DateFrom = new DateTime(2023, 1, 1), DateTo = new DateTime(2023, 6, 1) }
        };

        foreach (Discount d in discounts)
        {
            context.Discounts.Add(d);
        }

        var payments = new Payment[]
        {
            new Payment { Client = clients[0], Subscription = subscriptions[0], Date = new DateTime(2023, 1, 1) },
            new Payment { Client = clients[1], Subscription = subscriptions[1], Date = new DateTime(2023, 2, 1) }
        };

        foreach (Payment p in payments)
        {
            context.Payments.Add(p);
        }

        context.SaveChanges();
    }
}
