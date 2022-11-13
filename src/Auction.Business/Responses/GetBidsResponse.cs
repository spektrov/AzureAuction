using Auction.Data.Entities;

namespace Auction.Business.Responses;

public class GetBidsResponse : BaseResponse
{
    public ICollection<Bid>? Bids { get; set; }
}