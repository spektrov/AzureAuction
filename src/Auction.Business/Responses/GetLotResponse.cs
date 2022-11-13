using Auction.Data.Entities;
using Auction.Business.Models;

namespace Auction.Business.Responses;

public class GetLotResponse : BaseResponse
{
    public Lot? Lot { get; set; }
}