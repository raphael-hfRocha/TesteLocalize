using CadastroEmpresas.src.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Security.JWT
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Generate(Usuario usuario)
        {
            var handler = new JwtSecurityTokenHandler();

            // Use a mesma secret do appsettings.json
            var secret = _configuration["JWT:Secret"];
            var key = Encoding.UTF8.GetBytes(secret);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"]
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}