namespace Auction.Business.Requests;

public class UploadFileRequest
{
    public string FilePath { get; set; } = string.Empty;

    public string FileName { get; set; } = string.Empty;
}