using System.Text.Json.Serialization;

namespace Auction.Business.Responses;

public class DeleteLotResponse : BaseResponse
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid LotId { get; set; }
}