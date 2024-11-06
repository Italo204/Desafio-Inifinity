using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataContext;
using DesafioFrete.Models;
using DesafioFrete.Services.token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioFrete.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class TokenController : ControllerBase
    {
        private readonly JwtOptions _jwtOptions;
        private readonly FreteDatabaseContext _context;

        private readonly TokenService TokenService;

        public TokenController(JwtOptions jwt, FreteDatabaseContext context)
        {
            _context = context;
            _jwtOptions = jwt;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] string email, string senha)
        {
            // Verifica se o usuário existe no banco de dados
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email.Equals(email));

            if (usuario == null || !senha.Equals(usuario.Senha)) // Verifica a senha
            {
                return Unauthorized();
            }

            var permissions = new[] { "User " }; // Exemplo de permissões
            var accessToken = TokenService.CreateAccessToken(_jwtOptions, usuario.Nome, TimeSpan.FromMinutes(30), permissions);
            var refreshToken = TokenService.CreateRefreshToken(_jwtOptions, usuario.Nome, TimeSpan.FromHours(7));

            // Aqui você pode salvar o refresh token no banco de dados associado ao usuário

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenModel model)
        {
            // Aqui você deve verificar se o refresh token é válido (ex: no banco de dados)
            // Para simplificação, vamos assumir que o refresh token é válido
            var principal = TokenService.GetPrincipalFromExpiredToken(model.AccessToken, _jwtOptions);
            var username = principal.FindFirstValue("sub");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Nome == username);

            if (usuario == null)
            {
                return Unauthorized();
            }

            var permissions = new[] { "User " }; // Exemplo de permissões
            var newAccessToken = TokenService.CreateAccessToken(_jwtOptions, username, TimeSpan.FromMinutes(30), permissions);
            var newRefreshToken = TokenService.CreateRefreshToken(_jwtOptions, username, TimeSpan.FromHours(7)); // Exemplo: 7 dias de validade

            // Aqui você pode atualizar o refresh token no banco de dados

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }
    }
}