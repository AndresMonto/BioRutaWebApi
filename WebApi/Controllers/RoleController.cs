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
    public class RoleController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DbContextBioRuta _DbContext;

        public RoleController(IConfiguration configuration, ILogger<UserController> logger, DbContextBioRuta DbContext)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._DbContext = DbContext;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public List<Role> GetProducts()
        {
            return this._DbContext.GetRoles();
        }

    }
}