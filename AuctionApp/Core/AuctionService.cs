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

    public void Add(string userName, string title)
    {
        throw new NotImplementedException();
    }
}