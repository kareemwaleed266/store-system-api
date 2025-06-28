using Microsoft.AspNetCore.Mvc;
using Store.Service.Services.BasketService;
using Store.Service.Services.BasketService.Dtos;

namespace Store.API.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketByIdAsync(string id)
            => Ok(await _basketService.GetBasketAsync(id));

        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok(await _basketService.UpdateBasketAsync(basket));

        [HttpGet]
        public async Task<ActionResult> DeleteBasketAsync(string id)
            => Ok(await _basketService.DeleteBasketAsync(id));
    }
}
