using Auction.Business.Interfaces;
using Auction.Business.Responses;
using Auction.Data;
using Auction.Data.Entities;
using Microsoft.EntityFrameworkCore;


namespace Auction.Business.Services;


public interface ILotRepository
{
    public Task<Lot?> GetLotByIdAsync(Guid id);
    public Task<ICollection<Lot>> GetAllLotsAsync();
}


public class LotRepository : ILotRepository
{
    private readonly AuctionDbContext _auctionDbContext;

    public LotRepository(AuctionDbContext auctionDbContext)
    {
        _auctionDbContext = auctionDbContext;
    }


    public async Task<Lot?> GetLotByIdAsync(Guid id)
    {
        return await _auctionDbContext.Lots
            .Include(x => x.Category)
            .Where(x => x.TimeEnd > DateTime.Now)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    
    
    public async Task<ICollection<Lot>> GetAllLotsAsync()
    {
        return await _auctionDbContext.Lots
            .Include(x => x.Category)
            .ToListAsync();
    }
}




public class LotService : ILotService
{
    private readonly AuctionDbContext _auctionDbContext;
    private readonly IBlobService _blobService;
    private readonly ILotRepository _lotRepository;

    public LotService(AuctionDbContext auctionDbContext, IBlobService blobService, ILotRepository lotRepository)
    {
        _auctionDbContext = auctionDbContext;
        _blobService = blobService;
        _lotRepository = lotRepository;
    }

    
    public async Task<GetLotResponse> GetLotByIdAsync(Guid id)
    {
        var lot = await _lotRepository.GetLotByIdAsync(id);

        return lot == null ? 
            new GetLotResponse { Success = false } : 
            new GetLotResponse { Success = true, Lot = lot };
    }

    public async Task<GetLotsResponse> GetAllLotsAsync()
    {
        var lots = await _lotRepository.GetAllLotsAsync();

        return new GetLotsResponse { Success = true, Lots = lots };
    }

    public async Task<GetLotsResponse> GetHolderLotsAsync(Guid holderId)
    {
        var lots = (await _lotRepository.GetAllLotsAsync())
            .Where(x => x.UserId == holderId)
            .ToList();

        return new GetLotsResponse { Success = true, Lots = lots };
    }
    
    public async Task<GetLotsResponse> GetBoughtByUser(Guid userId)
    {
        var group =  _auctionDbContext.Bids
            .Include(x => x.Lot)
            .Where(x => x.UserId == userId)
            .GroupBy(b => b.Lot);

        var boughtLotIds = new List<Guid>();
        foreach (var lotBid in group)
        {
            boughtLotIds.Add(lotBid.First(x => x.Price == lotBid.Max(bid => bid.Price)).LotId);
        }
        
        var endedLots = _auctionDbContext.Lots.Where(x => x.TimeEnd <= DateTime.Now);
        var result = new List<Lot>();
        foreach (var ended in endedLots)
        {
            if (boughtLotIds.Any(l => l == ended.Id)) result.Add(ended);
        }

        return new GetLotsResponse() { Success = true, Lots = result };
    }
    
    public async Task<CreateLotResponse> CreateAsync(Lot lot)
    {
       await _auctionDbContext.Lots.AddAsync(lot);
       
        var saveResponse = await _auctionDbContext.SaveChangesAsync();
        
        if (saveResponse >= 0)
        {
            return new CreateLotResponse
            {
                Success = true,
                Lot = lot
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

        if (lot.UserId != holderId || lot.TimeEnd <= DateTime.Now)
        {
            return new DeleteLotResponse
            {
                Success = false,
                Error = "You don't have access to delete this lot",
                ErrorCode = "T02"
            };
        }

        await _blobService.DeleteContainerAsync(lotId.ToString());
        
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