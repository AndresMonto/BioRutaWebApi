using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Products")]
    public class Product : BaseModel
    {
        public string Name { get; set; }
        public int Score { get; set; }
        public bool State { get; set; }
    }
}
