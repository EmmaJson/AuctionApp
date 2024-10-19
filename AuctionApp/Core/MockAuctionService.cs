using System.Collections.ObjectModel;
using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class MockAuctionService : IAuctionService
{
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
        activeAuctions.Sort();
        return activeAuctions;
    }

    public Auction GetById(int id)
    {
        return _auctions.Find(a => a.Id == id && a.IsActive());      //Lambdauttryck i parantes
    }
    
    public void Add(string userName, string title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }
    
    private static readonly List<Auction> _auctions = new();
    // C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction(1, "Dog","Likes to eat pig ear",DateTime.Today.AddDays(5),"emmajoh2@kth.se");
        Auction a2 = new Auction(2, "Cat","Likes to climbs on sofa",DateTime.Today.AddDays(2),"emmajoh2@kth.se");
        Auction a3 = new Auction(3, "Lova","Likes to sleep and study",DateTime.Today.AddDays(3),"emmajoh2@kth.se");
        Auction a4 = new Auction(4, "Ended Auction","Sleeping..",DateTime.Today.AddDays(-1), "emmajoh2@kth.se"); //Wont show since it is not active;
        Auction a5 = new Auction("Vase", "emmajoh2@kth.se");
        a2.AddBid(new Bid(1, "emmajoh2@kth.se", 160000));
        a2.AddBid(new Bid(1, "emmajoh2@kth.se", 15000.50));
        _auctions.Add(a1);
        _auctions.Add(a2);
        _auctions.Add(a3);
        _auctions.Add(a4);
    }
}