using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PaymentAPI.Context;
using PaymentAPI.Controllers;
using PaymentAPI.Entities;
using PaymentAPI.TestesUnitarios.Auxiliares;
using PaymentAPI.TestesUnitarios.RepositorioTeste;
using System.Net;

namespace PaymentAPI.TestesUnitarios.Testes
{
    public class TesteVendaController : MetodosAuxiliaresParaTestes
    {
        [Fact]
        public void TesteBuscarVenda200Ok()
        {
            var options = InicializandoDBSimulada();

            CarregandoDadosNaDb(options);

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