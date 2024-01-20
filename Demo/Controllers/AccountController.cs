using Core.IdentityEntities;
using Demo.Controllers;
using Demo.HandleResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Services.UserService;
using Services.Services.UserService.Dto;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(IUserService userService , UserManager<AppUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
            {
            var user = await _userService.Register(registerDto);

            if (user == null)
                return BadRequest(new ApiException(400, "Email Already Exists"));


            return Ok(user);

        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userService.Login(loginDto);

            if (user == null)
                return Unauthorized(new ApiResponse(401));


            return Ok(user);

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return new UserDto
            {
                DisplayName = user.DisplayName,
                Email = user.Email 
            };

        }



    }
}
