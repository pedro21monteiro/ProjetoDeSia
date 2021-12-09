using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeSia.Models
{
    public class Utilizador
    {
        [Key]
        public int IdUtilizador { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public int Categoria { get; set; } //0- Admin, 1- utilizador

        public ICollection<Tecnica> Tecnica { get; set; }
    }
}
