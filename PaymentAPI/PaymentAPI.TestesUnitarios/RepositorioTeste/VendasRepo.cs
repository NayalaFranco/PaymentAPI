using PaymentAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.TestesUnitarios.RepositorioTeste
{
    internal class VendasRepo
    {
        public Venda VendasTeste1()
        {
            return new Venda
            {
                Id = 0,
                Data = new DateTime(2022, 11, 01, 10, 23, 00),
                ItensVendidos = new ProdutosRepo().TresProdutos(),
                StatusVenda = Enums.EnumStatusVenda.AguardandoPagamento,
                Vendedor = new VendedorRepo().VendedorDaniel()
            };
        }


        public Venda VendasTeste2()
        {
            return new Venda
            {
                Id = 0,
                Data = new DateTime(2022, 11, 10, 14, 45, 00),
                ItensVendidos = new ProdutosRepo().QuatroProdutos(),
                StatusVenda = Enums.EnumStatusVenda.AguardandoPagamento,
                Vendedor = new VendedorRepo().VendedoraNayala()
            };
        }
    }
}
