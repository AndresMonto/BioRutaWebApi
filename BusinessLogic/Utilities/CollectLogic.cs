using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using ProyectoReciclaje.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic.Utilities
{
    public static class CollectLogic
    {
        

        //public static User CreateUser(this DbContextBioRuta DbContext, User User, bool client)
        //{
        //    User.Message = "";
        //    User.Error = false;

        //    var QueryUser = DbContext.User.Where(x => x.Email.Trim().ToLower().Contains(User.Email, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (QueryUser.Any())
        //    {
        //        User.Error = true;
        //        User.Message = Resources.UsuarioRepetido;
        //    }
        //    else
        //    {
        //        if (client)
        //        {
        //            User.Role = DbContext.Role.FirstOrDefault(x => x.Id == int.Parse(Resources.ClientId));
        //            User.RoleId = User.Role.Id;
        //            User.State = true;
        //        }
        //        else
        //        {
        //            User.RoleId = User.Role.Id;
        //            User.Role = DbContext.Role.FirstOrDefault(x => x.Id == User.RoleId);
        //        }

        //        User.Password = SecurityManager.Encrypt(User.Password);

        //        EntityEntry<User> value = DbContext.User.Add(User);
        //        DbContext.SaveChanges();
        //        User = value.Entity;
        //    }

        //    return User;
        //}

        //public static User UpdateUser(this DbContextBioRuta DbContext, User User)
        //{
        //    User.Message = "";
        //    User.Error = false;

        //    var QueryUser = DbContext.User.Where(x => x.Id != User.Id && x.Email.Trim().ToLower().Contains(User.Email, StringComparison.OrdinalIgnoreCase)).ToList();

        //    if (QueryUser.Any())
        //    {
        //        User.Error = true;
        //        User.Message = Resources.UsuarioRepetido;
        //    }
        //    else
        //    {
        //        DbContext.Entry(User).State = EntityState.Modified;
        //        DbContext.SaveChanges();
        //        User = DbContext.Entry(User).Entity;
        //    }

        //    return User;
        //}

        public static List<Collect> GetCollects(this DbContextBioRuta DbContext, string Search)
        {
            var QueryUser = DbContext.Collect.Include(x => x.Client).Include(x => x.State);

            if (!string.IsNullOrEmpty(Search))
            {
                return QueryUser.Where(x => x.Client.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.State.Name.ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) ).ToList();
            }

            return QueryUser.ToList();
        }

        public static List<Collect> GetCollects(this DbContextBioRuta DbContext, CollectSearch Search)
        {
            var QueryUser = DbContext.Collect.Include(x => x.Client).Include(x => x.State);
            return QueryUser.Where(x => x.RegistrationDate.Date >= Search.DateIni.Date && x.RegistrationDate.Date <= Search.DateFin.Date).ToList();
        }

        //public static User GetUserById(this DbContextBioRuta DbContext, int Id)
        //{
        //    return DbContext.User.Include(x => x.Role).FirstOrDefault(x => x.Id == Id);
        //}

        //public static User DeleteUser(this DbContextBioRuta DbContext, User User)
        //{
        //    var QueryUser = DbContext.User.FirstOrDefault(x => x.Id == User.Id);
        //    QueryUser.State = false;
        //    DbContext.Entry(QueryUser).State = EntityState.Modified;
        //    DbContext.SaveChanges();

        //    return DbContext.Entry(QueryUser).Entity;
        //}


    }
}