using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    
    [Display(Name = "Auction Item")]
    public string Title { get; set; }
    
    [Display(Name = "Auction Owner")]
    public string UserName { get; set; }
    [Display(Name = "Item Descrpition")]
    public string Description { get; set; }
    
    // Talar om ifall nått speciellt ska gälla när man visar datat
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDate { get; set; }
    [Display(Name = "Starting Price")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double StartingPrice { get; set; }
    [Display(Name = "Active Auction")]
    public bool IsActive { get; set; }

    public List<BidVm> BidVMs { get; set; } = new();

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVm = new AuctionDetailsVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            UserName = auction.AuctionOwnerName,
            Description = auction.Description,
            EndDate = auction.EndDate,
            StartingPrice = auction.StartingPrice,
            IsActive = auction.IsActive()
        };
        foreach (var bid in auction.Bids)
        {
            detailsVm.BidVMs.Add(BidVm.FromBid(bid));
        }
        return detailsVm;
    }
}