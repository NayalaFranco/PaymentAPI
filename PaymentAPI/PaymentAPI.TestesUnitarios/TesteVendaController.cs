using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Context;
using PaymentAPI.Controllers;
using PaymentAPI.Entities;
using PaymentAPI.TestesUnitarios.RepositorioTeste;
using System.Net;

namespace PaymentAPI.TestesUnitarios
{
    public class TesteVendaController
    {
        [Fact]
        public void TesteBuscarVenda200Ok()
        {
            List<Produto> produtos = new List<Produto>();
            //usando dependencia InMemory pois após pesquisas descobri que o Moq/Mock
            //não funciona com o DbSet do EFCore que foi ensinado no curso.
            var options = new DbContextOptionsBuilder<PaymentContext>()
                .UseInMemoryDatabase(databaseName: "dbLoja")
                .Options;

            // inserindo dados na db usando uma instancia do context
            using (var context = new PaymentContext(options))
            {
                context.Vendas.Add(new VendasRepo().VendasTeste1());
                context.Vendas.Add(new VendasRepo().VendasTeste2());

                context.Vendas.Add(new Venda { Data = DateTime.Now, 
                    Vendedor = new VendedorRepo().VendedorNayala(), 
                    ItensVendidos = new ProdutosRepo().DoisProdutos(), 
                    StatusVenda = Enums.EnumStatusVenda.AguardandoPagamento });
            }

            // usando uma instancia limpa do context para rodar o teste
            using (var context = new PaymentContext(options))
            {
                VendaController vendaController = new VendaController(context);
                var resultado = vendaController.BuscarVenda(1);

                Console.WriteLine(resultado);

                Assert.IsAssignableFrom<OkObjectResult>(resultado);
            }


            
        }
    }
}