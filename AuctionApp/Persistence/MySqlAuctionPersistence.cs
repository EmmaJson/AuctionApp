using System.Data;
using AuctionApp.Core;
using AuctionApp.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence;

public class MySqlAuctionPersistence : IAuctionPersistence
{
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public MySqlAuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public List<Auction> GetAllAuctions()
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now)
            .ToList();
        
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        result.Sort();
        return result;
    }

    public List<Auction> GetUserWonAuctions(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate <= DateTime.Now && a.BidDbs.First().UserName.Equals(userName))
            .ToList();
        
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        result.Sort();
        return result;
    }

    public List<Auction> GetUserActiveAuctions(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now && a.BidDbs.Any(b => b.UserName.Equals(userName)))
            .ToList();
        
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        result.Sort();
        return result;
    }
    public Auction GetById(int id)
    {
        Console.WriteLine($"Searching for auction with ID: {id}");
        AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(a => a.Id == id)
            .Include(a => a.BidDbs)
            .FirstOrDefault();   // Null if not found!

        if (auctionDb == null)
        {
            Console.WriteLine("Auction not found in the database."); // Log if not found
            throw new DataException("Auction not found");
        }

        Console.WriteLine($"Auction found: {auctionDb.Title}"); // Log auction title or relevant info

        // Log bid data
        foreach (var bid in auctionDb.BidDbs)
        {
            Console.WriteLine($"Bid ID: {bid.Id}, Bid Date: {bid.BidDate}");
        }

        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bidDb in auctionDb.BidDbs)
        {
            Console.WriteLine($"Mapping BidDb - Id: {bidDb.Id}, BidDate: {bidDb.BidDate}");
            Bid bid = _mapper.Map<Bid>(bidDb);
            Console.WriteLine($"Mapped Bid - Id: {bid.Id}, BidDate: {bid.BidDate}");
            auction.AddBid(bid);
        }

        return auction;
    }


    public void Save(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
    
        var existingAuction = _dbContext.AuctionDbs
            .Include(a => a.BidDbs)
            .FirstOrDefault(a => a.Id == adb.Id);

        if (existingAuction != null)
        {
            // Update existing auction properties
            _dbContext.Entry(existingAuction).CurrentValues.SetValues(adb);

            // Check for new bids
            foreach (var bid in auction.Bids)
            {
                // Create a new BidDb instance to avoid tracking issues
                var existingBid = existingAuction.BidDbs.FirstOrDefault(b => b.UserName == bid.UserName && b.Amount == bid.Amount);

                if (existingBid == null) // Only add if the bid doesn't already exist
                {
                    var bidDb = _mapper.Map<BidDb>(bid);
                    existingAuction.BidDbs.Add(bidDb);
                }
            }
        }
        else
        {
            // If the auction doesn't exist, add it along with bids
            _dbContext.AuctionDbs.Add(adb);
        }

        _dbContext.SaveChanges();
    }
}