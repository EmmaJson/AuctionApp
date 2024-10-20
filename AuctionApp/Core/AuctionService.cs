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

    public void UpdateDescription(int auctionId, string userName, string description)
    {
        Console.WriteLine($"Attempting to add bid for auction ID: {auctionId}, by user: {userName}");
        if (string.IsNullOrWhiteSpace(userName)) 
            throw new DataException("Username is required");
        // Retrieve the auction by ID
        Auction auction = _auctionPersistence.GetById(auctionId);
        if (auction == null) 
            throw new DataException("Auction not found");

        if (auction.AuctionOwnerName.Equals(userName))
        {
            // Set description
            auction.UpdateDescription(description);
        
            // Save the auction with the new bid
            _auctionPersistence.Save(auction);
        }
        else throw new InvalidOperationException("Only owner can change the description");
    }
    
    public void AddBid(int auctionId, string userName, double amount)
    {
        Console.WriteLine($"Attempting to add bid for auction ID: {auctionId}, amount: {amount}, by user: {userName}");
        if (string.IsNullOrWhiteSpace(userName)) 
            throw new DataException("Username is required");

        // Retrieve the auction by ID
        Auction auction = _auctionPersistence.GetById(auctionId);
        if (auction == null) 
            throw new DataException("Auction not found");

        // Log the current highest bid for debugging
        if (auction.Bids.Any())
        {
            double currentHighestBid = auction.Bids.Max(b => b.Amount);
            Console.WriteLine($"Current highest bid: {currentHighestBid}");
        }

        // Create and add the new bid
        Bid bid = new Bid(userName, amount);
        auction.AddBid(bid);

        // Save the auction with the new bid
        _auctionPersistence.Save(auction);
    }
}