using Core.Models;
using Core.Interfaces.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Jwt
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _options;

        // внедряем настройки конфигурации через "JwtOptions" в appsettings.json
        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options.Value;
        }

        // используем nuget пакет BCrypt.Net-Next для создания токенов
        public string GenerateToken(User user)
        {
            // содержимое токена, его полезная нагрузка
            Claim[] claims = [new("userId", user.Id.ToString()),
                              new("userName", user.UserName),
                              new("passwordHash", user.PasswordHash),
                              new("email", user.Email),
                              new("role", user.Role.ToString())];

            // алгоритм кодировки токена
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
                SecurityAlgorithms.HmacSha256);

            // создание токена
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpiteMinutes)
            );

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }
    }
}
