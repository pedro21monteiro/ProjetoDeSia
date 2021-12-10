using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeSia.Models
{
    public class Item
    {
        [Key]
        public int IdItem { get; set; }

        [Required]
        public string Descricao { get; set; }

       
        //não é required pois a pontuação é gerada automaticamente
        public double Pontucao { get; set; }//0,1,2 ou 3, posições dos 4 quadrantes
        [Required]
        public int Importancia { get; set; }//0,1,2,3,4 ou 5

        //a classificacao é o nome do quadrante
        public string classificacao { get; set; }


        public Tecnica Tecnica { get; set; }

        public int TecnicaId { get; set; }

        //tem um quadrante id
        public Quadrante Quadrante { get; set; }
        public int QuadId { get; set; }
    }
}
