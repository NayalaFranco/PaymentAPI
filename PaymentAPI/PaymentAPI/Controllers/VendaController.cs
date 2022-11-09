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
        /*
        [HttpPatch("{id}")]
        public IActionResult AtualizarVenda(int id, EnumStatusVenda status)
        {
            
        }
        */
    }
}
