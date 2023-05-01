using BusinessLogic.Context;
using BusinessLogic.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProyectoReciclaje.Models;
using System.Security.Claims;

namespace ProyectoReciclaje.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;
        private readonly IConfiguration _configuration;
        private readonly DbContextBioRuta _DbContext;

        public UserController(IConfiguration configuration, ILogger<UserController> logger, DbContextBioRuta DbContext)
        {
            this._logger = logger;
            this._configuration = configuration;
            this._DbContext = DbContext;
        }

        [HttpGet]
        [Route("[action]")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
            })
            .ToArray();
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
                var test = this._DbContext.User.Add(User);
                this._DbContext.SaveChanges();
                return test.Entity;
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

    }
}