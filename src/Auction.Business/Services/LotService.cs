using Auction.Business.Interfaces;
using Auction.Business.Responses;
using Auction.Data;
using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auction.Business.Services;

public class LotService : ILotService
{
    private readonly AuctionDbContext _auctionDbContext;

    public LotService(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }


    public async Task<GetLotsResponse> GetAllLotsAsync()
    {
        var lots = await _auctionDbContext.Lots
            .Include(x => x.Category)
            .ToListAsync();

        return new GetLotsResponse { Success = true, Lots = lots };
    }

    public async Task<GetLotsResponse> GetHolderLotsAsync(Guid holderId)
    {
        var lots = await _auctionDbContext.Lots
            .Where( x => x.UserId == holderId)
            .Include(x => x.Category)
            .ToListAsync();

        return new GetLotsResponse { Success = true, Lots = lots };
    }
    
    public async Task<GetLotResponse> GetLotByIdAsync(Guid id)
    {
        var lot = await _auctionDbContext.Lots
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == id);

        return lot == null ? 
            new GetLotResponse { Success = false } : 
            new GetLotResponse { Success = true, Lot = lot };
    }

    public async Task<CreateLotResponse> CreateAsync(Lot lot)
    {
        var createdLot = await _auctionDbContext.Lots.AddAsync(lot);
        
        var saveResponse = await _auctionDbContext.SaveChangesAsync();
        
        if (saveResponse >= 0)
        {
            return new CreateLotResponse
            {
                Success = true,
                Lot = createdLot.Entity
            };
        }
        return new CreateLotResponse
        {
            Success = false,
            Error = "Unable to create lot",
            ErrorCode = "T05"
        };
    }

    public async Task<DeleteLotResponse> DeleteAsync(Guid lotId, Guid holderId)
    {
        var lot = await _auctionDbContext.Lots.FirstOrDefaultAsync(t => t.Id == lotId);

        if (lot == null)
        {
            return new DeleteLotResponse
            {
                Success = false,
                Error = "Lot not found",
                ErrorCode = "T01"
            };
        }

        if (lot.UserId != holderId)
        {
            return new DeleteLotResponse
            {
                Success = false,
                Error = "You don't have access to delete this lot",
                ErrorCode = "T02"
            };
        }

        _auctionDbContext.Lots.Remove(lot);

        var saveResponse = await _auctionDbContext.SaveChangesAsync();

        if (saveResponse >= 0)
        {
            return new DeleteLotResponse
            {
                Success = true,
                LotId = lotId
            };
        }

        return new DeleteLotResponse
        {
            Success = false,
            Error = "Unable to delete lot",
            ErrorCode = "T03"
        };
    }
}