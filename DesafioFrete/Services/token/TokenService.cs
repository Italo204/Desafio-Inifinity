using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DesafioFrete.Models;
using Microsoft.IdentityModel.Tokens;

namespace DesafioFrete.Services.token
{
    public class TokenService
    {
        public string CreateAccessToken(
            JwtOptions jwtOptions,
            string name,
            TimeSpan expiration,
            string[] permissions
        )
        {
            var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var claims = new List<Claim>()
            {
                new Claim("sub", name),
                new Claim("sub", name),
                new Claim("aud", jwtOptions.Audience)
            };

            var roleClaims = permissions.Select(x => new Claim("role", x));
            claims.AddRange(roleClaims);

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials
            );

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);

            return rawToken;
        }

        public string CreateRefreshToken(JwtOptions jwtOptions, string name, TimeSpan expiration)
        {
            var keyBytes = Encoding.UTF8.GetBytes(jwtOptions.SigningKey);
            var symmetricKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var claims = new List<Claim>()
            {
                new Claim("sub", name),
                new Claim("type", "refresh")
            };

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                claims: claims,
                expires: DateTime.Now.Add(expiration),
                signingCredentials: signingCredentials
            );

            var rawToken = new JwtSecurityTokenHandler().WriteToken(token);

            return rawToken;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, JwtOptions jwtOptions)
        {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, // Não validar a expiração do token
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey)),
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out var securityToken);
            return principal;
        }
        catch
        {
            // Se a validação falhar, retorne null ou lance uma exceção conforme necessário
            return null;
        }
    }
    }
}