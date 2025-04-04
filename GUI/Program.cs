

using System;
using BLL;
using DTO;
using System.Collections.Generic;
using DAL;

namespace ConsoleApp
{
    class Program
    {
        static LinkedList<ProductDTO> linkedList = new LinkedList<ProductDTO>();
        static DataIO dataIO = new DataIO("D:\\Study\\oop\\QLDT_DATANG\\GUI\\SanPham.xml");
        static OrderBLL orderBLL = new OrderBLL();

        static void Main(string[] args)
        {
            linkedList = dataIO.ReadData();
            if (linkedList.Count == 0)
            {
                linkedList.AddLast(new ProductDTO("P001", "Điện thoại A", 500, 10, "Điện thoại thông minh A"));
                linkedList.AddLast(new ProductDTO("P002", "Điện thoại B", 700, 5, "Điện thoại thông minh B"));
                linkedList.AddLast(new ProductDTO("P003", "Điện thoại C", 900, 8, "Điện thoại thông minh C"));
            }

            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("Chọn chức năng: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        DisplayList();
                        break;
                    case "2":
                        CreateOrder();
                        break;
                    case "3":
                        AddProductToOrder();
                        break;
                    case "4":
                        RemoveProductFromOrder();
                        break;
                    case "5":
                        UpdateProductQuantityInOrder();
                        break;
                    case "6":
                        PrintOrderDetails();
                        break;
                    case "7":
                        dataIO.WriteData(linkedList);
                        Console.WriteLine("Dữ liệu đã được lưu vào file XML.");
                        break;
                    case "8":
                        linkedList = dataIO.ReadData();
                        Console.WriteLine("Dữ liệu đã được đọc từ file XML.");
                        break;
                    case "9":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Chức năng không hợp lệ. Vui lòng chọn lại.");
                        break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n===== MENU =====");
            Console.WriteLine("1. Hiển thị danh sách sản phẩm & đơn hàng");
            Console.WriteLine("2. Tạo đơn hàng mới");
            Console.WriteLine("3. Thêm sản phẩm vào đơn hàng");
            Console.WriteLine("4. Xóa sản phẩm khỏi đơn hàng");
            Console.WriteLine("5. Cập nhật số lượng sản phẩm trong đơn hàng");
            Console.WriteLine("6. In chi tiết đơn hàng");
            Console.WriteLine("7. Lưu dữ liệu vào file XML");
            Console.WriteLine("8. Đọc dữ liệu từ file XML");
            Console.WriteLine("9. Thoát");
        }

        // Hiển thị danh sách liên kết: nếu đối tượng là OrderDTO thì in chi tiết đơn hàng, nếu là ProductDTO thì in thông tin sản phẩm
        static void DisplayList()
        {
            foreach (var item in linkedList)
            {
                if (item is OrderDTO order)
                {
                    Console.WriteLine("\n----- ĐƠN HÀNG -----");
                    Console.WriteLine(order.GetOrderDetails());
                }
                else
                {
                    ProductDTO product = item as ProductDTO;
                    Console.WriteLine("\n----- SẢN PHẨM -----");
                    Console.WriteLine($"Mã: {product.ProductID}, Tên: {product.Name}, Giá: {product.Price}, Tồn: {product.Stock}, Mô tả: {product.Description}");
                }
            }
        }

        // Tìm đơn hàng theo mã trong danh sách liên kết
        static OrderDTO FindOrderByID(string orderID)
        {
            foreach (var item in linkedList)
            {
                if (item is OrderDTO order && order.OrderID == orderID)
                    return order;
            }
            return null;
        }

        // Tìm sản phẩm theo mã (chỉ duyệt các đối tượng không phải OrderDTO)
        static ProductDTO FindProductByID(string productID)
        {
            foreach (var item in linkedList)
            {
                if (!(item is OrderDTO) && item.ProductID == productID)
                    return item;
            }
            return null;
        }

        static void CreateOrder()
        {
            Console.Write("Nhập mã đơn hàng: ");
            string orderID = Console.ReadLine();
            OrderDTO order = orderBLL.CreateOrder(orderID, DateTime.Now, "Chờ xử lý");
            linkedList.AddLast(order);
            Console.WriteLine("Đơn hàng đã được tạo.");
        }

        static void AddProductToOrder()
        {
            Console.Write("Nhập mã đơn hàng: ");
            string orderID = Console.ReadLine();
            OrderDTO order = FindOrderByID(orderID);
            if (order == null)
            {
                Console.WriteLine("Không tìm thấy đơn hàng.");
                return;
            }
            Console.Write("Nhập mã sản phẩm: ");
            string productID = Console.ReadLine();
            ProductDTO product = FindProductByID(productID);
            if (product == null)
            {
                Console.WriteLine("Không tìm thấy sản phẩm.");
                return;
            }
            Console.Write("Nhập số lượng đặt: ");
            if (!int.TryParse(Console.ReadLine(), out int quantity))
            {
                Console.WriteLine("Số lượng không hợp lệ.");
                return;
            }
            if (orderBLL.AddProductToOrder(order, product, quantity))
                Console.WriteLine("Sản phẩm đã được thêm vào đơn hàng.");
        }

        static void RemoveProductFromOrder()
        {
            Console.Write("Nhập mã đơn hàng: ");
            string orderID = Console.ReadLine();
            OrderDTO order = FindOrderByID(orderID);
            if (order == null)
            {
                Console.WriteLine("Không tìm thấy đơn hàng.");
                return;
            }
            Console.Write("Nhập mã sản phẩm cần xóa: ");
            string productID = Console.ReadLine();
            if (orderBLL.RemoveProductFromOrder(order, productID))
                Console.WriteLine("Sản phẩm đã được xóa khỏi đơn hàng.");
        }

        static void UpdateProductQuantityInOrder()
        {
            Console.Write("Nhập mã đơn hàng: ");
            string orderID = Console.ReadLine();
            OrderDTO order = FindOrderByID(orderID);
            if (order == null)
            {
                Console.WriteLine("Không tìm thấy đơn hàng.");
                return;
            }
            Console.Write("Nhập mã sản phẩm: ");
            string productID = Console.ReadLine();
            Console.Write("Nhập số lượng mới: ");
            if (!int.TryParse(Console.ReadLine(), out int newQuantity))
            {
                Console.WriteLine("Số lượng không hợp lệ.");
                return;
            }
            if (orderBLL.UpdateProductQuantity(order, productID, newQuantity))
                Console.WriteLine("Số lượng sản phẩm đã được cập nhật.");
        }

        static void PrintOrderDetails()
        {
            Console.Write("Nhập mã đơn hàng: ");
            string orderID = Console.ReadLine();
            OrderDTO order = FindOrderByID(orderID);
            if (order == null)
            {
                Console.WriteLine("Không tìm thấy đơn hàng.");
                return;
            }
            orderBLL.PrintOrderDetails(order);
        }
    }
}
