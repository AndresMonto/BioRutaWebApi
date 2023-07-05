using BusinessLogic.Context;
using ProyectoReciclaje.Models;

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