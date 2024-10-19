using AuctionApp.Core.Interfaces;

namespace AuctionApp.Core;

public class AuctionService(IAuctionPersistence auctionPersistence) : IAuctionService
{
    public List<Auction> GetAllActive()
    {
        List<Auction> auctions = auctionPersistence.GetAllAuctions();
        return auctions;
    }

    public List<Auction> GetUserWonAuctions(string userName)
    {
        List<Auction> auctions = auctionPersistence.GetUserWonAuctions(userName);
        return auctions;
    }

    public List<Auction> GetUserActiveAuctions(string userName)
    {
        List<Auction> auctions = auctionPersistence.GetUserActiveAuctions(userName);
        return auctions;
    }

    public Auction GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(string userName, string title)
    {
        throw new NotImplementedException();
    }
}