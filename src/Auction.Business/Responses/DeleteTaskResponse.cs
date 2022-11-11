using System.Text.Json.Serialization;

namespace Auction.Business.Responses;

public class DeleteTaskResponse : BaseResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid TaskId { get; set; }
}