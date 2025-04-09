using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhoneDTO : ProductDTO
    {
        public string HeDieuHanh { get; set; }
        public int DungLuongPin { get; set; }

        public PhoneDTO() { }

        public PhoneDTO(string productID, string name, decimal price, int stock, string description, string heDieuHanh, int dungLuongPin)
            : base(productID, name, price, stock, description)
        {
            HeDieuHanh = heDieuHanh;
            DungLuongPin = dungLuongPin;
        }
    }
}
