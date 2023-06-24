using BusinessLogic.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProyectoReciclaje.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogic.Utilities
{
    public static class UserValidator
    {
        public static Login ValidUserSignIn(this Login login, DbContextBioRuta DbContext, IConfiguration config) {

            User user = DbContext.User.Include(x=>x.Role).Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefault();
            DbContext.Database.CloseConnection();

            if (user != null)
            {
                var token = new JwtSecurityTokenHandler().WriteToken(Token.GetToken(user, config));
                login.Token = token;
            }
            else
            {
                login.Error = true;
                login.Message = Resources.UsuarioNoEncontrado;
            }

            return login;
        }

        public static EntityEntry<User> CreateClient(this DbContextBioRuta DbContext, User User)
        {
            //DbContext.Database.OpenConnection();
            User.Role = DbContext.Role.FirstOrDefault(x => x.Id == int.Parse(Resources.ClientId));
            User.RoleId = User.Role.Id;
            EntityEntry<User> value = DbContext.User.Add(User);
            DbContext.SaveChanges();
            DbContext.Database.CloseConnection();
            return value;
        }

        
    }
}