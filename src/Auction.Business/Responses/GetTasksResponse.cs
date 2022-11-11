namespace Auction.Business.Responses;

public class GetTasksResponse : BaseResponse
{
    public IList<Auction.Data.Entities.Task> Tasks { get; set; }
}