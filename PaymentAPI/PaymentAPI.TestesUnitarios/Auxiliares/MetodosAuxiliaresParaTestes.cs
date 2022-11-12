using Microsoft.EntityFrameworkCore;
using PaymentAPI.Context;
using PaymentAPI.Entities;
using PaymentAPI.TestesUnitarios.RepositorioTeste;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.TestesUnitarios.Auxiliares
{
    //objetivo dessa classe é deixar os metodos de testes mais limpos
    public class MetodosAuxiliaresParaTestes
    {
        public DbContextOptions<PaymentContext> InicializandoDBSimulada()
        {
            //usando dependencia InMemory pois após pesquisas descobri que o Moq/Mock
            //não funciona com o DbSet do EFCore que foi ensinado no curso.
            return new DbContextOptionsBuilder<PaymentContext>()
                .UseInMemoryDatabase(databaseName: "dbLoja")
                .Options;
        }

        public void CarregandoDadosNaDb(DbContextOptions<PaymentContext> options)
        {
            // inserindo dados na db usando uma instancia do context
            using (var context = new PaymentContext(options))
            {
                context.Vendas.Add(new VendasRepo().VendasTeste1());
                context.Vendas.Add(new VendasRepo().VendasTeste2());

                context.Vendas.Add(new Venda
                {
                    Data = DateTime.Now,
                    Vendedor = new VendedorRepo().VendedorNayala(),
                    ItensVendidos = new ProdutosRepo().DoisProdutos(),
                    StatusVenda = Enums.EnumStatusVenda.AguardandoPagamento
                });
            }
        }
    }
}
