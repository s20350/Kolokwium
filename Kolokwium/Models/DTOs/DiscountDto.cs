namespace Kolokwium.Models.DTOs;

public class DiscountDto
{
    public int Value { get; set; }
    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
}