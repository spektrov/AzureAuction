using System.Text.Json.Serialization;

namespace Auction.Business.Requests;

public class TaskUpdateRequest
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime Ts { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid UserId { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? FirstName { get; set; }
}