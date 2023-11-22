using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace YorozuyaServer.utils;

public class JwtUtil
{
    public string GenerateJwtToken(long Id)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Sid, Id.ToString()),
        });
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7JF5L1WS8b9S7Hd0De6h2djrV"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "yorozuya",
            audience: "yorozuya_audience",
            claims: claimsIdentity.Claims,
            expires: DateTime.UtcNow.AddDays(7),
            notBefore: DateTime.UtcNow.AddMinutes(-5),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yorozuya",
            ValidAudience = "yorozuya_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7JF5L1WS8b9S7Hd0De6h2djrV")),
            LifetimeValidator = (before, expires, token, param) =>
            {
                // 检查令牌的过期时间是否合法
                return expires > DateTime.UtcNow;
            }
        };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return principal;
    }
    
    public ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yorozuya",
            ValidAudience = "yorozuya_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC7JF5L1WS8b9S7Hd0De6h2djrV")),
            LifetimeValidator = (before, expires, token, param) =>
            {
                // 检查令牌的过期时间是否合法
                return expires > DateTime.UtcNow;
            }
        };
        
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        ClaimsPrincipal? principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
        JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return null;
        }

        return principal;
    }
    
    public bool ValidateToken(string token)
    {
        ClaimsPrincipal? principal = GetPrincipalFromToken(token);
        if (principal == null)
        {
            return false;
        }

        ClaimsIdentity identity = principal.Identity as ClaimsIdentity;
        if (identity == null)
        {
            return false;
        }

        if (!identity.IsAuthenticated)
        {
            return false;
        }

        return true;
    }
}