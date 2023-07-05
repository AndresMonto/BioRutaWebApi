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

        public static List<Collect> GetCollects(this DbContextBioRuta DbContext, string Search)
        {
            var QueryUser = DbContext.Collect.Include(x => x.Client).Include(x => x.State);

            if (!string.IsNullOrEmpty(Search))
            {
                return QueryUser.Where(x => x.Client.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.State.Name.ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            return QueryUser.ToList();
        }

        public static List<Collect> GetCollects(this DbContextBioRuta DbContext, CollectSearch Search)
        {
            var QueryUser = DbContext.Collect.Include(x => x.Client).Include(x => x.State);
            return QueryUser.Where(x => x.RegistrationDate.Date >= Search.DateIni.Date && x.RegistrationDate.Date <= Search.DateFin.Date).ToList();
        }

        public static Collect GetCollectById(this DbContextBioRuta DbContext, int Id)
        {
            var QueryUser = DbContext.Collect.Include(x => x.Client).Include(x => x.State).Include(x => x.Collecter).FirstOrDefault(x=> x.Id == Id);
            QueryUser.Products = DbContext.Product_Collect.Include(x => x.Product).Include(x => x.Measure).Where(x=>x.CollectId == Id).ToList();
            return QueryUser;
        }

    }
}