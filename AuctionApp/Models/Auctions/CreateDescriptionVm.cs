using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Models.Auctions;

public class CreateDescriptionVm
{
    [Required]
    [StringLength(500, ErrorMessage = "Max length 500 characters")]
    public string? Description { get; set; }

}