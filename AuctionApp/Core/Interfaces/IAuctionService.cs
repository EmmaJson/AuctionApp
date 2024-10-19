namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllActive();
    
    List<Auction> GetUserWonAuctions(string userName);
    
    List<Auction> GetUserActiveAuctions(string userName);
    
    Auction GetById(int id); 
    
    void Add(string userName, string title);
}