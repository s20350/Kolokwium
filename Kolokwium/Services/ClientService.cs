using Kolokwium.Models;
using Kolokwium.Models.DTOs;
using Kolokwium.Repositories;
using Kolokwium.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ClientDto> GetClientWithSubscriptionsAsync(int idClient)
    {
        var client = await _clientRepository.GetClientWithSubscriptionsAsync(idClient);
        if (client == null)
        {
            throw new Exception("Client not found!!!");
        }

        var clientDto = new ClientDto
        {
            FirstName = client.FirstName,
            LastName = client.LastName,
            Email = client.Email,
            Phone = client.Phone,
            Subscriptions = client.Sales.Select(s => 
            {
                var payments = client.Payments.Where(p => p.IdSubscription == s.IdSubscription).ToList();
                decimal totalPaidAmount = 0.0m;

                foreach (var payment in payments)
                {
                    var discountValue = s.Subscription.Discounts
                        .Where(d => d.DateFrom <= payment.Date && d.DateTo >= payment.Date)
                        .OrderByDescending(d => d.Value)
                        .FirstOrDefault()?.Value ?? 0;

                    var discountedPrice = s.Subscription.Price * (1 - discountValue / 100.0m);
                    totalPaidAmount += discountedPrice;
                }

                return new SubscriptionDto
                {
                    IdSubscription = s.IdSubscription,
                    Name = s.Subscription.Name,
                    TotalPaidAmount = totalPaidAmount
                };
            }).ToList()
        };

        return clientDto;
    }

    public async Task AddPaymentAsync(int idClient, int idSubscription, DateTime date)
    {
        var client = await _clientRepository.GetClientWithSubscriptionsAsync(idClient);
        if (client == null)
        {
            throw new Exception("Client not found!!!");
        }

        var subscription = client.Sales.FirstOrDefault(s => s.IdSubscription == idSubscription)?.Subscription;
        if (subscription == null)
        {
            throw new Exception("Subscription not found!!!!");
        }

        var lastPayment = client.Payments.Where(p => p.IdSubscription == idSubscription).OrderByDescending(p => p.Date).FirstOrDefault();
        var nextPaymentDate = lastPayment?.Date.AddMonths(subscription.RenewalPeriod) ?? client.Sales.First(s => s.IdSubscription == idSubscription).CreatedAt.AddMonths(subscription.RenewalPeriod);

        if (date < nextPaymentDate)
        {
            throw new Exception("Payment already made for this period!!!!");
        }

        var discount = subscription.Discounts.OrderByDescending(d => d.Value).FirstOrDefault(d => d.DateFrom <= date && d.DateTo >= date)?.Value ?? 0;
        var paymentAmount = subscription.Price * (1 - discount / 100.0m);

        var payment = new Payment
        {
            IdClient = idClient,
            IdSubscription = idSubscription,
            Date = date
        };

        await _clientRepository.AddPaymentAsync(payment);
        await _clientRepository.SaveChangesAsync();
    }
}
