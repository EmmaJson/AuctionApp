using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionVm
{
    // Id:t ska skickas med i klienten, men inte listas i HTML-koden
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    // Talar om ifall nått speciellt ska gälla när man visar datat
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            EndDate = auction.EndDate,
            IsActive = auction.IsActive()
        };
    }
}
