using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeSia.Models
{
    public class Quadrante
    {
        [Key]
        public int IdQuadrante { get; set; }

        [Required]
        public string Nome_Quad { get; set; }

        public int PosicaoQuadrante { get; set; }//0,1,2 ou 3, posições dos 4 quadrantes

        public Tecnica Tecnica { get; set; }

        public int TecnicaId { get; set; }
    }
}
