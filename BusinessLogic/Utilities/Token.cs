using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProyectoReciclaje.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Utilities
{
    public static class Token
    {
        public static JwtSecurityToken GetToken(User user, IConfiguration config)
        {
            string ValidIssuer = config["ApiAuth:Issuer"];
            string ValidAudience = config["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["ApiAuth:SecretKey"]));

            //Agregamos los claim nuestros
            var claims = new[]
            {
                new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new {user.Name, user.Email, Role = new { user.Role.Name } }))
            };

            return new JwtSecurityToken
            (
                issuer: ValidIssuer,
                audience: ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(1) ,
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
