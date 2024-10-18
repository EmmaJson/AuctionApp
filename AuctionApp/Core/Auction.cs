namespace AuctionApp.Core;

public class Auction
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public string UserName { get; set; }
    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids { get; set; }
    
    public Auction(string title, string userName)
    {
        Title = title;
        EndDate = DateTime.Now.AddDays(7);
        UserName = userName;
    }

    public Auction(int id, string title, string description, DateTime endDate, string userName)
    {
        Id = id;
        Title = title;
        Description = description;
        EndDate = endDate;
        UserName = userName;
    }

    public Auction()
    {
    }

    public void AddBid(Bid newBid)
    {
        _bids.Add(newBid);
    }

    public bool IsActive()
    {
        if (_bids.Count() == 0) return true;
        return _bids.All(b => EndDate > DateTime.Now);
    }

    public override string ToString()
    {
        return $"Id: {Id}: Title: {Title}, Description: {Description}, EndDate: {EndDate}";
    }
    
    
}