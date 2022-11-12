using Auction.Business.Responses;
using Auction.Data.Entities;

namespace Auction.Business.Interfaces;

public interface ILotService
{
    public Task<GetLotsResponse> GetAllLotsAsync();

    public Task<GetLotsResponse> GetHolderLotsAsync(Guid holderId);

    public Task<CreateLotResponse> CreateAsync(Lot lot);

    public Task<DeleteLotResponse> DeleteAsync(Guid lotId, Guid holderId);
}