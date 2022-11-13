using Auction.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Auction.API.Controllers;

[ApiController]
[Route("api/blobs")]
public class BlobController : BaseApiController
{
    private readonly IBlobService _blobService;

    public BlobController(IBlobService blobService)
    {
        _blobService = blobService;
    }

    [HttpGet("{lotId:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> UploadProfilePicture(Guid lotId)
    {
        var photosUri = await _blobService.GetContainerAsync(lotId.ToString());

        return Ok(photosUri);
    }
    
    [HttpPost("{lotId:guid}"), DisableRequestSizeLimit]
    public async Task<ActionResult> UploadPhotos(Guid lotId)
    {
        var files = Request.Form.Files;

        if (files.Count == 0)
        {
            return BadRequest();
        }

        foreach (var file in files)
        {
            var result = await _blobService.UploadFileBlobAsync(lotId.ToString(), file.OpenReadStream(),
                file.ContentType, file.FileName);
            var toReturn = result.AbsoluteUri;
        }
        
        return Ok();
    }
}