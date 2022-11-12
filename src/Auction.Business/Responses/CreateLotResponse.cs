using Auction.Data.Entities;

namespace Auction.Business.Responses;

public class CreateLotResponse : BaseResponse
{
    public Lot? Lot { get; set; }
}