using Infrastructure.BasketRepository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.BasketRepository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(String BasketId);
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket Basket);
        Task<bool> DeleteBasketAsync(String BasketId);
    }
}
