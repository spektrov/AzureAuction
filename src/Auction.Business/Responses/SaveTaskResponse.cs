using Task = Auction.Data.Entities.Task;

namespace Auction.Business.Responses;


public class SaveTaskResponse : BaseResponse
{
    public Task? Task { get; set; }
}