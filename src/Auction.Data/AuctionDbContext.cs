using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Task = Auction.Data.Entities.Task;

namespace Auction.Data;

public class AuctionDbContext : DbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public DbSet<Task> Tasks { get; set; }

    public DbSet<Bid> Bids { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Lot> Lots { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    //public DbSet<LotHolder> LotHolders { get; set; }
    
    //public DbSet<Tariff> Tariffs { get; set; }

    
    
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Task>();
        modelBuilder.Entity<RefreshToken>();
        
        modelBuilder.Entity<Category>().HasData(_categories);
        //modelBuilder.Entity<Tariff>().HasData(_tariffs);
        modelBuilder.Entity<User>();
        modelBuilder.Entity<Lot>();
        modelBuilder.Entity<Bid>();
        //modelBuilder.Entity<LotHolder>();

        base.OnModelCreating(modelBuilder);
    }
    

    // private static readonly IList<Tariff> _tariffs = new List<Tariff>()
    // {
    //     new () {Id = Guid.Parse("EF0BB22C-2A87-4A26-ACDA-F3BC84C2FEBF"), Name = "Primary", Price = 0, MaxLotNumber = 5},
    // };
    
    private static IList<Category> _categories = new List<Category>()
    {
        new() {Id = Guid.Parse("D7D20F1E-E5AC-40E1-8A89-89E3428D92DF"), Name = "Art"},
        new () {Id = Guid.Parse("1A511D1A-1CB8-4578-B82D-E5DA10C3D49C"), Name = "Vehicle"},
        new () {Id = Guid.Parse("55216863-17E6-48A8-95F6-7169DC8A257D"), Name = "Gadget"}
    };

    #region InitializeNotUsed
    // private static readonly IList<User> _users = new List<User>()
    // {
    //     new () {Id = Guid.Parse("0434F004-3EC2-459E-B2B9-C4C4E99817C1"), Email = "denys.spektrov@nure.ua", },
    //     new () {Id = Guid.Parse("B89FEDBF-69EE-479B-986E-A08CC93B71A8"),  Email = "nazarii.shcerbak@nure.ua", },
    //     new () {Id = Guid.Parse("C9C9A0AF-5D9D-43DA-B754-0B410260ECD6"),  Email = "andrii.babanin@nure.ua", },
    // };
    //
    //
    // private static readonly IList<LotHolder> _holders = new List<LotHolder>()
    // {
    //     new () {Id = Guid.Parse("FC231F52-4DF0-49BA-985A-F414BA6F511A"), UserId = _users.ElementAt(0).Id, TariffId = _tariffs.ElementAt(0).Id, LotsLeft = _tariffs.ElementAt(0).MaxLotNumber},
    //     new () {Id = Guid.Parse("7ACE44D7-964B-43FA-8746-BFBEB00D1F1A"), UserId = _users.ElementAt(1).Id, TariffId = _tariffs.ElementAt(0).Id, LotsLeft = _tariffs.ElementAt(0).MaxLotNumber},
    //     new () {Id = Guid.Parse("B7CB6165-22E1-4811-BD0A-C0A2989179EA"), UserId = _users.ElementAt(2).Id, TariffId = _tariffs.ElementAt(0).Id, LotsLeft = _tariffs.ElementAt(0).MaxLotNumber},
    // };
    //
    //
    //
    // private static readonly IList<Lot> _lots = new List<Lot>()
    // {
    //     new (){Id = Guid.Parse("0C13C063-146C-4EEB-AE43-0F01AF8F7DC3"), Name = "Iphone 12 128gb Gray", Description = "Як новий. Акумулятор 80%", StartPrice = 820, TimeStart = DateTime.Now, TimeEnd = DateTime.Now + TimeSpan.FromDays(20), HolderId = _holders.ElementAt(1).Id, CategoryId = _categories.ElementAt(2).Id},
    //     new (){Id = Guid.Parse("F1BCDF5D-1563-491B-80B8-586C6A649666"), Name = "Kia Rio 2015", Description = "Пробіг 200000", StartPrice = 12000, TimeStart = DateTime.Now, TimeEnd = DateTime.Now + TimeSpan.FromDays(40), HolderId = _holders.ElementAt(0).Id, CategoryId = _categories.ElementAt(1).Id},
    //     new (){Id = Guid.Parse("F77BC0FA-8CED-4408-91A7-4BB09B8CE9DD"), Name = "Sven Maiers Blue Birds", Description = "Original.", StartPrice = 1500, TimeStart = DateTime.Now, TimeEnd = DateTime.Now + TimeSpan.FromDays(10), HolderId = _holders.ElementAt(1).Id, CategoryId = _categories.ElementAt(0).Id},
    //     new (){Id = Guid.Parse("D216A184-D072-46EA-9FE2-A34FC17880C0"), Name = "Aser Aspire", Description = "5 років у використанні", StartPrice = 500, TimeStart = DateTime.Now, TimeEnd = DateTime.Now + TimeSpan.FromDays(25), HolderId = _holders.ElementAt(2).Id, CategoryId = _categories.ElementAt(1).Id},
    // };
    //
    // private static readonly IList<Bid> _bids = new List<Bid>()
    // {
    //     new () { Id = Guid.Parse("0AAF6CF3-EE44-4A60-9C10-5E71323BD212"), LotId = _lots.ElementAt(0).Id, UserId = _users.ElementAt(1).Id, Price = 850, Time = DateTime.Now},
    //     new () { Id = Guid.Parse("26E86B23-0A1F-4BE1-BBFD-7284039A43B1"), LotId = _lots.ElementAt(0).Id, UserId = _users.ElementAt(0).Id, Price = 900, Time = DateTime.Now + TimeSpan.FromHours(4)},
    //     new () { Id = Guid.Parse("E63A0208-B40F-46AC-ADD9-07E2E7C81011"), LotId = _lots.ElementAt(1).Id, UserId = _users.ElementAt(0).Id, Price = 12100, Time = DateTime.Now + TimeSpan.FromHours(1)},
    //     new () { Id = Guid.Parse("9C7D2956-835F-4E14-9D8F-5F755E930186"), LotId = _lots.ElementAt(1).Id, UserId = _users.ElementAt(2).Id, Price = 12500, Time = DateTime.Now + TimeSpan.FromHours(10)},
    //     new () { Id = Guid.Parse("21DD5463-7EE6-4FE7-8235-4A35B32B7836"), LotId = _lots.ElementAt(2).Id, UserId = _users.ElementAt(1).Id, Price = 1600, Time = DateTime.Now + TimeSpan.FromHours(3)},
    //     new () { Id = Guid.Parse("A2997287-D311-49FE-9D1B-B80EE586ECBB"), LotId = _lots.ElementAt(2).Id, UserId = _users.ElementAt(2).Id, Price = 2200, Time = DateTime.Now + TimeSpan.FromHours(7)},
    //     new () { Id = Guid.Parse("506FFC04-C868-4802-9393-ECE163EB59E0"), LotId = _lots.ElementAt(3).Id, UserId = _users.ElementAt(0).Id, Price = 550, Time = DateTime.Now + TimeSpan.FromHours(1)},
    //     new () { Id = Guid.Parse("1F25B0E1-F7E5-4B7D-813C-7635A722C97C"), LotId = _lots.ElementAt(3).Id, UserId = _users.ElementAt(1).Id, Price = 570, Time = DateTime.Now + TimeSpan.FromHours(12)},
    // };
    #endregion
}