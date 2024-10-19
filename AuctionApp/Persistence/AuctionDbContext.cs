using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
    
    public DbSet<BidDb> BidDbs { get; set; }
    public DbSet<AuctionDb> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)      //seed-data
    {
        AuctionDb adb = new AuctionDb()
        {
            Id = -1,                                                        // seed data
            Title = "Learn ASP.NET Core with MVC",
            Description = "Sucks ass",
            EndDate = DateTime.Now.AddDays(5),
            AuctionOwnerName = "emmajoh2@kth.se",
            StartingPrice = 100,
            BidDbs = new List<BidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(adb);

        BidDb bdb1 = new BidDb()
        {
            Id = -1,
            UserName = "julg@kth.se",
            BidDate = DateTime.Now,
            Amount = 200,
            AuctionId = -1,
        };
        BidDb bdb2 = new BidDb()
        {
            Id = -2,
            UserName = "julg@kth.se",
            BidDate = DateTime.Now,
            Amount = 250,
            AuctionId = -1,
        };
        modelBuilder.Entity<BidDb>().HasData(bdb1);
        modelBuilder.Entity<BidDb>().HasData(bdb2);
    }
}