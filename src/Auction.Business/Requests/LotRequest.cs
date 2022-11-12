using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auction.Business.Requests;

public class LotRequest
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Description]
    public string Description { get; set; } = string.Empty;
    
    [Description]
    public decimal StartPrice { get; set; }
    
    public DateTime TimeStart { get; set; }
    
    public DateTime TimeEnd { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid HolderId { get; set; }
    
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Guid CategoryId { get; set; }
}