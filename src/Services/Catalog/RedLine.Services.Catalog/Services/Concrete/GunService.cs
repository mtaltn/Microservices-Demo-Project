using AutoMapper;
using MongoDB.Driver;
using RedLine.Services.Catalog.Models;
using RedLine.Services.Catalog.Settings;
using RedLine.Shared.Dtos;
using System.Linq.Expressions;

namespace RedLine.Services.Catalog.Services;

public class GunService : IGunService
{
    private readonly IMongoCollection<GunModels> _gunCollection;
    private readonly IMongoCollection<CategoryModels> _categoryCollection;
    private readonly IMapper _mapper;

    public GunService(IDatabaseSettings databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _gunCollection = database.GetCollection<GunModels>(databaseSettings.GunCollectionName);
        _categoryCollection = database.GetCollection<CategoryModels>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<InternalApiResponseDto<List<GunDto>>> GetAllWithDeletedFalseAsync()
    {
        var guns = await _gunCollection
            .Find(gun => gun.isDeleted == false)
            .ToListAsync();
        if (guns.Any())
        {
            foreach (var gun in guns)
            {
                gun.CategoryModels = await _categoryCollection.Find<CategoryModels>(x => x.Id == gun.CategotyId && x.isDeleted == false).FirstAsync();
            }
        }
        else
        {
            guns = new List<GunModels>();
        }

        return InternalApiResponseDto<List<GunDto>>
            .Success(_mapper.Map<List<GunDto>>(guns), 200);
    }

    public async Task<InternalApiResponseDto<List<GunDto>>> GetAllAsync()
    {
        var guns = await _gunCollection
            .Find(gun => true)
            .ToListAsync();
        if (guns.Any())
        {
            foreach (var gun in guns)
            {
                gun.CategoryModels = await _categoryCollection.Find<CategoryModels>(x => x.Id == gun.CategotyId).FirstAsync();
            }
        }
        else
        {
            guns = new List<GunModels>();
        }

        return InternalApiResponseDto<List<GunDto>>
            .Success(_mapper.Map<List<GunDto>>(guns), 200);
    }

    public async Task<InternalApiResponseDto<GunCreateDto>> CreateAsync(GunCreateDto gunCreateDto)
    {
        var newGun = _mapper.Map<GunModels>(gunCreateDto);

        await _gunCollection.InsertOneAsync(newGun);

        return InternalApiResponseDto<GunCreateDto>
            .Success(_mapper.Map<GunCreateDto>(newGun), 200);
    }

    public async Task<InternalApiResponseDto<GunDto>> GetByIdWithDeletedFalseAsync(string id)
    {
        var gun = await _gunCollection
            .Find(x => x.Id == id && x.isDeleted == false)
            .FirstOrDefaultAsync();

        if (gun is null)
            return InternalApiResponseDto<GunDto>
                .Fail("Gun Not Found", 404);
        gun.CategoryModels = await _categoryCollection.Find<CategoryModels>(x => x.Id == gun.CategotyId && x.isDeleted == false).FirstAsync();

        return InternalApiResponseDto<GunDto>
            .Success(_mapper.Map<GunDto>(gun), 200);
    }

    public async Task<InternalApiResponseDto<GunDto>> GetByIdAsync(string id)
    {
        var gun = await _gunCollection
            .Find(x => x.Id == id)
            .FirstOrDefaultAsync();
        if (gun is null)
            return InternalApiResponseDto<GunDto>
                .Fail("Gun Not Found", 404);
        gun.CategoryModels = await _categoryCollection.Find<CategoryModels>(x => x.Id == gun.CategotyId).FirstAsync();

        return InternalApiResponseDto<GunDto>
            .Success(_mapper.Map<GunDto>(gun), 200);
    }

    public async Task<InternalApiResponseDto<List<GunDto>>> GetAllByUserIdWithDeletedFalseAsync(string userId)
    {
        var guns = await _gunCollection
            .Find<GunModels>(x => x.UserId == userId && x.isDeleted == false)
            .ToListAsync();
        if (guns.Any())
        {
            foreach (var gun in guns)
            {
                gun.CategoryModels = await _categoryCollection.Find<CategoryModels>(x => x.Id == gun.CategotyId).FirstAsync();
            }
        }
        else
        {
            guns = new List<GunModels>();
        }

        return InternalApiResponseDto<List<GunDto>>
            .Success(_mapper.Map<List<GunDto>>(guns), 200);
    }

    public async Task<InternalApiResponseDto<NoContent>> UpdateAsync(GunUpdateDto gunUpdateDto)
    {
        var updateGun = _mapper.Map<GunModels>(gunUpdateDto);
        var result = await _gunCollection.FindOneAndReplaceAsync(x => x.Id == updateGun.Id, updateGun);

        if (result is null)
            return InternalApiResponseDto<NoContent>.Fail("Gun not found", 404);

        return InternalApiResponseDto<NoContent>.Success(204);
    }

    public async Task<InternalApiResponseDto<NoContent>> DeleteAsync(string id)
    {
        var deleteGun = await _gunCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (deleteGun is null)
            return InternalApiResponseDto<NoContent>.Fail("Gun not found", 404);

        var updateDefinition = Builders<GunModels>.Update.Set(x => x.isDeleted, true);
        var result = await _gunCollection.UpdateOneAsync(x => x.Id == id, updateDefinition);

        if (result.ModifiedCount is 0)
            return InternalApiResponseDto<NoContent>.Fail("Failed to update Gun", 500);

        return InternalApiResponseDto<NoContent>.Success(204);
    }

    public async Task<bool> AnyAsync(Expression<Func<GunDto, bool>> expression)
    {
        var gunsFromDb = await GetAllAsync();

        return gunsFromDb.Payload.Any(expression.Compile());
    }
}
