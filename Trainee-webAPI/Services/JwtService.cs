using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class JwtService
{
    private readonly string _secret;
    private readonly string _expDate;

    public JwtService(IConfiguration config)
    {
        _secret = config["Jwt:Key"];
        _expDate = config["Jwt:ExpireDays"];
    }

    public string GenerateToken(string email)
    {
        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Email, email)
        };

        // Safer parsing — fallback to 1 day if value is null or invalid
        double expireDays = double.TryParse(_expDate, out var days) ? days : 1;

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(expireDays),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
