using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDeSia.Models
{
    public class UsarTecnicaViewModel
    {
        public Tecnica oTecnica { get; set; }
        public Item oItem { get; set; }
        public Quadrante oQuadrante { get; set; }

        public List<Item> oListItem { get; set; }
        public List<Quadrante> oListQuadrante { get; set; }


        //ver se o utilizador tem autorização para aceder a viewModel
        public bool temPermicoes { get; set; }

    }
}
