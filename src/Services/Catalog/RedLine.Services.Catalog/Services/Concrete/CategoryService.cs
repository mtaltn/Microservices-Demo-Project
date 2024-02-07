using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using RedLine.Services.Catalog.Filters;
using RedLine.Services.Catalog.Models;
using RedLine.Services.Catalog.Settings;
using RedLine.Shared.Dtos;
using System.Linq.Expressions;
using System.Text.Json;

namespace RedLine.Services.Catalog.Services;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<CategoryModels> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<CategoryModels>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<InternalApiResponseDto<List<CategoryDto>>> GetAllWithDeletedFalseAsync()
    {
        var categories = await _categoryCollection
            .Find(category => category.isDeleted == false)
            .ToListAsync();

        return InternalApiResponseDto<List<CategoryDto>>
            .Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    public async Task<InternalApiResponseDto<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection
            .Find(category => true)
            .ToListAsync();

        return InternalApiResponseDto<List<CategoryDto>>
            .Success(_mapper.Map<List<CategoryDto>>(categories), 200);
    }

    public async Task<InternalApiResponseDto<CategoryCreateDto>> CreateAsync(CategoryCreateDto categoryCreateDto)
    {
        var newCategory = _mapper.Map<CategoryModels>(categoryCreateDto);
        await _categoryCollection.InsertOneAsync(newCategory);

        return InternalApiResponseDto<CategoryCreateDto>
            .Success(_mapper.Map<CategoryCreateDto>(newCategory), 200);
    }

    public async Task<InternalApiResponseDto<CategoryDto>> GetByIdWithDeletedFalseAsync(string id)
    {
        var category = await _categoryCollection
            .Find(x => x.Id == id && x.isDeleted == false)
            .FirstOrDefaultAsync();

        if (category is null)
            return InternalApiResponseDto<CategoryDto>
                .Fail("Category Not Found", 404);

        return InternalApiResponseDto<CategoryDto>
            .Success(_mapper.Map<CategoryDto>(category), 200);
    }

    public async Task<InternalApiResponseDto<CategoryDto>> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out ObjectId objectId))
        {
            return InternalApiResponseDto<CategoryDto>
                .Fail("Incorrect data entry : CategoryDb", 400);
        }

        var category = await _categoryCollection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (category is null)
            return InternalApiResponseDto<CategoryDto>
                .Fail("Category Not Found", 404);

        return InternalApiResponseDto<CategoryDto>
            .Success(_mapper.Map<CategoryDto>(category), 200);
    }

    public async Task<InternalApiResponseDto<NoContent>> UpdateAsync(CategoryUpdateDto categoryUpdateDto)
    {
        var updateCategory = _mapper.Map<CategoryModels>(categoryUpdateDto);
        var result = await _categoryCollection.FindOneAndReplaceAsync(x => x.Id == updateCategory.Id, updateCategory);

        if (result is null)
            return InternalApiResponseDto<NoContent>.Fail("Gun not found", 404);

        return InternalApiResponseDto<NoContent>.Success(204);
    }

    public async Task<InternalApiResponseDto<NoContent>> DeleteAsync(string id)
    {
        var deleteCategory = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (deleteCategory is null)
            return InternalApiResponseDto<NoContent>.Fail("Category not found", 404);

        var updateDefinition = Builders<CategoryModels>.Update.Set(x => x.isDeleted, true);
        var result = await _categoryCollection.UpdateOneAsync(x => x.Id == id, updateDefinition);

        if (result.ModifiedCount is 0)
            return InternalApiResponseDto<NoContent>.Fail("Failed to update Gun", 500);

        return InternalApiResponseDto<NoContent>.Success(204);
    }

    public async Task<bool> AnyAsync(Expression<Func<CategoryDto, bool>> expression)
    {
        var categoriesFromDb = await GetAllAsync(); // Tüm kategorileri getir

        // Belirli bir koşula uyan bir kayıt var mı kontrol et
        return categoriesFromDb.Payload.Any(expression.Compile());
    }
}
