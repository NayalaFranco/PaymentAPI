using PaymentAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.TestesUnitarios.RepositorioTeste
{
    internal class ProdutosRepo
    {
        public List<Produto> QuatroProdutos()
        {
            return new List<Produto>
            {
                new Produto
                {
                    Descricao = "TV 4k",
                    Quantidade = 1,
                    Valor = 2036.02M
                },
                new Produto
                {
                    Descricao = "Batedeira",
                    Quantidade = 1,
                    Valor = 570.36M
                },
                new Produto
                {
                    Descricao = "Ventilador",
                    Quantidade = 1,
                    Valor = 206.10M
                },
                new Produto
                {
                    Descricao = "Notebook",
                    Quantidade = 1,
                    Valor = 3449.99M
                }
            };
        }

        public List<Produto> TresProdutos()
        {
            return new List<Produto>
            {
                new Produto
                {
                    Descricao = "Videogame",
                    Quantidade = 1,
                    Valor = 1998.90M
                },
                new Produto
                {
                    Descricao = "Ar Condicionado",
                    Quantidade = 1,
                    Valor = 1529.00M
                },
                new Produto
                {
                    Descricao = "Secador de Cabelo",
                    Quantidade = 1,
                    Valor = 279.90M
                }
            };
        }

        public List<Produto> DoisProdutos()
        {
            return new List<Produto>
            {
                new Produto
                {
                    Descricao = "Frigideira",
                    Quantidade = 1,
                    Valor = 89.99M
                },
                new Produto
                {
                    Descricao = "Jogo de Lençol",
                    Quantidade = 1,
                    Valor = 109.90M
                }
            };
        }
    }
}
