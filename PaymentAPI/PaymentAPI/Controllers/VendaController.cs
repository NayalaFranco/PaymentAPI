using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Context;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendaController : ControllerBase
    {
        private readonly PaymentContext _context;

        public VendaController(PaymentContext context)
        {
            _context = context;
        }

        //BuscarVenda, RegistrarVenda, AtualizarVenda

        [HttpGet("BuscarVenda/{id}")]
        public IActionResult BuscarVenda(int id)
        {
            /*   solução para corrigir um erro onde por conta do 
             *   include se a venda não existisse ele não reconhecia
             *   como nulo no if */
            var vendaTeste = _context.Vendas.Find(id);

            if (vendaTeste is null)
                return NotFound();

            var venda = _context.Vendas.Where(x => x.Id == id)
                .Include(v => v.Vendedor).Include(i => i.ItensVendidos);

            return Ok(venda);
        }


        [HttpPost("RegistrarVenda")]
        //analisando melhor essa venda, não está do jeito que eu queria
        public IActionResult RegistrarVenda(Venda venda)
        {
            if (venda.Vendedor is null)
                return NotFound("Vendedor não encontrado");

            if (venda.ItensVendidos.Count() <= 0)
                return BadRequest("É preciso ter ao menos 1 produto");

            _context.Add(venda);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarVenda), new { id = venda.Id }, venda);
        }

        [HttpPut("AtualizarVenda/{id}")]
        public IActionResult AtualizarVenda(int id, EnumStatusVenda status)
        {
            var venda = _context.Vendas.Find(id);

            if (venda is null)
                return NotFound();


            //loop para só permitir mudanças especificas

            //nota: porque se comparar enums com == dá certo,
            //mas se comparar com != a comparação buga e retorna tudo true? ;-;

            //De: `Aguardando pagamento` Para: `Pagamento Aprovado`
            //De: `Aguardando pagamento` Para: `Cancelada`
            if (venda.StatusVenda == EnumStatusVenda.AguardandoPagamento
                && (status == EnumStatusVenda.PagamentoAprovado || status == EnumStatusVenda.Cancelado))
                {
                    return SalvaStatus(venda, status);
                }
            //De: `Pagamento Aprovado` Para: `Enviado para Transportadora`
            //De: `Pagamento Aprovado` Para: `Cancelada`
            else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado &&
                status == EnumStatusVenda.EnviadoParaTransportadora || status == EnumStatusVenda.Cancelado)
            {
                return SalvaStatus(venda, status);
            }
            //De: `Enviado para Transportador`. Para: `Entregue`
            else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado &&
               status == EnumStatusVenda.Entregue)
            {
                return SalvaStatus(venda, status);
            }
            
            return BadRequest("Transição de Status Inválida!");

        }

        private ObjectResult SalvaStatus(Venda venda, EnumStatusVenda status)
        {
            venda.StatusVenda = status;

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(venda);
        }


    }
}
