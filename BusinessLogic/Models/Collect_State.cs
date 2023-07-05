using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Collect_States")]
    public class Collect_State : BaseModel
    {
        public string Name { get; set; }
    }
}
