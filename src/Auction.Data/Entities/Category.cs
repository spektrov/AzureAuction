using Auction.Data.Entities;

namespace Auction.Data.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}