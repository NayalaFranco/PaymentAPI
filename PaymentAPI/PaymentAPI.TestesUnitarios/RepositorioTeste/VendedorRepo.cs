using PaymentAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentAPI.TestesUnitarios.RepositorioTeste
{
    internal class VendedorRepo
    {
        public Vendedor VendedorDaniel()
        {
            return new Vendedor
            {
                Nome = "Daniel",
                Cpf = "111.111.111-11",
                Email = "daniel@email.com",
                Telefone = "(11) 1111-1111"
            };               
        }

        public Vendedor VendedorNayala()
        {
            return new Vendedor
            {
                Nome = "Nayala",
                Cpf = "222.222.222-22",
                Email = "nay@email.com",
                Telefone = "(22) 2222-2222"
            };
        }       
    }
}
