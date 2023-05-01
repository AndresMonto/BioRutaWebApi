using BusinessLogic.Models;

namespace ProyectoReciclaje.Models
{
    public class Login : BaseModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
