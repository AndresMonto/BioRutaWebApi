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
    public class CollectController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DbContextBioRuta _DbContext;

        public CollectController(IConfiguration configuration, ILogger<UserController> logger, DbContextBioRuta DbContext)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._DbContext = DbContext;
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public List<Collect> GetCollects([FromBody] string Search)
        {
            return this._DbContext.GetCollects(Search);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public List<Collect> GetCollectsDate([FromBody] CollectSearch Search)
        {
            return this._DbContext.GetCollects(Search);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public User GetUserById([FromBody] int Id)
        {
            return this._DbContext.GetUserById(Id);
        }

        [Authorize]
        [HttpPost]
        [Route("[action]")]
        public string GetUserInfo()
        {
            return HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.UserData)?.FirstOrDefault()?.Value ?? "";
        }

        [HttpPost]
        [Route("[action]")]
        public User CreateUser([FromBody] User User)
        {
            try
            {
                return this._DbContext.CreateUser(User, false);
            }
            catch (Exception e)
            {
                User.Error = true;
                User.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return User;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public User CreateClient([FromBody] User User)
        {
            try
            {
                return this._DbContext.CreateUser(User, true);
            }
            catch (Exception e)
            {
                User.Error = true;
                User.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return User;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public Login SignIn([FromBody] Login login)
        {
            try
            {
                return login.ValidUserSignIn(this._DbContext, this._configuration);
            }
            catch (Exception e)
            {
                login.Error = true;
                login.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return login;
            }
        }

        [HttpPost]
        [Route("[action]")]
        public User DeleteUser([FromBody] User User)
        {
            try
            {
                return this._DbContext.DeleteUser(User);
            }
            catch (Exception e)
            {
                User.Error = true;
                User.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return User;
            }

        }


        [HttpPost]
        [Route("[action]")]
        public User UpdateUser([FromBody] User User)
        {
            try
            {
                return this._DbContext.UpdateUser(User);
            }
            catch (Exception e)
            {
                User.Error = true;
                User.Message = e.Message;
                _logger.LogError("Login: " + e.Message, e);
                return User;
            }

        }

    }
}