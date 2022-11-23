using Auction.Business.Responses;
using Auction.Business.Services;
using Auction.Data.Entities;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace Auction.Tests;

public class LotServiceTests
{

    [Test]
    public async Task GetLotByIdAsync_ExistingId_GetLotResponse()
    {
        var mockRepo = new Mock<ILotRepository>();

        var id = new Guid("0BB6D15E-C312-4CEE-8E84-1F81BFD6BDFC");

        var lotService = new LotService(null, null, mockRepo.Object);
        mockRepo
            .Setup(x => x.GetLotByIdAsync(id))
            .ReturnsAsync(stubLots.ElementAt(0));


        var expected = new GetLotResponse()
        {
            Success = true,
            Lot = stubLots.ElementAt(0)
        };

        var actual = await lotService.GetLotByIdAsync(id);
        
        Assert.AreEqual(expected.Lot.Id, actual.Lot.Id);
        Assert.AreEqual(expected.Success, actual.Success);
    }


    [Test]
    public async Task GetAllLotsAsync_ReturnsGetLotsResponse()
    {
        var mockRepo = new Mock<ILotRepository>();
        
        var lotService = new LotService(null, null, mockRepo.Object);
        mockRepo
            .Setup(x => x.GetAllLotsAsync())
            .ReturnsAsync(stubLots);


        var expected = new GetLotsResponse()
        {
            Success = true,
            Lots = stubLots
        };

        var actual = await lotService.GetAllLotsAsync();

        Assert.NotNull(actual.Lots);
        Assert.AreEqual(expected.Success, actual.Success);
        Assert.AreEqual(expected.Lots.Count, actual.Lots.Count);
        for (int i = 0; i < actual.Lots.Count; i++)
        {
            Assert.AreEqual(expected.Lots.ElementAt(i).Id, actual.Lots.ElementAt(i).Id);
        }
    }

    [Test]
    public async Task GetHolderLotsAsync_HolderId_Returns1Lot()
    {
        var userId = new Guid("7CE054A1-00A0-4357-87C5-68D6899D344B");
        
        var mockRepo = new Mock<ILotRepository>();
        
        var lotService = new LotService(null, null, mockRepo.Object);
        mockRepo
            .Setup(x => x.GetAllLotsAsync())
            .ReturnsAsync(stubLots);
        
        var expected = new GetLotsResponse()
        {
            Success = true,
            Lots = new List<Lot>() { stubLots.ElementAt(1) }
        };

        var actual = await lotService.GetHolderLotsAsync(userId);
        
        Assert.NotNull(actual.Lots);
        Assert.AreEqual(expected.Success, actual.Success);
        Assert.AreEqual(expected.Lots.Count, actual.Lots.Count);
        for (int i = 0; i < actual.Lots.Count; i++)
        {
            Assert.AreEqual(expected.Lots.ElementAt(i).Id, actual.Lots.ElementAt(i).Id);
        }
    }



    private ICollection<Lot> stubLots =>
         new List<Lot>()
    {
        new Lot()
        {
            Id = new Guid("0BB6D15E-C312-4CEE-8E84-1F81BFD6BDFC"),
            CategoryId = new Guid("7A0EB90E-D84D-4358-B99E-1A6BB2A1CBFB"),
            Name = "Sport Car X",
            Description = "Black 2016",
            StartPrice = 200000,
            MaxPrice = 250000,
            TimeStart = DateTime.Now - TimeSpan.FromHours(6),
            TimeEnd = DateTime.Now + TimeSpan.FromDays(2),
            UserId = new Guid("73E7A78D-C6B7-499E-A3E9-1C0C82C7FA4B")
        },
        new Lot()
        {
            Id = new Guid("55B2A218-39E8-4DCA-AAC4-752040DE3B17"),
            CategoryId = new Guid("7A0EB90E-D84D-4358-B99E-1A6BB2A1CBFB"),
            Name = "Motorbike Hyundai",
            Description = "Blue 2018",
            StartPrice = 100000,
            MaxPrice = 210000,
            TimeStart = DateTime.Now - TimeSpan.FromHours(6),
            TimeEnd = DateTime.Now + TimeSpan.FromDays(2),
            UserId = new Guid("7CE054A1-00A0-4357-87C5-68D6899D344B")
        }
    };

}