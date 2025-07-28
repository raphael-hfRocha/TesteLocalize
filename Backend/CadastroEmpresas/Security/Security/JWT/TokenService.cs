using CadastroEmpresas.src.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            var handler = new JwtSecurityTokenHandler();

            // Use a mesma secret do appsettings.json
            var secret = _configuration["JWT:Secret"];
            if (string.IsNullOrEmpty(secret))
                throw new InvalidOperationException("JWT:Secret não configurado");

            var key = Encoding.UTF8.GetBytes(secret);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            // Criar claims para identificar o usuário
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Email), // Isso permite User.Identity.Name funcionar
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString()),
                new Claim("userId", usuario.IdUsuario.ToString()),
                new Claim("userName", usuario.Nome)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // Adicionar o Subject com claims
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