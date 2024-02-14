using Redline.Services.Basket.Models;
using RedLine.Shared.Dtos;

namespace Redline.Services.Basket.Services;

public interface IBasketService
{
    Task<InternalApiResponseDto<BasketDto>> GetBasket(string userId);
    Task<InternalApiResponseDto<bool>> SaveOrUpdate(BasketDto basketDto);
    Task<InternalApiResponseDto<bool>> Delete(string userId);
}
