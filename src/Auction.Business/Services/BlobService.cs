using Auction.Business.Interfaces;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using BlobInfo = Auction.Business.Models.BlobInfo;

namespace Auction.Business.Services;

public class BlobService : IBlobService
{
    private readonly BlobServiceClient _blobServiceClient;

    public BlobService(BlobServiceClient blobServiceClient)
    {
        _blobServiceClient = blobServiceClient;
    }
    
    
    public async Task<ICollection<string>> GetContainerAsync(string container)
    {
        var result = new List<string>();
        
        var containerClient = GetContainerClient(container);

        await foreach (BlobItem blobItem in containerClient.GetBlobsAsync())
        {
            var blob = await GetUriAsync(blobItem.Name, container);
            result.Add(blob);
        }

        return result;
    }

    private async Task<string> GetUriAsync(string name, string container)
    {
        var containerClient = GetContainerClient(container);
        var blobClient = containerClient.GetBlobClient(name);
        return blobClient.Uri.AbsoluteUri;
    }

    public async Task<BlobInfo> GetBlobAsync(string name, string container)
    {
        var containerClient = GetContainerClient(container);
        var blobClient = containerClient.GetBlobClient(name);
        
        var blobDownloadInfo = await blobClient.DownloadAsync();

        return new BlobInfo(blobDownloadInfo.Value.Content, blobDownloadInfo.Value.ContentType);
    }

    public async Task<Uri> UploadFileBlobAsync(string blobContainerName, Stream content, string contentType, string fileName)
    {
        var containerClient = GetContainerClient(blobContainerName);
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(content, new BlobHttpHeaders { ContentType = contentType });
        return blobClient.Uri;
    }
    
    public async Task DeleteContainerAsync(string container)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        await containerClient.DeleteIfExistsAsync();
    }

    public async Task DeleteBlobAsync(string blobName, string container)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(container);
        var blobClient = containerClient.GetBlobClient(blobName);
        await blobClient.DeleteIfExistsAsync();
    }
    

    private BlobContainerClient GetContainerClient(string blobContainerName)
    {
        var containerClient = _blobServiceClient.GetBlobContainerClient(blobContainerName);
        containerClient.CreateIfNotExists(PublicAccessType.Blob);
        return containerClient;
    }
}