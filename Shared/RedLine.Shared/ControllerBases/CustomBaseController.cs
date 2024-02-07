using Microsoft.AspNetCore.Mvc;
using RedLine.Shared.Dtos;

namespace RedLine.Shared.ControllerBases;

public class CustomBaseController : ControllerBase
{
    public IActionResult CreateActionResultInstance<T>(InternalApiResponseDto<T> internalApiResponseDto)
    {
        return new ObjectResult(internalApiResponseDto)
        {
            StatusCode= internalApiResponseDto.HttpStatusCode
        };
    }
}
