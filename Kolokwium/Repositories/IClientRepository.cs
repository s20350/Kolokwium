using Kolokwium.Models;

namespace Kolokwium.Repositories;

public interface IClientRepository
{
    Task<Client> GetClientWithSubscriptionsAsync(int idClient);
    Task AddPaymentAsync(Payment payment);
    Task SaveChangesAsync();
}
