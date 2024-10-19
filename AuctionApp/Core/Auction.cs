namespace AuctionApp.Core;

public class Auction : IComparable<Auction>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime EndDate { get; set; }
    public string UserName { get; set; }
    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids => _bids;
    
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
        if (_bids.Count != 0)
        {
            if (_bids.First().CompareTo(newBid) <= 0)
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
        return this.EndDate.CompareTo(other.EndDate);
    }
    
    public override string ToString()
    {
        return $"Id: {Id}: Title: {Title}, Description: {Description}, EndDate: {EndDate}";
    }
}