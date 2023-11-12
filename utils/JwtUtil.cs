using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace YorozuyaServer.utils;

public class JwtUtil
{
    public static string GenerateJwtToken(ClaimsIdentity claimsIdentity)
    {
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("55668899"));
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "yorozuya",
            audience: "yorozuya_audience",
            claims: claimsIdentity.Claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yorozuya",
            ValidAudience = "yorozuya_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("55668899")),
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
    
    public static ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        TokenValidationParameters validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "yorozuya",
            ValidAudience = "yorozuya_audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("55668899")),
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
    
    public static bool ValidateToken(string token)
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