using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaymentAPI.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _config;
    private readonly Dictionary<string, (string Password, string[] Roles)> _testUsers = new()
{
    { "admin", ("admin123", new[] { "Admin" }) },
    { "manager", ("manager123", new[] { "Manager" }) },
    { "user", ("user123", new[] { "User" }) }
};

    public bool ValidateTestUser(string username, string password)
    {
        return _testUsers.TryGetValue(username, out var userInfo) && userInfo.Password == password;
    }
    public TokenService(IConfiguration config)
    {
        _config = config;
    }

    public string GenerateToken(string username)
    {
        if (!_testUsers.TryGetValue(username, out var userInfo))
            throw new Exception("Invalid user");
       
        var claims = new List<Claim>
        {
        new Claim(ClaimTypes.Name, username)
        };

        // Add role claims
        foreach (var role in userInfo.Roles)
        {
        claims.Add(new Claim(ClaimTypes.Role, role));
        }

    
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
