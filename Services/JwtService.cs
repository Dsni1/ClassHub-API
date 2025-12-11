using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClassHub.Models;
using System.Security.Cryptography;

namespace ClassHub.Services
{
    public class JwtService
    {
        private readonly string _key;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _accessMinutes;

        public JwtService(IConfiguration config)
        {
            // ENV VARIABLES
            _key = Environment.GetEnvironmentVariable("JWT_KEY");
            _issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
            _audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
            _accessMinutes = int.Parse(Environment.GetEnvironmentVariable("ACCESS_TOKEN_MINUTES") ?? "60");
        }

        public string GenerateToken(User user, bool rememberMe)
        {
            var expiresMinutes =
                rememberMe ? _accessMinutes * 24 : _accessMinutes;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiresMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(string ipAddress, string userAgent)
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(randomBytes);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(
                    int.Parse(Environment.GetEnvironmentVariable("REFRESH_TOKEN_DAYS") ?? "7")
                ),
                CreatedByIp = ipAddress,
                UserAgent = userAgent
            };
        }
    }
}
