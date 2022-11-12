using Auction.Data.Entities;

namespace Auction.Business.Responses;

public class GetLotResponse : BaseResponse
{
    public Lot? Lot { get; set; }
}