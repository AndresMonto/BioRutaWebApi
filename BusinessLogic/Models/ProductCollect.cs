using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Products_Collects")]
    public class ProductCollect : BaseModel
    {

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public int CollectId { get; set; }
        //[ForeignKey("CollectId")]
        //public Collect Collect { get; set; }

        public int Amount { get; set; }

        public int MeasureId { get; set; }
        [ForeignKey("MeasureId")]
        public Measure Measure { get; set; }

    }
}
