using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using ProyectoReciclaje.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic.Utilities
{
    public static class UserLogic
    {
        public static Login ValidUserSignIn(this Login login, DbContextBioRuta DbContext, IConfiguration config)
        {
            login.Message = "";
            login.Error = false;

            User user = DbContext.User.Include(x => x.Role).Where(x => x.Email == login.Email && x.Password == SecurityManager.Encrypt(login.Password)).FirstOrDefault();

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

        public static User CreateUser(this DbContextBioRuta DbContext, User User, bool client)
        {
            User.Message = "";
            User.Error = false;

            var QueryUser = DbContext.User.Where(x => x.Email.Trim().ToLower().Contains(User.Email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (QueryUser.Any())
            {
                User.Error = true;
                User.Message = Resources.UsuarioRepetido;
            }
            else
            {
                if (client)
                {
                    User.Role = DbContext.Role.FirstOrDefault(x => x.Id == int.Parse(Resources.ClientId));
                    User.RoleId = User.Role.Id;
                    User.State = true;
                }
                else
                {
                    User.RoleId = User.Role.Id;
                    User.Role = DbContext.Role.FirstOrDefault(x => x.Id == User.RoleId);
                }

                User.Password = SecurityManager.Encrypt(User.Password);

                EntityEntry<User> value = DbContext.User.Add(User);
                DbContext.SaveChanges();
                User = value.Entity;
            }

            return User;
        }

        public static User UpdateUser(this DbContextBioRuta DbContext, User User)
        {
            User.Message = "";
            User.Error = false;

            var QueryUser = DbContext.User.Where(x => x.Id != User.Id && x.Email.Trim().ToLower().Contains(User.Email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (QueryUser.Any())
            {
                User.Error = true;
                User.Message = Resources.UsuarioRepetido;
            }
            else
            {
                DbContext.Entry(User).State = EntityState.Modified;
                DbContext.SaveChanges();
                User = DbContext.Entry(User).Entity;
            }

            return User;
        }

        public static List<User> GetUsers(this DbContextBioRuta DbContext, string Search)
        {
            var QueryUser = DbContext.User.Include(x => x.Role);

            if (!string.IsNullOrEmpty(Search))
            {
                if ("activo".Contains(Search.ToLower()) || "inactivo".Contains(Search.ToLower()))
                {
                    return QueryUser.Where(x => x.Email.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Role.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.State == "activo".Contains(Search.ToLower()) ? true : !("inactivo".Contains(Search.ToLower()))).ToList();
                }
                else
                {
                    return QueryUser.Where(x => x.Email.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Role.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }

            }

            return QueryUser.ToList();
        }

        public static User GetUserById(this DbContextBioRuta DbContext, int Id)
        {
            return DbContext.User.Include(x => x.Role).FirstOrDefault(x => x.Id == Id);
        }

        public static User DeleteUser(this DbContextBioRuta DbContext, User User)
        {
            var QueryUser = DbContext.User.FirstOrDefault(x => x.Id == User.Id);
            QueryUser.State = false;
            DbContext.Entry(QueryUser).State = EntityState.Modified;
            DbContext.SaveChanges();

            return DbContext.Entry(QueryUser).Entity;
        }


    }
}