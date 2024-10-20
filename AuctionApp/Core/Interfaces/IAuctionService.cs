namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllActive();
    
    List<Auction> GetUserWonAuctions(string userName);
    
    List<Auction> GetUserActiveAuctions(string userName);
    
    Auction GetById(int id); 
    
    void Add(string title, string description, DateTime endDate, string auctionOwnerName, double startingPrice);
    
    void UpdateDescription(int auctionId, string userName, string desription);

    void AddBid(int auctionId ,string userName, double amount);
    
}