using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Xml;
using DTO;

namespace DAL
{
    internal class SanPhamDAL
    {
    }
    public class ProductDAL
    {
        private readonly string _xmlFilePath;

        public ProductDAL(string xmlFilePath)
        {
            _xmlFilePath = xmlFilePath;
        }

        public List<ProductDTO> GetAllProducts()
        {
            List<ProductDTO> products = new List<ProductDTO>();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(_xmlFilePath);
                XmlNodeList productNodes = doc.SelectNodes("//Products/Product");

                foreach (XmlNode node in productNodes)
                {
                    ProductDTO product = new ProductDTO
                    {
                        ProductID = node["ProductID"]?.InnerText,
                        Name = node["Name"]?.InnerText,
                        Price = decimal.Parse(node["Price"]?.InnerText ?? "0"),
                        Stock = int.Parse(node["Stock"]?.InnerText ?? "0"),
                        Description = node["Description"]?.InnerText
                    };

                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file XML: " + ex.Message);
            }

            return products;
        }
    }
}
