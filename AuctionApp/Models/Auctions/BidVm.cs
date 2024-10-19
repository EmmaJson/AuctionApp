using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class BidVm
{
    // Id:t ska skickas med i klienten, men inte listas i HTML-koden
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string UserName { get; set; }
    
    // Talar om ifall nått speciellt ska gälla när man visar datat
    [Display(Name = "Bidded Amount")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double Amount { get; set; }
    
    [Display(Name = "Bid time")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime BidDate { get ; set; }

    public static BidVm FromBid(Bid bid)
    {
        return new BidVm()
        {
            Id = bid.Id,
            UserName = bid.UserName,
            Amount = bid.Amount,
            BidDate = bid.BidDate,
        };
    }
}