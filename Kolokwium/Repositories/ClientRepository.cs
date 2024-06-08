using Kolokwium.Data;
using Kolokwium.Models;
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly ApplicationDbContext _context;

    public ClientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Client> GetClientWithSubscriptionsAsync(int idClient)
    {
        return await _context.Clients
            .Include(c => c.Sales)
            .ThenInclude(s => s.Subscription)
            .Include(c => c.Payments)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
