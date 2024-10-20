using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace AuctionApp.Models.Auctions;

public class CreateAuctionVm
{
    [Required]
    [StringLength(128, ErrorMessage = "Max length 128 characters")]
    public string? Title { get; set; }

    [Required]
    [StringLength(500, ErrorMessage = "Max length 500 characters")]
    public string? Description { get; set; }
    
    [Required]
    public DateTime EndDate { get; set; }
    
    [Required]
    public double StartingPrice { get; set; }
}