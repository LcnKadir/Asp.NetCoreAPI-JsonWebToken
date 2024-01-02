using CoreLayer.DTOs;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreatUserDto creatUserDto)
        {
            return ActionResultInstance(await _userService.CreatUserAsync(creatUserDto));
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            return ActionResultInstance(await _userService.GetUserByNameAsync(HttpContext.User.Identity.Name));
        }
    }
}
