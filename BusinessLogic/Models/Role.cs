using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Roles")]
    public class Role : BaseModel
    {
        public string Name { get; set; }
    }
}
