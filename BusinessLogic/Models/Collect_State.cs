﻿using BusinessLogic.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoReciclaje.Models
{
    [Table("Collect_Measure")]
    public class Measure : BaseModel
    {
        public string Name { get; set; }
    }
}