using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using DTO;
namespace DAL
{
    public class DataIO
    {
        private string xmlFilePath;

        public DataIO(string filePath)
        {
            xmlFilePath = filePath;
        }

        public LinkedList<ProductDTO> ReadData()
        {
            if (!File.Exists(xmlFilePath))
            {
                return new LinkedList<ProductDTO>();
            }
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ProductDTO>), new Type[] { typeof(OrderDTO) });
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Open))
                {
                    List<ProductDTO> list = (List<ProductDTO>)serializer.Deserialize(fs);
                    return new LinkedList<ProductDTO>(list);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi đọc file XML: " + ex.Message);
                return new LinkedList<ProductDTO>();
            }
        }

        public void WriteData(LinkedList<ProductDTO> data)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<ProductDTO>), new Type[] { typeof(OrderDTO) });
                using (FileStream fs = new FileStream(xmlFilePath, FileMode.Create))
                {
                    serializer.Serialize(fs, new List<ProductDTO>(data));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi ghi file XML: " + ex.Message);
            }
        }
    }
}
