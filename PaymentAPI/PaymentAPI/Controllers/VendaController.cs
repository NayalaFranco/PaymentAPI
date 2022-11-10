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
            var venda = _context.Vendas.Where(x => x.Id == id)
                .Include(v => v.Vendedor).Include(i => i.ItensVendidos);

            if (venda is null)
                return NotFound();

            return Ok(venda);
        }


        [HttpPost("RegistrarVenda")]
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
            /*
            * De: `Aguardando pagamento` Para: `Pagamento Aprovado`
            * De: `Aguardando pagamento` Para: `Cancelada`
            * De: `Pagamento Aprovado` Para: `Enviado para Transportadora`
            * De: `Pagamento Aprovado` Para: `Cancelada`
            * De: `Enviado para Transportador`. Para: `Entregue`
            */
            //nota: porque se comparar enums com == dá certo,
            //mas se comparar com != a comparação buga e retorna tudo true?
            if (venda.StatusVenda == EnumStatusVenda.AguardandoPagamento
                && (status == EnumStatusVenda.PagamentoAprovado || status == EnumStatusVenda.Cancelado))
                {
                    return BadRequest("deu");
                }
            
                //bad ou status code not modified?
                
            /*
            else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado &&
                status != EnumStatusVenda.EnviadoParaTransportadora || status != EnumStatusVenda.Cancelado)
            {
                //bad ou status code not modified?
                return BadRequest("Transição de status inválida");
            }
            else if (venda.StatusVenda == EnumStatusVenda.PagamentoAprovado &&
               status != EnumStatusVenda.Entregue)
            {
                //bad ou status code not modified?
                return BadRequest("Transição de status inválida");
            }
            */

            venda.StatusVenda = status;

            _context.Vendas.Update(venda);
            _context.SaveChanges();

            return Ok(venda);
        }
    }
}
