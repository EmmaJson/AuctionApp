using System.ComponentModel.DataAnnotations;

namespace AuctionApp.Persistence;

public class AuctionDb
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Title { get; set; }
    
    [Required]
    [MaxLength(500)]
    public string Description { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime EndDate { get; set; }
    
    [Required]
    public string AuctionOwnerName { get; set; }
    
    [Required]
    public double StartingPrice { get; set; }
    
    //Navigation property
    public List<BidDb> BidDbs { get; set; } = new List<BidDb>();
}