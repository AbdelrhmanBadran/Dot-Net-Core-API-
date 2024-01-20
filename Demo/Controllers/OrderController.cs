using Core.Entities;
using Demo.Controllers;
using Demo.HandleResponse;
using Microsoft.AspNetCore.Mvc;
using Services.Services.OrderService;
using Services.Services.OrderService.Dto;
using System.Security.Claims;
using System.Web.Mvc;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace ECommerce.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateOrderAsync(OrderDto orderDto)
        {
            var order = await _orderService.CreateOrderAsync(orderDto);
            
            if(order == null)
                return BadRequest(new ApiResponse(400 , "Error While Creating Your Order"));

            return Ok(order); 

        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForUsersAsync()
        {

            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetAllOrdersForUsersAsync(email);

            if (order is { Count: <= 0 })
                return Ok(new ApiResponse(200, "You don't have any orders yet"));


            return Ok(order);

        }
        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetOrderByIdAsync(int id)
        {

            var email = User?.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id , email);

            if (order == null)
                return Ok(new ApiResponse(200, $"This is no order With this Id = {id}" ));

            return Ok(order);


        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethodAsync()
            => Ok(await _orderService.GetDeliveryMethodAsync());


    }
}
