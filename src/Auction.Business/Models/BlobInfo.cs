namespace Auction.Business.Models;

public class BlobInfo
{
    public BlobInfo(Stream content, string contentType)
    {
        Content = content;
        ContentType = contentType;
    }

    public string ContentType { get; set; }
    
    public Stream Content { get; set; }
}