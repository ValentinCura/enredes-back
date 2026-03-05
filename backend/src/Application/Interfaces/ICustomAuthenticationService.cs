using Application.Models;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        AuthenticationResponseDto Autenticar(AuthenticationRequestDto request);
    }
}