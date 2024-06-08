namespace Kolokwium.Models;

public class Sale
{
    public int IdSale { get; set; }  //do usuniecia
    public int IdClient { get; set; }
    public Client Client { get; set; }
    public int IdSubscription { get; set; }
    public Subscription Subscription { get; set; }
    public DateTime CreatedAt { get; set; }
}
