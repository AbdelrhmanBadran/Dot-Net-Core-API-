using AutoMapper;
using Infrastructure.BasketRepository;
using Infrastructure.BasketRepository.Entities;
using Services.Services.BasketService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.BasketService
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
         => await basketRepository.DeleteBasketAsync(BasketId);

        public async Task<CustomerBasketDto> GetBasketAsync(string BasketId)
        {
            var basket = await basketRepository.GetBasketAsync(BasketId);

            var mappedBasket = mapper.Map<CustomerBasketDto>(basket);

            return mappedBasket ?? new CustomerBasketDto();
        }

        public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto Basket)
        {
            var customerBasket = mapper.Map<CustomerBasket>(Basket);

            var updatedBasket = await basketRepository.UpdateBasketAsync(customerBasket);

            var mappedCustomerBasket = mapper.Map<CustomerBasketDto>(updatedBasket);

            return mappedCustomerBasket;
        }
    }
}
