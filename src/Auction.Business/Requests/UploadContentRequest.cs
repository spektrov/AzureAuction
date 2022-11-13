namespace Auction.Business.Requests;

public class UploadContentRequest
{
    public string Content { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
}