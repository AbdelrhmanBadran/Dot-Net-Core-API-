using AutoMapper;
using Core.Entities;
using Core.Entities.OrderEntities;
using Microsoft.Extensions.Configuration;
using Services.Services.ProductServices.Dto;
using StackExchange.Redis;

namespace Services.Services.OrderService.Dto
{
    public class OrderUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
       
        private readonly IConfiguration _configuration;

        public OrderUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.PictureUrl))
                return _configuration["BaseUrl"] + source.ItemOrdered.PictureUrl;

            return null;
        }
        
    }
}
