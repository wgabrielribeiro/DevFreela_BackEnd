using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly IConfiguration configuration;

    public AuthService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string ComputeHash(string password)
    {
        using(var hash = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashedBytes = hash.ComputeHash(passwordBytes);

            var builder = new StringBuilder();

            for (int i = 0; i < hashedBytes.Length; i++) 
            {
                builder.Append(hashedBytes[i].ToString("x2"));
            }
            
            return builder.ToString();
        }
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = this.configuration["Jwt:Issuer"];
        var audience = this.configuration["Jwt:Audience"];
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
        var duration = this.configuration["Jwt:DurationInMinutes"];

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("username", email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.UtcNow.AddMinutes(Convert.ToInt32(duration)), credentials);

        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
