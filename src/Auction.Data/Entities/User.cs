namespace Auction.Data.Entities;

public class User : BaseEntity
{
    public string PasswordHash { get; set; }

    public string PasswordSalt { get; set; }
    
    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public DateTime Ts { get; set; }

    public bool Active { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();

    public ICollection<Task> Tasks { get; set; } = new HashSet<Task>();
    
    public ICollection<Lot> Lots { get; set; } = new HashSet<Lot>();
}