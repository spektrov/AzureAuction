using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;


[Route("api/lots")]
[Authorize]
[ApiController]
public class LotController : BaseApiController
{
    private readonly ILotService _lotService;
    
    public LotController(ILotService lotService)
    {
        _lotService = lotService;
    }
    
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var getLotsResponse = await _lotService.GetAllLotsAsync();

        if (!getLotsResponse.Success)
        {
            return UnprocessableEntity(getLotsResponse);
        }

        var tasksResponse = getLotsResponse.Lots;

        return Ok(tasksResponse);
    }
    
    
    [HttpGet("{holderId:guid}")]
    public async Task<IActionResult> GetHolderLots(Guid holderId)
    {
        var getLotsResponse = await _lotService.GetHolderLotsAsync(holderId);

        if (!getLotsResponse.Success)
        {
            return UnprocessableEntity(getLotsResponse);
        }

        var tasksResponse = getLotsResponse.Lots;

        return Ok(tasksResponse);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(LotRequest lotRequest)
    {
        var lot = new Lot
        {
            Name = lotRequest.Name,
            Description = lotRequest.Description,
            StartPrice = lotRequest.StartPrice,
            TimeStart = lotRequest.TimeStart,
            TimeEnd = lotRequest.TimeEnd,
            CategoryId = lotRequest.CategoryId,
            HolderId = lotRequest.HolderId,
            MaxPrice = lotRequest.StartPrice
        };

        var createLotResponse = await _lotService.CreateAsync(lot);

        if (!createLotResponse.Success)
        {
            return UnprocessableEntity(createLotResponse);
        }
        
        return Ok(createLotResponse);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteLotResponse = await _lotService.DeleteAsync(id, UserId);
        if (!deleteLotResponse.Success)
        {
            return UnprocessableEntity(deleteLotResponse);
        }

        return Ok(deleteLotResponse.LotId);
    }
}