using Microsoft.AspNetCore.Mvc;
using Redline.Services.Basket.Models;
using Redline.Services.Basket.Services;
using RedLine.Shared.ControllerBases;
using RedLine.Shared.Services;

namespace Redline.Services.Basket.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _identityService;

        public BasketController(IBasketService basketService, ISharedIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => CreateActionResultInstance(await _basketService.GetBasket(_identityService.GetUserId)); 
        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto) => CreateActionResultInstance(await _basketService.SaveOrUpdate(basketDto));
        [HttpDelete]
        public async Task<IActionResult> Delete() => CreateActionResultInstance(await _basketService.Delete(_identityService.GetUserId));
    }
}
