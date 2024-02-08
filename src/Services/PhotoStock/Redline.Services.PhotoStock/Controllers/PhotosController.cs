using Microsoft.AspNetCore.Mvc;
using Redline.Services.PhotoStock.Models;
using RedLine.Shared.ControllerBases;
using RedLine.Shared.Dtos;

namespace Redline.Services.PhotoStock.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PhotosController : CustomBaseController
{
    [HttpPost]
    public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
    {
        if (photo?.Length > 0)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);

            using var stream = new FileStream(path, FileMode.Create);
            await photo.CopyToAsync(stream, cancellationToken);

            var returnPath = $"photos/{photo.FileName}";

            PhotoDto photoDto = new() { Url = returnPath };

            return CreateActionResultInstance(InternalApiResponseDto<PhotoDto>.Success(photoDto,200));
        }
        return CreateActionResultInstance(InternalApiResponseDto<PhotoDto>.Fail("Photo is empty",400));
    }

    [HttpDelete]
    public IActionResult PhotoDelete(string photoUrl)
    {
        var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
        if(!System.IO.File.Exists(path))
        {
            return CreateActionResultInstance(InternalApiResponseDto<NoContent>.Fail("Photo is not found", 404));
        }
        System.IO.File.Delete(path);
        return CreateActionResultInstance(InternalApiResponseDto<NoContent>.Success(204));
    }
}
