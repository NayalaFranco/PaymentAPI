﻿using Microsoft.AspNetCore.Mvc;
using PaymentAPI.Context;
using PaymentAPI.Entities;
using System.ComponentModel.DataAnnotations;

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

        // metodo somente para gerar uns vendedores para testes rapidos via swagger
        [HttpPost("GeraVendedoresTeste")]
        public IActionResult GeraVendedoresTeste()
        {
            AdicionarVendedor("Daniel", "1111111111", "daniel@email.com", "(00) 0000-0000");
            AdicionarVendedor("Nayala", "2222222222", "nay@mail.com", "(11) 1111-1111");

            return Ok("Vendedores Gerados Para Teste!");
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
        public IActionResult AdicionarVendedor([Required] string Nome, [Required] string Cpf,
            //mais facil manipular entrada de email usando DataAnnotations
            [Required][EmailAddress] string Email, [Required] string Telefone)
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

        // Deletar um vendedor vinculado a uma venda pode causar um problemão
        /*
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
        */
        
    }
}
