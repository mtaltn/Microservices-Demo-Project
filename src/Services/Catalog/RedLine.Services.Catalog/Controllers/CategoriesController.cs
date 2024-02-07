using Microsoft.AspNetCore.Mvc;
using RedLine.Services.Catalog.Filters;
using RedLine.Services.Catalog.Models;
using RedLine.Services.Catalog.Services;
using RedLine.Shared.ControllerBases;

namespace RedLine.Services.Catalog.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : CustomBaseController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _categoryService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllWithDeletedFalseAsync()
    {
        var response = await _categoryService.GetAllWithDeletedFalseAsync();
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
    {
        var response = await _categoryService.CreateAsync(categoryCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
    {
        var response = await _categoryService.UpdateAsync(categoryUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _categoryService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var response = await _categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetByIdWithDeletedFalseAsync(string id)
    {
        var response = await _categoryService.GetByIdWithDeletedFalseAsync(id);
        return CreateActionResultInstance(response);
    }
}
