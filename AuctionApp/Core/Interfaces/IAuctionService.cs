namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllActive();
    
    Auction GetById(int id); 
    
    void Add(string userName, string title);
}