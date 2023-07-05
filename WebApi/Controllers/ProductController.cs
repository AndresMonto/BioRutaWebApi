using BusinessLogic.Context;
using BusinessLogic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoReciclaje.Models;
using System.Resources;
using System.Security.Claims;

namespace ProyectoReciclaje.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DbContextBioRuta _DbContext;

        public ProductController(IConfiguration configuration, ILogger<UserController> logger, DbContextBioRuta DbContext)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._DbContext = DbContext;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public List<Product> GetProducts([FromBody] string Search)
        {
            return this._DbContext.GetProducts(Search);
        }

        [HttpPost]
        [Route("[action]")]
        public Product DeleteProduct([FromBody] Product Product)
        {
            try
            {
                return this._DbContext.DeleteProduct(Product);
            }
            catch (Exception e)
            {
                Product.Error = true;
                Product.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return Product;
            }

        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public Product GetProductById([FromBody] int Id)
        {
            return this._DbContext.GetProductById(Id);
        }

        [HttpPost]
        [Route("[action]")]
        public Product CreateProduct([FromBody] Product Produt)
        {
            try
            {
                return this._DbContext.CreateProduct(Produt);
            }
            catch (Exception e)
            {
                Produt.Error = true;
                Produt.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return Produt;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public Product UpdateProduct([FromBody] Product Product)
        {
            try
            {
                return this._DbContext.UpdateProduct(Product);
            }
            catch (Exception e)
            {
                Product.Error = true;
                Product.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return Product;
            }

        }

    }
}