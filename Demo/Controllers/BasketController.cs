using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services.BasketService;
using Services.Services.BasketService.Dto;

namespace Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : BaseController
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }
        [HttpGet]
        public async Task<ActionResult<CustomerBasketDto>> GetBasketById(string id)
            => Ok(await basketService.GetBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<CustomerBasketDto>> UpdateBasketAsync(CustomerBasketDto basket)
            => Ok(await basketService.UpdateBasketAsync(basket));
        [HttpDelete]
        public async Task<ActionResult<CustomerBasketDto>> DeleteBasketById(string id)
            => Ok(await basketService.DeleteBasketAsync(id));


            
       
    }
}
