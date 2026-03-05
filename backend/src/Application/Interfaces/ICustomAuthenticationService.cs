using Application.Models;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<AuthenticationResponseDto> Autenticar(AuthenticationRequestDto request);
    }
}