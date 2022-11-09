using Microsoft.AspNetCore.Mvc;
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
        
        [HttpGet("{id}")]
        public IActionResult BuscarVenda(int id)
        {
            var venda = _context.Vendas.Find(id);

            if (venda is null)
                return NotFound();

            return Ok(venda);
        }

        [HttpPost]
        public IActionResult RegistrarVenda(Venda venda)
        {
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
