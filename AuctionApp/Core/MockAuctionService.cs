using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctions;
        ///TODO: Probably not relevant?
    }

    public List<Auction> GetAllActive()
    {
        List<Auction> activeAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (auction.IsActive())
            {
                activeAuctions.Add(auction);
            }
        }
        return activeAuctions;
    }

    public Auction GetById(int id)
    {
        throw new NotImplementedException();
        //return _auctions.Find(a => a.Id == id && a.IsActive() == true);      //Lambdauttryck i parantes
    }
    
    public void Add(string userName, string title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }
    
    private static readonly List<Auction> _auctions = new();
    // C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction(1, "Auction 1","Learn ASP: NET with MVC",DateTime.Today.AddDays(3),"emmajoh2@kth.se");
        Auction a2 = new Auction(2, "Auction 2","Sleeping..",DateTime.Today.AddDays(-2), "emmajoh2@kth.se"); //Wont show since it is not active;
        a2.AddBid(new Bid(1, "emmajoh2@kth.se", 100));
        a2.AddBid(new Bid(1, "emmajoh2@kth.se", 150));
        _auctions.Add(a1);
        _auctions.Add(a2);
    }
}