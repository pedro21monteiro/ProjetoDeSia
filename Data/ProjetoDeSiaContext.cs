using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoDeSia.Models;

namespace ProjetoDeSia.Data
{
    public class ProjetoDeSiaContext : DbContext
    {
        public ProjetoDeSiaContext (DbContextOptions<ProjetoDeSiaContext> options)
            : base(options)
        {
        }

        public DbSet<ProjetoDeSia.Models.Utilizador> Utilizador { get; set; }

        public DbSet<ProjetoDeSia.Models.Tecnica> Tecnica { get; set; }

        public DbSet<ProjetoDeSia.Models.Item> Item { get; set; }



 
    }
}
