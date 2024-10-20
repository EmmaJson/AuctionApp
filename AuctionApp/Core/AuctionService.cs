using System.Data;
using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }
    
    public List<Auction> GetAllActive()
    {
        List<Auction> auctions = _auctionPersistence.GetAllAuctions();
        return auctions;
    }

    public List<Auction> GetUserWonAuctions(string userName)
    {
        List<Auction> auctions = _auctionPersistence.GetUserWonAuctions(userName);
        return auctions;
    }

    public List<Auction> GetUserActiveAuctions(string userName)
    {
        List<Auction> auctions = _auctionPersistence.GetUserActiveAuctions(userName);
        return auctions;
    }

    public Auction GetById(int id)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }

    public void Add(string title, string description, DateTime endDate, string auctionOwnerName, double startingPrice)
    {
        if (title == null || title.Length > 128) throw new DataException("Title is required");
        if (description == null || description.Length > 500) throw new DataException("Description is required");        //TODO: egen exception
        if (endDate < DateTime.Now) throw new DataException("End date is required");
        if (auctionOwnerName == null) throw new DataException("Auction owner name is required");
        if (startingPrice < 0) throw new DataException("Starting price is required");
        
        Auction auction = new Auction(title, description, endDate, auctionOwnerName, startingPrice);
        _auctionPersistence.Save(auction);
    }
}