using Application.Interfaces;
using Application.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services
{
    public class AuthenticationService : ICustomAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationServiceOptions _options;

        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private async Task<Domain.Entities.User?> ValidateUser(AuthenticationRequestDto request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                return null;

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                return null;

            if (!user.Status)
                throw new UnauthorizedAccessException("Usuario Inhabilitado");

            return user;
        }

        public async Task<AuthenticationResponseDto> Autenticar(AuthenticationRequestDto request)
        {
            var user = await ValidateUser(request);
            if (user == null)
                throw new Exception("Email o contraseña incorrectos");

            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
    {
        new Claim("sub", user.Id.ToString()),
        new Claim("mail", user.Email),
        new Claim("role", user.Type)
    };

            var token = new JwtSecurityToken(
                _options.Issuer,
                _options.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthenticationResponseDto
            {
                Token = tokenToReturn,
                Type = user.Type
            };
        }

        public class AuthenticationServiceOptions
        {
            public const string AuthenticationService = "AuthenticationService";
            public string Issuer { get; set; } = string.Empty;
            public string Audience { get; set; } = string.Empty;
            public string SecretForKey { get; set; } = string.Empty;
        }
    }
}