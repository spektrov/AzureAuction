using Auction.Data.Entities;

namespace Auction.Business.Responses;

public class MakeBidResponse : BaseResponse
{
    public Bid? Bid { get; set; }
}