namespace AuctionApp.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAuctions();
    List<Auction> GetUserWonAuctions(string userName);
    List<Auction> GetUserActiveAuctions(string userName);
    Auction GetById(int id); 
    void Save(Auction auction);
}