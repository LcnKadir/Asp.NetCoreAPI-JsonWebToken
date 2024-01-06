using CoreLayer.DTOs;
using CoreLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Exceptions;

namespace AuthServer.API.Controllers
{
    
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationController : CustomBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateToken(LoginDto loginDto)
        {
       
            var result = await _authenticationService.CreatTokenAsync(loginDto);
            return ActionResultInstance(result); //Durum kodunu çağırırken if-else yazmaktan kaçınmak ve kod fazlalığından kurtulmak adına; CustomBaseController oluşturularak, tek bir method ile durum kodunun'un gelmesi sağlanıldı.//In order to avoid typing if-else when calling the status code and to get rid of code redundancy, CustomBaseController was created and the status code was received with a single method.
        }


        [HttpPost]
        public IActionResult CreateTokenByClient(ClientLoginDto clientloginDto)
        {
            var result = _authenticationService.CreatTokenByClient(clientloginDto);
            return ActionResultInstance(result);
        }


        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.RevokeRefreshTokenAsync(refreshTokenDto.Token);
            return ActionResultInstance(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateRefreshToken(RefreshTokenDto refreshTokenDto)
        {
            var result = await _authenticationService.CreatTokenByRefreshTokenAsync(refreshTokenDto.Token);
            return ActionResultInstance(result);
        }
    }
}
