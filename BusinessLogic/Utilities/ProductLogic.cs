using BusinessLogic.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProyectoReciclaje.Models;

namespace BusinessLogic.Utilities
{
    public static class ProductLogic
    {
        public static List<Product> GetProducts(this DbContextBioRuta DbContext, string Search)
        {
            var QueryUser = DbContext.Product;

            if (!string.IsNullOrEmpty(Search))
            {
                if ("activo".Contains(Search.ToLower()) || "inactivo".Contains(Search.ToLower()))
                {
                    return QueryUser.Where(x => x.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Score.ToString().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.State == "activo".Contains(Search.ToLower()) ? true : !("inactivo".Contains(Search.ToLower()))).ToList();
                }
                else
                {
                    return QueryUser.Where(x => x.Name.Trim().ToLower().Contains(Search, StringComparison.OrdinalIgnoreCase) || x.Score.ToString().Contains(Search, StringComparison.OrdinalIgnoreCase)).ToList();
                }

            }

            return QueryUser.ToList();
        }

        public static Product CreateProduct(this DbContextBioRuta DbContext, Product Product)
        {
            Product.Message = "";
            Product.Error = false;

            var QueryUser = DbContext.Product.Where(x => x.Name.Trim().ToLower().Contains(Product.Name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (QueryUser.Any())
            {
                Product.Error = true;
                Product.Message = Resources.ProductoRepetido;
            }
            else
            {
                EntityEntry<Product> value = DbContext.Product.Add(Product);
                DbContext.SaveChanges();
                Product = value.Entity;
            }

            return Product;
        }

        public static Product UpdateProduct(this DbContextBioRuta DbContext, Product Product)
        {
            Product.Message = "";
            Product.Error = false;

            var QueryUser = DbContext.Product.Where(x => x.Id != Product.Id && x.Name.Trim().ToLower().Contains(Product.Name, StringComparison.OrdinalIgnoreCase)).ToList();

            if (QueryUser.Any())
            {
                Product.Error = true;
                Product.Message = Resources.ProductoRepetido;
            }
            else
            {
                DbContext.Entry(Product).State = EntityState.Modified;
                DbContext.SaveChanges();
                Product = DbContext.Entry(Product).Entity;
            }

            return Product;
        }

        public static Product DeleteProduct(this DbContextBioRuta DbContext, Product Product)
        {
            var QueryUser = DbContext.Product.FirstOrDefault(x => x.Id == Product.Id);
            QueryUser.State = false;
            DbContext.Entry(QueryUser).State = EntityState.Modified;
            DbContext.SaveChanges();

            return DbContext.Entry(QueryUser).Entity;
        }

        public static Product GetProductById(this DbContextBioRuta DbContext, int Id)
        {
            return DbContext.Product.FirstOrDefault(x => x.Id == Id);
        }
    }
}