using RedLine.Services.Catalog.Models;
using RedLine.Shared.Dtos;
using System.Linq.Expressions;

namespace RedLine.Services.Catalog.Services;

public interface IGunService
{
    Task<InternalApiResponseDto<List<GunDto>>> GetAllWithDeletedFalseAsync();
    Task<InternalApiResponseDto<List<GunDto>>> GetAllAsync();
    Task<InternalApiResponseDto<GunCreateDto>> CreateAsync(GunCreateDto gunCreateDto);
    Task<InternalApiResponseDto<GunDto>> GetByIdWithDeletedFalseAsync(string id);
    Task<InternalApiResponseDto<GunDto>> GetByIdAsync(string id);
    Task<InternalApiResponseDto<List<GunDto>>> GetAllByUserIdWithDeletedFalseAsync(string userId);
    Task<InternalApiResponseDto<NoContent>> UpdateAsync(GunUpdateDto gunUpdateDto);
    Task<InternalApiResponseDto<NoContent>> DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<GunDto, bool>> expression);
}
