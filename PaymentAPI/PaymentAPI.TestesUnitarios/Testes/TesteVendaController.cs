using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Context;
using PaymentAPI.Controllers;
using PaymentAPI.Entities;
using PaymentAPI.Enums;
using PaymentAPI.TestesUnitarios.RepositorioTeste;

namespace PaymentAPI.TestesUnitarios.Testes
{
    public class TesteVendaController
    {
        private readonly PaymentContext _context;
        VendaController _vendaController;

        public TesteVendaController()
        {

            var options = new DbContextOptionsBuilder<PaymentContext>()
                .UseInMemoryDatabase(databaseName: "dbLoja")
                .Options;

            _context = new PaymentContext(options);

            _vendaController = new VendaController(_context);

            // inicializando 2 vendedores
            _context.Vendedores.Add(new VendedorRepo().VendedorDaniel());
            _context.Vendedores.Add(new VendedorRepo().VendedoraNayala());

            // inicializando algumas uma vendas por garantia dado a execução aleatoria dos testes.
            _context.Vendas.Add(new VendasRepo().VendasTeste1());
            _context.Vendas.Add(new VendasRepo().VendasTeste2());
            _context.Vendas.Add(new VendasRepo().VendasTeste2());
            _context.SaveChanges();
        }


        // Testes:
        [Theory(DisplayName = "Teste Registrar Venda Ok")]
        [MemberData(nameof(DataProdutos))]
        public void TesteRegistrarVenda201Created(int IdVendedor, DateTime Data, List<Produto> ListaProdutos)
        {
            var resultado = _vendaController.RegistrarVenda(IdVendedor,
                Data, ListaProdutos);

            Assert.IsAssignableFrom<CreatedAtActionResult>(resultado);
        }
        

        [Fact(DisplayName = "Teste Registrar Venda NotFound Vendedor")]
        public void TesteRegistrarVendaE404()
        {
            var resultado = _vendaController.RegistrarVenda(200,
                DateTime.Now, new ProdutosRepo().DoisProdutos());

            Assert.IsAssignableFrom<NotFoundObjectResult>(resultado);
        }

        [Fact(DisplayName = "Teste Registrar Venda Sem Produto")]
        public void TesteRegistrarVendaSemProdutoE400()
        {
            var resultado = _vendaController.RegistrarVenda(1,
                DateTime.Now, null);

            Assert.IsAssignableFrom<BadRequestObjectResult>(resultado);
        }


        [Fact(DisplayName = "Teste Buscar Venda")]
        public void TesteBuscarVenda200Ok()
        {
            var resultado = _vendaController.BuscarVenda(1);

            Assert.IsAssignableFrom<OkObjectResult>(resultado);          
        }

        [Fact(DisplayName = "Teste Atualizar Status de Aguardando Pagamento Até Entregue")]
        public void TesteAtualizarStatusLinearAteEntregue200Ok()
        {
            _vendaController.AtualizarVenda(1, EnumStatusVenda.PagamentoAprovado);
            _vendaController.AtualizarVenda(1, EnumStatusVenda.EnviadoParaTransportadora);
            var resultado = _vendaController.AtualizarVenda(1, EnumStatusVenda.Entregue);

            Assert.IsAssignableFrom<OkObjectResult>(resultado);
        }

        [Fact(DisplayName = "Teste Atualizar Status de Aguardando Pagamento Até Cancelado")]
        public void TesteAtualizarStatusLinearAteCancelado200Ok()
        {
            _vendaController.AtualizarVenda(2, EnumStatusVenda.PagamentoAprovado);
            var resultado = _vendaController.AtualizarVenda(2, EnumStatusVenda.Cancelado);

            Assert.IsAssignableFrom<OkObjectResult>(resultado);
        }

        [Theory(DisplayName = "Teste de Falha ao Atualizar Status")]
        [InlineData(EnumStatusVenda.EnviadoParaTransportadora)]
        [InlineData(EnumStatusVenda.Entregue)]
        public void TesteDeFalhaAoAtualizarStatusE400(EnumStatusVenda status)
        {
            _vendaController.AtualizarVenda(3, status);
            var resultado = _vendaController.AtualizarVenda(3, status);

            Assert.IsAssignableFrom<BadRequestObjectResult>(resultado);
        }

        // para alimentar o theory do metodo de registrar venda
        public static IEnumerable<object[]> DataProdutos()
        {
            yield return new object[]
            {
                1,
                new DateTime(2022, 11, 12),
                new ProdutosRepo().QuatroProdutos()
            };
            yield return new object[]
            {
                2,
                new DateTime(2022, 11, 10),
                new ProdutosRepo().TresProdutos()
            };
            yield return new object[]
            {
                1,
                new DateTime(2022, 11, 11),
                new ProdutosRepo().DoisProdutos()
            };
            yield return new object[]
            {
                2,
                new DateTime(2022, 11, 12),
                new ProdutosRepo().QuatroProdutos()
            };

        }
    }

        
}