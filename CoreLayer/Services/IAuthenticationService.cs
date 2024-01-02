using CoreLayer.DTOs;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IAuthenticationService
    {
        Task<Response<TokenDto>> CreatTokenAsync(LoginDto loginDto);

        Task<Response<TokenDto>> CreatTokenByRefreshTokenAsync(string  refreshToken);

        Task<Response<NoDataDto>> RevokeRefreshTokenAsync (string refreshToken);

        Response<ClientTokenDto> CreatTokenByClient(ClientLoginDto clientLoginDto);

    }
}
