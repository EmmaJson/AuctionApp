namespace AuctionApp.Core;
public class Bid : IComparable<Bid>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }
    
    private DateTime _bidDate;
    public DateTime BidDate { get => _bidDate; }
    
    public Bid(string userName, double amount)
    {
        UserName = userName;
        Amount = amount;
        _bidDate = DateTime.Now;
    }

    public Bid(int id, string userName, double amount)
    {
        Id = id;
        UserName = userName;
        Amount = amount;
        _bidDate = DateTime.Now;
    }
    
    public int CompareTo(Bid other)
    {
        return -this.Amount.CompareTo(other.Amount);
    }
    
    public override string ToString()
    {
        return $"Id: {Id}: UserName: {UserName}, Amount: {Amount}, BidDate: {BidDate}";
    }
}