namespace Kolokwium.Models.DTOs;

public class PaymentDto
{
    public DateTime Date { get; set; }
    public int IdClient { get; set; }
    public Client Client { get; set; }
    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }
}