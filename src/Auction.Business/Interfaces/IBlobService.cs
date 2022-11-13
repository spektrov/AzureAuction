using Auction.Business.Models;

namespace Auction.Business.Interfaces;

public interface IBlobService
{
    public Task<ICollection<string>> GetContainerAsync(string container);

    public Task<BlobInfo> GetBlobAsync(string name, string container);

    public Task DeleteContainerAsync(string container);

    public Task DeleteBlobAsync(string blobName, string container);

    public Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName);
}