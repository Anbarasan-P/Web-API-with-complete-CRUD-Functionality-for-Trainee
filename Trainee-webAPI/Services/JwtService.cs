<<<<<<< HEAD
﻿using System.IdentityModel.Tokens.Jwt;
=======
﻿//using System;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using Microsoft.IdentityModel.Tokens;

//public class JwtService
//{
//    private readonly string _secret;
//    private readonly string _expDate;

//    public JwtService(IConfiguration config)
//    {
//        _secret = config["Jwt:Key"];
//        _expDate = config["Jwt:ExpireDays"];
//    }

//    // ✅ Accept traineeId also
//    public string GenerateToken(int traineeId, string email)
//    {
//        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
//        var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

//        var claims = new[]
//        {
//            new Claim(ClaimTypes.NameIdentifier, traineeId.ToString()),  // 🧠 Store ID
//            new Claim(ClaimTypes.Email, email)
//        };

//        double expireDays = double.TryParse(_expDate, out var days) ? days : 1;

//        var token = new JwtSecurityToken(
//            claims: claims,
//            expires: DateTime.Now.AddDays(expireDays),
//            signingCredentials: credentials);

//        return new JwtSecurityTokenHandler().WriteToken(token);
//    }
//}

using System;
using System.IdentityModel.Tokens.Jwt;
>>>>>>> d0337bb040fe99a6b1a0cd508fd9857820358a6f
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

        double expireDays = double.TryParse(_expDate, out var days) ? days : 1;

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(expireDays),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
