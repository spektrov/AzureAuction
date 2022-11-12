using Auction.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auction.API.Controllers;

[Route("api/categories")]
[ApiController]
public class CategoryController : BaseApiController
{
    private readonly AuctionDbContext _auctionDbContext;


    public CategoryController(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _auctionDbContext.Categories.ToListAsync();

        return Ok(categories);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _auctionDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
        {
            return BadRequest("Not found");
        }
        
        return Ok(category);
    }
    
    [HttpGet("{name:alpha}")]
    public async Task<IActionResult> GetById(string name)
    {
        var category = await _auctionDbContext.Categories.FirstOrDefaultAsync(x => x.Name == name);

        if (category == null)
        {
            return BadRequest("Not found");
        }
        
        return Ok(category);
    }
}