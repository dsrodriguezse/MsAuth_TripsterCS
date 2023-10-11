using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MsAuth_Tripster.Models;
using MsAuth_Tripster.Models.Custom;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace MsAuth_Tripster.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DbpruebaContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(DbpruebaContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GenerarToken(string idUsuario)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreado = tokenHandler.WriteToken(tokenConfig);

            return tokenCreado;
        }

        public async Task<AuthorizationResponse> DevolverToken(AuthorizationRequest authorization)
        {
            var usuario_encontrado = _context.Usuarios.FirstOrDefault(x =>
                x.NombreUsuario == authorization.NombreUsuario &&
                x.Clave == authorization.Clave
            );

            if(usuario_encontrado == null)
            {
                return await Task.FromResult<AuthorizationResponse>(null);
            }

            string tokenCreado = GenerarToken(usuario_encontrado.IdUsuario.ToString());

            return new AuthorizationResponse()
            {
                Token = tokenCreado,
                Resultado = true,
                Msg = "Ok"
            };
        }
    }
}
