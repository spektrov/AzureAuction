using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Auction.Business.Helpers;

public class TokenHelper
{
    public static string Issuer = null!;
    public static string Audience = null!;
    public static string Secret = null!;

    public TokenHelper(string issuer, string audience, string secret)
    {
        Issuer = issuer;
        Audience = audience;
        Secret = secret;
    }

    public static async Task<string> GenerateAccessToken(Guid userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Convert.FromBase64String(Secret);

        var claimsIdentity = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
        });

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = claimsIdentity,
            Issuer = Issuer,
            Audience = Audience,
            Expires = DateTime.Now.AddMinutes(3),
            SigningCredentials = signingCredentials,

        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);

        return await Task.Run(() => tokenHandler.WriteToken(securityToken));
    }
    public static async Task<string> GenerateRefreshToken()
    {
        var secureRandomBytes = new byte[32];

        using var randomNumberGenerator = RandomNumberGenerator.Create();
        await Task.Run(() => randomNumberGenerator.GetBytes(secureRandomBytes));

        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }
}