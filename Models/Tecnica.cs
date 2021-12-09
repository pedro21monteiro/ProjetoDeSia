using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeSia.Models
{
    public class Tecnica
    {
        [Key]
        public int IdTecnica { get; set; }

        [Required]
        public string Nome { get; set; }


        [Required]
        public string Descricao { get; set; }

        public Utilizador Utilizador { get; set; }

        public int UtilizadorId { get; set; }

        public ICollection<Quadrante> Quadrante { get; set; }

        public ICollection<Item> Item { get; set; }

    }
}
