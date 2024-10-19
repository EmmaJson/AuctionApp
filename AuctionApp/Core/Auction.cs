namespace AuctionApp.Core;

public class Auction : IComparable<Auction>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public string AuctionOwnerName { get; set; }
    public double StartingPrice { get; set; }
    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids => _bids;
    
    public Auction(string title, string auctionOwnerName)
    {
        Title = title;
        EndDate = DateTime.Now.AddDays(7);
        AuctionOwnerName = auctionOwnerName;
    }

    public Auction(int id, string title, string description, DateTime endDate, string auctionOwnerName, double startingPrice)
    {
        Id = id;
        Title = title;
        Description = description;
        EndDate = endDate;
        AuctionOwnerName = auctionOwnerName;
        StartingPrice = startingPrice;
    }

    public Auction()
    {
    }

    public void AddBid(Bid newBid)
    {
        if (AuctionOwnerName == newBid.UserName)
        {
            throw new NotImplementedException();
        }
        if (EndDate.CompareTo(DateTime.Now) <= 0)
        {
            throw new NotImplementedException();
        }
        if (_bids.Count == 0 && newBid.Amount <= StartingPrice)
        {
            throw new NotImplementedException();
        }
        if (_bids.Count != 0)
        {
            if (newBid.CompareTo(_bids.First()) > 0)
            {
                throw new NotImplementedException();
            }
        }
        _bids.Add(newBid);
        _bids.Sort();
    }

    public bool IsActive()
    {
        return EndDate > DateTime.Now;
    }

    public int CompareTo(Auction other)
    {
        return EndDate.CompareTo(other.EndDate);
    }
    
    public override string ToString()
    {
        return $"Id: {Id}: Title: {Title}, Description: {Description}, EndDate: {EndDate}";
    }
}