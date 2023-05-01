﻿using Microsoft.EntityFrameworkCore;
using ProyectoReciclaje.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Context
{
    public class DbContextBioRuta : DbContext
    {   
        public DbContextBioRuta(DbContextOptions<DbContextBioRuta> options): base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
    }
}
