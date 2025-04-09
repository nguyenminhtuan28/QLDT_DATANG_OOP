

using System;
using BLL;
using DTO;
using System.Collections.Generic;
using DAL;

namespace ConsoleApp
{
    class Program
    {
        static ProductDAL productDAL = new ProductDAL();
        static ProductBLL productBLL = new ProductBLL(productDAL);
        static OrderBLL orderBLL = new OrderBLL();

        static List<OrderDTO> orders = new List<OrderDTO>();

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                ShowMenu();
                Console.Write("Chon chuc nang: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddPhone(); break;
                    case "2": AddLaptop(); break;
                    case "3": DisplayProducts(); break;
                    case "4": CreateOrder(); break;
                    case "5": AddProductToOrder(); break;
                    case "6": RemoveProductFromOrder(); break;
                    case "7": UpdateProductQuantity(); break;
                    case "8": PrintOrderDetails(); break;
                    case "9":
                        new DataIO().SaveOrders("C:\\Users\\TUAN\\Source\\Repos\\QLDT_DATANG_OOP\\GUI\\SanPham.xml", orders);
                        Console.WriteLine("Da luu don hang vao file xml");
                        break;
                    case "10":
                        orders = new DataIO().LoadOrders("orders.xml");
                        Console.WriteLine("Da tai don hang tu file xml");
                        break;
                    case "11":
                        exit = true; break;
                    default: Console.WriteLine("Lua chon khong hop le."); break;
                }
            }
        }

        static void ShowMenu()
        {
            Console.WriteLine("===== MENU ===== ");
            Console.WriteLine("1. Them dien thoai");
            Console.WriteLine("2. Them laptop");
            Console.WriteLine("3. Hien thi tat ca san pham");
            Console.WriteLine("4. Tao don hang");
            Console.WriteLine("5. Them san pham vao don hang");
            Console.WriteLine("6. Xoa san pham khoi don hang");
            Console.WriteLine("7. Cap nhat so luong trong don hang");
            Console.WriteLine("8. In chi tiet don hang");
            Console.WriteLine("9. Luu don hang vao file XML");
            Console.WriteLine("10. Doc don hang tu file XML");
            Console.WriteLine("11. Thoat");
        }

        static void AddPhone()
        {
            Console.Write("Ma SP: "); string id = Console.ReadLine();
            Console.Write("Ten SP: "); string name = Console.ReadLine();
            Console.Write("Gia: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Ton kho: "); int stock = int.Parse(Console.ReadLine());
            Console.Write("Mo ta: "); string desc = Console.ReadLine();
            Console.Write("He dieu hanh: "); string os = Console.ReadLine();
            Console.Write("Pin: "); int pin = int.Parse(Console.ReadLine());

            productBLL.AddProduct(new PhoneDTO(id, name, price, stock, desc, os, pin));
            Console.WriteLine("Da them dien thoai.");
        }

        static void AddLaptop()
        {
            Console.Write("Ma SP: "); string id = Console.ReadLine();
            Console.Write("Ten SP: "); string name = Console.ReadLine();
            Console.Write("Gia: "); decimal price = decimal.Parse(Console.ReadLine());
            Console.Write("Ton kho: "); int stock = int.Parse(Console.ReadLine());
            Console.Write("Mo ta: "); string desc = Console.ReadLine();
            Console.Write("CPU: "); string cpu = Console.ReadLine();
            Console.Write("RAM: "); int ram = int.Parse(Console.ReadLine());

            productBLL.AddProduct(new LaptopDTO(id, name, price, stock, desc, cpu, ram));
            Console.WriteLine("Da them laptop.");
        }

        static void DisplayProducts()
        {
            foreach (var p in productBLL.GetAllProducts())
            {
                if (p is PhoneDTO dt)
                    Console.WriteLine($"[DT] {dt.Name}, OS: {dt.HeDieuHanh}, Pin: {dt.DungLuongPin}mAh");
                else if (p is LaptopDTO lt)
                    Console.WriteLine($"[LT] {lt.Name}, CPU: {lt.CPU}, RAM: {lt.RAM}GB");
                else
                    Console.WriteLine($"[SP] {p.Name}");
            }
        }

        static void CreateOrder()
        {
            Console.Write("Nhap ma don hang: ");
            string id = Console.ReadLine();
            var order = orderBLL.CreateOrder(id, DateTime.Now, "Cho xu ly");
            orders.Add(order);
            Console.WriteLine("Da tao don hang.");
        }

        static OrderDTO FindOrder()
        {
            Console.Write("Nhap ma don hang: ");
            string id = Console.ReadLine();
            return orders.Find(o => o.OrderID == id);
        }

        static void AddProductToOrder()
        {
            var order = FindOrder();
            if (order == null) { Console.WriteLine("Khong tim thay don hang."); return; }
            Console.Write("Nhap ma SP: ");
            string pid = Console.ReadLine();
            var product = productBLL.FindProduct(pid);
            if (product == null) { Console.WriteLine("Khong tim thay san pham."); return; }
            Console.Write("Nhap so luong: ");
            int qty = int.Parse(Console.ReadLine());
            if (orderBLL.AddProductToOrder(order, product, qty))
                Console.WriteLine("Them thanh cong.");
        }

        static void RemoveProductFromOrder()
        {
            var order = FindOrder();
            if (order == null) { Console.WriteLine("Khong tim thay don hang."); return; }
            Console.Write("Nhap ma SP can xoa: ");
            string pid = Console.ReadLine();
            if (orderBLL.RemoveProductFromOrder(order, pid))
                Console.WriteLine("Da xoa.");
        }

        static void UpdateProductQuantity()
        {
            var order = FindOrder();
            if (order == null) { Console.WriteLine("Khong tim thay don hang."); return; }
            Console.Write("Nhap ma SP: ");
            string pid = Console.ReadLine();
            Console.Write("Nhap SL moi: ");
            int qty = int.Parse(Console.ReadLine());
            if (orderBLL.UpdateProductQuantity(order, pid, qty))
                Console.WriteLine("Da cap nhat.");
        }

        static void PrintOrderDetails()
        {
            var order = FindOrder();
            if (order != null)
                orderBLL.PrintOrder(order);
        }
    }
}
