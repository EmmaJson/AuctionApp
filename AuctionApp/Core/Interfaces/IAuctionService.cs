namespace AuctionApp.Core.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllByUserName(string userName);
    
    Auction GetById(int id);
    
    void Add(string userName, string title);
}