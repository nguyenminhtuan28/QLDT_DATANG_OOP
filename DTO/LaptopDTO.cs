using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class LaptopDTO : ProductDTO
    {
        public string CPU { get; set; }
        public int RAM { get; set; }

        public LaptopDTO() { }

        public LaptopDTO(string productID, string name, decimal price, int stock, string description, string cpu, int ram)
            : base(productID, name, price, stock, description)
        {
            CPU = cpu;
            RAM = ram;
        }
    }
}
