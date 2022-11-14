using Auction.Business.Requests;
using Auction.Business.Responses;

namespace Auction.Business.Interfaces;

public interface IBidService
{
    public Task<MakeBidResponse> CreateAsync(BidRequest request);

    public Task<GetBidsResponse> GetAllAsync();
    
    public Task<GetBidsResponse> GetByUserAsync(Guid userId);
    public Task<GetBidsResponse> GetByLotAsync(Guid lotId);

    public Task<MakeBidResponse> GetByIdAsync(Guid id);
    
}