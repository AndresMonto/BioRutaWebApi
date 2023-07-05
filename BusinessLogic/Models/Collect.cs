using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Collects")]
    public class Collect : BaseModel
    {
        public DateTime RegistrationDate { get; set; }


        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public User Client { get; set; }

        public int StateId { get; set; }
        [ForeignKey("StateId")]
        public Collect_State State { get; set; }


        public string? ObservationCli { get; set; }
        public string? ObservationRec { get; set; }
    }

    public class CollectSearch {
        public DateTime DateIni { get; set; }
        public DateTime DateFin { get; set; }
    }
}
