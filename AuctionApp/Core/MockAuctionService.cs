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

    public List<Auction> GetUserWonAuctions(string userName)
    {
        List<Auction> wonAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (!auction.IsActive() && auction.Bids.First().UserName.Equals(userName))
            {
                wonAuctions.Add(auction);
            }
        }
        wonAuctions.Sort();
        return wonAuctions;
    }

    public List<Auction> GetUserActiveAuctions(string userName)
    {
        List<Auction> myActiveAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (auction.IsActive())
            {
                foreach (Bid bid in auction.Bids)
                {
                    if (bid.UserName.Equals(userName) && !myActiveAuctions.Contains(auction))
                    {
                        myActiveAuctions.Add(auction);
                    }
                }
            }
        }
        myActiveAuctions.Sort();
        return myActiveAuctions;
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
        Auction a1 = new Auction(1, "Dog","Likes to eat pig ear",DateTime.Today.AddDays(5),"julg@kth.se", 100);
        Auction a2 = new Auction(2, "Cat","Likes to climbs on sofa",DateTime.Today.AddDays(2),"julg@kth.se", 200);
        Auction a3 = new Auction(3, "Lova","Likes to sleep and study",DateTime.Today.AddDays(3),"julg@kth.se", 300.50);
        Auction a4 = new Auction(4, "Ended Auction","Sleeping..",DateTime.Today.AddDays(-1), "julg@kth.se", 400); //Wont show since it is not active;
        //Auction a5 = new Auction("Vase", "emmajoh2@kth.se");
        a1.AddBid(new Bid(1, "emmajoh2@kth.se", 201));
        a2.AddBid(new Bid(2, "emmajoh2@kth.se", 16000));
        a3.AddBid(new Bid(3, "emmajoh2@kth.se", 100050.50));
        
        _auctions.Add(a1);
        _auctions.Add(a2);
        _auctions.Add(a3);
        _auctions.Add(a4);
        //_auctions.Add(a5);
    }
}