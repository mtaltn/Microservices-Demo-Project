using RedLine.Services.Catalog.Models;
using RedLine.Shared.Dtos;
using System.Linq.Expressions;

namespace RedLine.Services.Catalog.Services;

public interface ICategoryService
{
    Task<InternalApiResponseDto<List<CategoryDto>>> GetAllWithDeletedFalseAsync();
    Task<InternalApiResponseDto<List<CategoryDto>>> GetAllAsync();
    Task<InternalApiResponseDto<CategoryCreateDto>> CreateAsync(CategoryCreateDto categoryCreateDto);
    Task<InternalApiResponseDto<CategoryDto>> GetByIdWithDeletedFalseAsync(string id);
    Task<InternalApiResponseDto<CategoryDto>> GetByIdAsync(string id);
    Task<InternalApiResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
    Task<InternalApiResponseDto<NoContent>> DeleteAsync(string id);
    Task<bool> AnyAsync(Expression<Func<CategoryDto, bool>> expression);
}
/*
 * If you want you can create a GenericBaseService for methods that are common 
 */