using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RedLine.Shared.Dtos;
using RedLine.Services.Catalog.Models;
using RedLine.Services.Catalog.Services;

namespace RedLine.Services.Catalog.Filters;

public class NotFoundFilter<CategoryDto> : IAsyncActionFilter 
{
    private readonly ICategoryService _categoryService;
    

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var idValue = context.ActionArguments.Values.FirstOrDefault();

        if (idValue is null)
        {
            await next.Invoke();
            return;
        }

        if (idValue is not string id)
        {
            context.Result = new BadRequestObjectResult(InternalApiResponseDto<CategoryDto>.Fail("Invalid Id format", 400));
            return;
        }

        var category = await _categoryService.GetByIdAsync(id);

        if (category != null)
        {
            await next.Invoke();
        }
        else
        {
            context.Result = new NotFoundObjectResult(InternalApiResponseDto<CategoryDto>.Fail("Category Not Found", 404));
            return;
        }

    }
}
