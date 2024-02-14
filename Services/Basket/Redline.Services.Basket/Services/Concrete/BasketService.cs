using Redline.Services.Basket.Models;
using RedLine.Shared.Dtos;
using System.Text.Json;

namespace Redline.Services.Basket.Services;

public class BasketService : IBasketService
{
    private readonly RedisService _redisService;

    public BasketService(RedisService redisService)
    {
        _redisService = redisService;
    }

    public async Task<InternalApiResponseDto<bool>> Delete(string userId)
    {
        var status = await _redisService.GetDb().KeyDeleteAsync(userId);
        return status
            ? InternalApiResponseDto<bool>.Success(204)
            : InternalApiResponseDto<bool>.Fail("Basket not found", 404);
    }

    public async Task<InternalApiResponseDto<BasketDto>> GetBasket(string userId)
    {
        var existBasket = await _redisService.GetDb().StringGetAsync(userId);
        return string.IsNullOrEmpty(existBasket)
            ? InternalApiResponseDto<BasketDto>.Fail("Basket Not Found", 404)
            : InternalApiResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);

        // If you want an alternative code example, you can take this as an example. It does the same thing
        /*
        if (string.IsNullOrEmpty(existBasket))
        {
            return InternalApiResponseDto<BasketDto>.Fail("Basket Not Found",404);
        }
        return InternalApiResponseDto<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket),200);
        */
    }

    public async Task<InternalApiResponseDto<bool>> SaveOrUpdate(BasketDto basketDto)
    {
        var status = await _redisService.GetDb().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return status
            ? InternalApiResponseDto<bool>.Success(204)
            : InternalApiResponseDto<bool>.Fail("Basket could not update or save", 500);
    }
}
