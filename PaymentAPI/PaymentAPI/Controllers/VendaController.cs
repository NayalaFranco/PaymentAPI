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
        /*
        [HttpGet("{id}")]
        public IActionResult BuscarVenda(int id)
        {
            
        }

        [HttpPost]
        public IActionResult RegistrarVenda(Venda venda)
        {
            
        }

        [HttpPatch("{id}")]
        public IActionResult AtualizarVenda(int id, EnumStatusVenda status)
        {
            
        }
        */
    }
}
