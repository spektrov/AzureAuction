using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

[Route("api/bids")]
[Authorize]
[ApiController]
public class BidController : BaseApiController
{
    private readonly IBidService _bidService;


    public BidController(IBidService bidService)
    {
        _bidService = bidService;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Post(BidRequest bidRequest)
    {
        bidRequest.Ts = DateTime.Now;
        bidRequest.UserId = UserId;

        var makeBidResponse = await _bidService.CreateAsync(bidRequest);

        if (!makeBidResponse.Success)
        {
            return UnprocessableEntity(makeBidResponse);
        }
        
        return Ok(makeBidResponse.Bid);
    }
    
}