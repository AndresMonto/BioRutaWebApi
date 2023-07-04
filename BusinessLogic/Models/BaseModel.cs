using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [NotMapped]
        public string? Message { get; set; }
        [NotMapped]
        public bool Error { get; set; }
    }
}
