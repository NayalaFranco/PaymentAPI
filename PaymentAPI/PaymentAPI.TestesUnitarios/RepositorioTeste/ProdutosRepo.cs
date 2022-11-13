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
                    Id = 0,
                    Descricao = "TV 4k",
                    Quantidade = 1,
                    Valor = 2036.02M
                },
                new Produto
                {
                    Id = 0,
                    Descricao = "Batedeira",
                    Quantidade = 1,
                    Valor = 570.36M
                },
                new Produto
                {
                    Id = 0,
                    Descricao = "Ventilador",
                    Quantidade = 1,
                    Valor = 206.10M
                },
                new Produto
                {
                    Id = 0,
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
                    Id = 0,
                    Descricao = "Videogame",
                    Quantidade = 1,
                    Valor = 1998.90M
                },
                new Produto
                {
                    Id = 0,
                    Descricao = "Ar Condicionado",
                    Quantidade = 1,
                    Valor = 1529.00M
                },
                new Produto
                {
                    Id = 0,
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
                    Id = 0,
                    Descricao = "Frigideira",
                    Quantidade = 1,
                    Valor = 89.99M
                },
                new Produto
                {
                    Id = 0,
                    Descricao = "Jogo de Lençol",
                    Quantidade = 1,
                    Valor = 109.90M
                }
            };
        }
    }
}
