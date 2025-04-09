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
        public void SaveOrders(string filePath, List<OrderDTO> orders)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<OrderDTO>), new Type[] {
                    typeof(OrderDTO), typeof(OrderItemDTO), typeof(PhoneDTO), typeof(LaptopDTO)
                });
                using FileStream fs = new FileStream(filePath, FileMode.Create);
                serializer.Serialize(fs, orders);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi luu don hang: " + ex.Message);
            }
        }

        public List<OrderDTO> LoadOrders(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return new List<OrderDTO>();
                XmlSerializer serializer = new XmlSerializer(typeof(List<OrderDTO>), new Type[] {
                    typeof(OrderDTO), typeof(OrderItemDTO), typeof(PhoneDTO), typeof(LaptopDTO)
                });
                using FileStream fs = new FileStream(filePath, FileMode.Open);
                return (List<OrderDTO>)serializer.Deserialize(fs);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi khi doc don hang: " + ex.Message);
                return new List<OrderDTO>();
            }
        }
    }
}
