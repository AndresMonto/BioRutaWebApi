using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using ProyectoReciclaje.Models;
using System.IdentityModel.Tokens.Jwt;

namespace BusinessLogic.Utilities
{
    public static class RoleLogic
    {
        public static List<Role> GetRoles(this DbContextBioRuta DbContext)
        {
            var QueryUser = DbContext.Role;

            return QueryUser.ToList();
        }


    }
}