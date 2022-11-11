using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Context;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VendedorController : ControllerBase
    {

        private readonly PaymentContext _context;

        public VendedorController(PaymentContext context)
        {
            _context = context;
        }

        [HttpGet("BuscarVendedor/{id}")]
        public IActionResult BuscarVendedor(int id)
        {
            var vendedor = _context.Vendedores.Find(id);

            if (vendedor is null)
                return NotFound();

            return Ok(vendedor);
        }


        [HttpPost("AdicionarVendedor")]
        public IActionResult AdicionarVendedor(string Nome, string Cpf, string Email, string Telefone)
        {
            Vendedor vendedor = new Vendedor();

            vendedor.Nome = Nome;
            vendedor.Cpf = Cpf;
            vendedor.Email = Email;
            vendedor.Telefone = Telefone;

            _context.Add(vendedor);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarVendedor), new { id = vendedor.Id }, vendedor);
        }

        
        [HttpDelete("DeletarVendedor/{id}")]
        public IActionResult DeletarVendedor(int id)
        {
            var vendedor = _context.Vendedores.Find(id);

            if (vendedor is null)
                return NotFound();

            _context.Vendedores.Remove(vendedor);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}
