namespace AuctionApp.Core;
public class Bid
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }
    
    private DateTime _bidDate;
    public DateTime BidDate { get => _bidDate; }

    public Bid(int id, string userName, double amount)
    {
        Id = id;
        UserName = userName;
        _bidDate = DateTime.Now;
    }
}