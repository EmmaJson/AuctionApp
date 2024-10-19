using System.ComponentModel.DataAnnotations;
using AuctionApp.Core;

namespace AuctionApp.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)]
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    // Talar om ifall nått speciellt ska gälla när man visar datat
    [Display(Name = "End Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }

    public List<BidVm> BidVMs { get; set; } = new();

    public static AuctionDetailsVm FromProject(Auction auction)
    {
        var detailsVM = new AuctionDetailsVm()
        {
            Id = auction.Id,
            Title = auction.Title,
            Description = auction.Description,
            EndDate = auction.EndDate,
            IsActive = auction.IsActive()
        };
        foreach (var bid in auction.Bids)
        {
            detailsVM.BidVMs.Add(BidVm.FromBid(bid));
        }
        return detailsVM;
    }
}