using Kolokwium.Models.DTOs;
using Kolokwium.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetClient(int id)
    {
        try
        {
            var client = await _clientService.GetClientWithSubscriptionsAsync(id);
            return Ok(client);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost("payment")]
    public async Task<IActionResult> AddPayment(int idClient, int idSubscription, DateTime date)
    {
        try
        {
            await _clientService.AddPaymentAsync(idClient, idSubscription, date);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
