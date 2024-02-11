using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HousingBillManagement.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HousingBillManagement.API.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public TokenService(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<string> GenerateToken(string? username, string? tcNo, string? phoneNumber, string password)
        {
            if (string.IsNullOrEmpty(username) && string.IsNullOrEmpty(tcNo) && string.IsNullOrEmpty(phoneNumber))
            {
                throw new ArgumentException("At least one of the inputs must be provided.");
            }

            var user = username != null
                ? await _userManager.FindByNameAsync(username)
                : tcNo != null
                    ? await _userManager.FindByLoginAsync("TCNo", tcNo)
                    : phoneNumber != null
                        ? await _userManager.FindByLoginAsync("PhoneNumber", phoneNumber)
                        : null;

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            {
                throw new Exception("Invalid");
            }

            var jwtSettings = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Key"));
            var issuer = jwtSettings.GetValue<string>("Issuer");
            var audience = jwtSettings.GetValue<string>("Audience");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("TCNo", user.TCNo),
            new Claim("PhoneNumber", user.PhoneNumber),

        };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var now = DateTime.UtcNow;
            var expires = now.AddDays(7); //check the timing for token

            var signingKey = new SymmetricSecurityKey(key);
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;

        }
    }
}
