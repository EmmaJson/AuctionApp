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
            .Where(a => a.EndDate <= DateTime.Now)
            .ToList();
        
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            if (auction.Bids.First().UserName.Equals(userName))
            {
                result.Add(auction);
            }
        }
        result.Sort();
        return result;
    }

    public List<Auction> GetUserActiveAuctions(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now)
            .ToList();
        
        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            if (auction.Bids.Any(b => b.UserName == userName)) {
                result.Add(auction);
            }
        }
        result.Sort();
        return result;
    }
    
    public Auction GetById(int id)
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(a => a.Id == id)
            .Include(a => a.BidDbs)
            .FirstOrDefault();   // Null if not found!
        
        if (auctionDb == null) throw new DataException("Project not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bidDb in auctionDb.BidDbs)
        {
            Bid bid = _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        return auction;
    }

    public void Save(Auction auction)
    {
        AuctionDb adb = _mapper.Map<AuctionDb>(auction);
        _dbContext.AuctionDbs.Add(adb);
        _dbContext.SaveChanges();
    }
}