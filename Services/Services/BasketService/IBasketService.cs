using Infrastructure.BasketRepository.Entities;
using Services.Services.BasketService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BasketService
{
    public interface IBasketService
    {
        Task<CustomerBasketDto> GetBasketAsync(String BasketId);
        Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto Basket);
        Task<bool> DeleteBasketAsync(String BasketId);
    }
}
