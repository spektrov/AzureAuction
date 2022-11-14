using Auction.Business.Interfaces;
using Auction.Business.Requests;
using Auction.Business.Responses;
using Auction.Data;
using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Auction.Business.Services;

public class BidService : IBidService
{
    private readonly AuctionDbContext _auctionDbContext;

    public BidService(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }

    
    public async Task<MakeBidResponse> CreateAsync(BidRequest request)
    {
        var lotBids = (await GetByLotAsync(request.LotId)).Bids;
        
        if (lotBids is { Count: > 0 } && lotBids.Max(x => x.Price) >= request.Price)
        {
            return new MakeBidResponse()
            {
                Success = false,
                Error = "Not the max price",
                ErrorCode = "T01"
            };
        }

        var bid = new Bid
        {
            LotId = request.LotId,
            UserId = request.UserId,
            Time = request.Ts,
            Price = request.Price
        };

        var updateLot = await _auctionDbContext.Lots.FirstOrDefaultAsync(x => x.Id == request.LotId);
        if (lotBids?.Count == 0 && request.Price < updateLot?.MaxPrice)
        {
            return new MakeBidResponse
            {
                Success = false,
                Error = "Not the max price",
                ErrorCode = "T02"
            };
        }
        
        await _auctionDbContext.Bids.AddAsync(bid);
        
        if (updateLot != null) updateLot.MaxPrice = request.Price;
        
        var saveResponse = await _auctionDbContext.SaveChangesAsync();
        
        if (saveResponse >= 0)
        {
            return new MakeBidResponse()
            {
                Success = true,
                Bid = bid
            };
        }
        return new MakeBidResponse()
        {
            Success = false,
            Error = "Unable to create bid",
            ErrorCode = "T02"
        };
    }

    public async Task<GetBidsResponse> GetAllAsync()
    {
        var bids = await _auctionDbContext.Bids
            .Include(x => x.Lot)
            .Include(x => x.User)
            .ToListAsync();

        return new GetBidsResponse() { Success = true, Bids = bids };
    }
    

    public async Task<GetBidsResponse> GetByUserAsync(Guid userId)
    {
        var bids = await _auctionDbContext.Bids
            .Include(x => x.Lot)
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return new GetBidsResponse() { Success = true, Bids = bids };
    }

    public async Task<GetBidsResponse> GetByLotAsync(Guid lotId)
    {
        var bids = await _auctionDbContext.Bids
            .Include(x => x.Lot)
            .Include(x => x.User)
            .Where(x => x.LotId == lotId)
            .ToListAsync();

        return new GetBidsResponse() { Success = true, Bids = bids };
    }

    public async Task<MakeBidResponse> GetByIdAsync(Guid id)
    {
        var bid = await _auctionDbContext.Bids
            .Include(x => x.Lot)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);

        return bid == null ? 
            new MakeBidResponse() { Success = false } : 
            new MakeBidResponse() { Success = true, Bid = bid };
    }
}