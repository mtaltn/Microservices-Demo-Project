using Microsoft.AspNetCore.Mvc;
using RedLine.Services.Catalog.Filters;
using RedLine.Services.Catalog.Models;
using RedLine.Services.Catalog.Services;
using RedLine.Shared.ControllerBases;

namespace RedLine.Services.Catalog.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class GunsController : CustomBaseController
{
    private readonly IGunService _gunService;

    public GunsController(IGunService gunService)
    {
        _gunService = gunService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllWithDeletedFalseAsync()
    {
        var response = await _gunService.GetAllWithDeletedFalseAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _gunService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]/{id}")]
    public async Task<IActionResult> GetByIdWithDeletedFalseAsync(string id)
    {
        var response = await _gunService.GetByIdWithDeletedFalseAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(string id)
    {
        var response = await _gunService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet("[action]/{userId}")]
    public async Task<IActionResult> GetAllByUserIdAsync(string userId)
    {
        var response = await _gunService.GetAllByUserIdWithDeletedFalseAsync(userId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(GunCreateDto gunCreateDto)
    {
        var response = await _gunService.CreateAsync(gunCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(GunUpdateDto gunUpdateDto)
    {
        var response = await _gunService.UpdateAsync(gunUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await _gunService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
