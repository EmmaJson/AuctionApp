using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionVm
{
    // Id:t ska skickas med i klienten, men inte listas i HTML-koden
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    [Display(Name = "Auction Item")]
    public string Title { get; set; }
    
    [Display(Name = "Auction Owner")]
    public string UserName { get; set; }
    [Display(Name = "Item Description")]
    public string Description { get; set; }
    
    // Talar om ifall nått speciellt ska gälla när man visar datat
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Starting Price")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double StartingPrice { get; set; }
    public bool IsActive { get; set; }

    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            UserName = auction.AuctionOwnerName,
            EndDate = auction.EndDate,
            StartingPrice = auction.StartingPrice,
            IsActive = auction.IsActive()
        };
    }
}
