namespace Auction.Business.Responses;

public class TaskResponse : BaseResponse
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public DateTime Ts { get; set; }
}