namespace Auction.Data.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }

    protected BaseEntity()
    {
        Id = new Guid();
    }
}