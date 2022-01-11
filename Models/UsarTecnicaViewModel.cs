using Microsoft.EntityFrameworkCore;
using ProjetoDeSia.Data;
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

        //vou meter aqui os 4 quadrantes
        public Quadrante oQuadrante1 { get; set; }
        public Quadrante oQuadrante2 { get; set; }
        public Quadrante oQuadrante3 { get; set; }
        public Quadrante oQuadrante4 { get; set; }

        public List<Item> oListItem { get; set; }
        public List<Quadrante> oListQuadrante { get; set; }


        //ver se o utilizador tem autorização para aceder a viewModel
        public bool temPermicoes { get; set; }

        public int[,] matrizgraficos { get; set; }

        //cenas para a pontuacao geral
        public int pontuacaoGeralquad1 { get; set; }
        public int pontuacaoGeralquad2 { get; set; }
        public int pontuacaoGeralquad3 { get; set; }
        public int pontuacaoGeralquad4 { get; set; }




        //gerar as viewbags  GraficoQuadNomeXimportancia----------------------
        public async Task gerarGraficoQuadNomeXimportanciaAsync(int idTecnica, ProjetoDeSiaContext _context)
        {
            matrizgraficos = new int[5, 5];
            //inicializar a matriz toda a 0
            for (int j = 0; j < 5; j++)
            {
                for (int i = 0; i < 5; i++)
                {
                    //j é o quad, i é a importancia
                    matrizgraficos[j, i] = 0;
                }
            }


            foreach (Item it in _context.Item.ToList())
            {
                if (it.TecnicaId == idTecnica)
                {

                    //buscar o quadrante à base de dados
                    Quadrante quad = await _context.Quadrante.FirstOrDefaultAsync(q => q.IdQuadrante == it.QuadId);
                    //verificar a que quadrante pertence


                    if (quad.PosicaoQuadrante == 1)
                    {
                        matrizgraficos[1, it.Importancia]++;
                    }
                    if (quad.PosicaoQuadrante == 2)
                    {
                        matrizgraficos[2, it.Importancia]++;
                    }
                    if (quad.PosicaoQuadrante == 3)
                    {
                        matrizgraficos[3, it.Importancia]++;
                    }
                    if (quad.PosicaoQuadrante == 4)
                    {
                        matrizgraficos[4, it.Importancia]++;
                    }
                }
            }
        }


        //----------------------------------------------------------------------------
        public async Task CriarPontuacaoGeralAsync(int idtecnica, ProjetoDeSiaContext _context)
        {
            pontuacaoGeralquad1 = 0;
            pontuacaoGeralquad2 = 0;
            pontuacaoGeralquad3 = 0;
            pontuacaoGeralquad4 = 0;

            foreach (Item it in _context.Item.ToList())
            {
                if (it.TecnicaId == idtecnica)
                {

                    //buscar o quadrante à base de dados
                    Quadrante quad = await _context.Quadrante.FirstOrDefaultAsync(q => q.IdQuadrante == it.QuadId);
                    //verificar a que quadrante pertence
                    if (quad.PosicaoQuadrante == 1)
                    {
                        pontuacaoGeralquad1 = pontuacaoGeralquad1 + Convert.ToInt32(it.Pontucao);
                    }
                    if (quad.PosicaoQuadrante == 2)
                    {
                        pontuacaoGeralquad2 = pontuacaoGeralquad2 + Convert.ToInt32(it.Pontucao);
                    }
                    if (quad.PosicaoQuadrante == 3)
                    {
                        pontuacaoGeralquad3 = pontuacaoGeralquad3 + Convert.ToInt32(it.Pontucao);
                    }
                    if (quad.PosicaoQuadrante == 4)
                    {
                        pontuacaoGeralquad4 = pontuacaoGeralquad4 + Convert.ToInt32(it.Pontucao);
                    }
                }
            }
        }


        //------------------------Preencher os 4 quadrantes
        public void Preencher4quadrantes(int idtecnica, ProjetoDeSiaContext _context)
        {
            //vai criar as viewbags que vai enviar para a view
            foreach (Quadrante quad in _context.Quadrante.ToList())
            {
                if (quad.TecnicaId == idtecnica)
                {
                    //verificar a que quadrante pertence
                    if (quad.PosicaoQuadrante == 1)
                    {
                        oQuadrante1 = quad;
                    }
                    if (quad.PosicaoQuadrante == 2)
                    {
                        oQuadrante2 = quad;
                    }
                    if (quad.PosicaoQuadrante == 3)
                    {
                        oQuadrante3 = quad;
                    }
                    if (quad.PosicaoQuadrante == 4)
                    {
                        oQuadrante4 = quad;
                    }
                }
            }


        }
    }
}