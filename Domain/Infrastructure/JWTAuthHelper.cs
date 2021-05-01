  
using System;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace Domain.Infrastructure
{
    public interface IJWTAuthHelper
    {
         OutboundDTO GenerateTokens(string username, Claim[] claims, DateTime now);
         OutboundDTO RefreshToken(string refreshToken, string accessToken, DateTime now);
         (ClaimsPrincipal, JwtSecurityToken) DecodeToken(string token);
    }

    public class JWTAuthHelper 
    {
        private readonly JwtConfig _jwtConfig;
        private readonly byte[] _secret;

        public JWTAuthHelper(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
            _secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
        }

        OutboundDTO GenerateTokens(string username, Claim[] claims, DateTime now)
        {
            var canAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

            var jwtToken = new JwtSecurityToken(
                _jwtConfig.Issuer,
                canAddAudienceClaim ? _jwtConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(_jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret), SecurityAlgorithms.HmacSha256Signature)
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            var outboundItemData = new OutboundDTO();

            outboundItemData.AddField(new DictionaryEntry { Key = "UserName", Value = username });
            outboundItemData.AddField(new DictionaryEntry {Key = "Token", Value = GenerateRefreshTokenString()});
            outboundItemData.AddField(new DictionaryEntry {Key = "ExpireAt", Value = now.AddMinutes(_jwtConfig.RefreshTokenExpiration)});

            var outboundItemData2 = new OutboundDTO();

            outboundItemData.AddField(new DictionaryEntry { Key = "AccessToken", Value = accessToken });
            outboundItemData.AddField(new DictionaryEntry {Key = "RefreshToken", Value = outboundItemData});

            return outboundItemData;
        }

        public OutboundDTO RefreshToken(string refreshToken, string accessToken, DateTime now)
        {
            var (principal, jwtToken) = DecodeJwyToken(accessToken);

            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Bad Token");
            }

            //Create rows in database for refresh tokens for more
            //security features at this point.

            return GenerateTokens(userName, principal.Claims.ToArray(), now);
        }

        public (ClaimsPrincipal, JwtSecurityToken) DecodeToken(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
            {
                throw new SecurityTokenException("Bad Token");
            }

            var principal = new JwtSecurityTokenHandler()
            .ValidateToken(token, new TokenValidationParameters{
                ValidateIssuer = true,
                ValidIssure = _jwtConfig.Issuer,
                ValidateSignatureSigningKey = true,
                IssureSigningKey = new SymmetricSecurityKey(_secret),
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(1)
            },
                out var validatedToken
            );

            return (principal, validatedToken as JwtSecurityToken);
        }

        private static string GenerateRefreshTokenString()
        {
            var refreshNumber = new byte[32];

            using var randomNumberGernerator = RandomNumberGenerator.Create();

            randomNumberGernerator.GetBytes(refreshNumber);

            return Convert.ToBase64String(refreshNumber);
        }


    }
}