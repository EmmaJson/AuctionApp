using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models.Auctions;

public class CreateBidVm
{

    [Required(ErrorMessage = "Amount is required")]
    public double Amount { get; set; }
}