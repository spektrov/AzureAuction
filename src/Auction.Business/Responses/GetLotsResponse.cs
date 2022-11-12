using Auction.Data.Entities;

namespace Auction.Business.Responses;

public class GetLotsResponse : BaseResponse
{
    public ICollection<Lot>? Lots { get; set; }
}