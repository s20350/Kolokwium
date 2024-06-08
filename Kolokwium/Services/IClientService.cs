using Kolokwium.Models.DTOs;

namespace Kolokwium.Services;

public interface IClientService
{
    Task<ClientDto> GetClientWithSubscriptionsAsync(int idClient);
    Task AddPaymentAsync(int idClient, int idSubscription, DateTime date);
}
