using Application.Models;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        string Autenticar(AuthenticationRequestDto authenticationRequest);
    }
}