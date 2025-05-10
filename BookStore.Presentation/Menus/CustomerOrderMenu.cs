using BookStore.Application.DTOs.Requests;
using BookStore.Application.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace BookStore.Presentation.Menus
{
    public class CustomerOrderMenu
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;

        public CustomerOrderMenu(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("*** Customers and Orders Menu ***");
                Console.WriteLine("1. Register New Customer");
                Console.WriteLine("2. List Customers");
                Console.WriteLine("3. Create New Order");
                Console.WriteLine("4. View Customer Orders");
                Console.WriteLine("5. View Order Details");
                Console.WriteLine("6. Edit Customer");
                Console.WriteLine("7. Delete Customer");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddCustomer();
                            break;
                        case "2":
                            ListCustomers();
                            break;
                        case "3":
                            CreateOrder();
                            break;
                        case "4":
                            ViewCustomerOrders();
                            break;
                        case "5":
                            ViewOrderDetails();
                            break;
                        case "6":
                            EditCustomer();
                            break;
                        case "7":
                            DeleteCustomer();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Invalid option. Press any key to continue...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ReadKey();
                }
            }
        }

        private void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("*** Register New Customer ***");
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Email: ");
            string email = Console.ReadLine();

            var customerDto = new CustomerRequestDto { Name = name, Email = email };
            _customerService.AddCustomer(customerDto);
            Console.WriteLine("Customer registered successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void ListCustomers()
        {
            Console.Clear();
            Console.WriteLine("*** Customer List ***");
            var customers = _customerService.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.Id}, Name: {customer.Name}, Email: {customer.Email}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void CreateOrder()
        {
            Console.Clear();
            Console.WriteLine("*** Create New Order ***");
            Console.Write("Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            var orderDetails = new List<OrderDetailRequestDto>();

            while (true)
            {
                Console.Write("Add book to order? (y/n): ");
                if (Console.ReadLine().ToLower() != "y")
                    break;

                Console.Write("Book ID: ");
                int bookId = int.Parse(Console.ReadLine());
                Console.Write("Quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                orderDetails.Add(new OrderDetailRequestDto { BookId = bookId, Quantity = quantity });
            }

            var orderDto = new OrderRequestDto
            {
                CustomerId = customerId,
                OrderDetails = orderDetails
            };

            _orderService.CreateOrder(orderDto);
            Console.WriteLine("Order created successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewCustomerOrders()
        {
            Console.Clear();
            Console.WriteLine("*** View Customer Orders ***");
            Console.Write("Enter Customer ID: ");
            int customerId = int.Parse(Console.ReadLine());
            var orders = _orderService.GetOrdersByCustomer(customerId);
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Date: {order.OrderDate}, Total: {order.TotalAmount}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ViewOrderDetails()
        {
            Console.Clear();
            Console.WriteLine("*** View Order Details ***");
            Console.Write("Enter Order ID: ");
            int orderId = int.Parse(Console.ReadLine());
            var order = _orderService.GetOrderById(orderId);
            Console.WriteLine($"Order ID: {order.Id}, Customer: {order.CustomerName}, Date: {order.OrderDate}, Total: {order.TotalAmount}");
            Console.WriteLine("Books:");
            foreach (var detail in order.OrderDetails)
            {
                Console.WriteLine($" - Book: {detail.BookTitle}, Quantity: {detail.Quantity}, Price: {detail.UnitPrice}");
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void EditCustomer()
        {
            Console.Clear();
            Console.WriteLine("*** Edit Customer ***");
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());
            var customer = _customerService.GetCustomerById(id);
            Console.WriteLine($"Current Name: {customer.Name}");
            Console.Write("New Name: ");
            string name = Console.ReadLine();
            Console.WriteLine($"Current Email: {customer.Email}");
            Console.Write("New Email: ");
            string email = Console.ReadLine();

            var customerDto = new CustomerRequestDto { Name = name, Email = email };
            _customerService.UpdateCustomer(id, customerDto);
            Console.WriteLine("Customer updated successfully! Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteCustomer()
        {
            Console.Clear();
            Console.WriteLine("*** Delete Customer ***");
            Console.Write("Enter Customer ID: ");
            int id = int.Parse(Console.ReadLine());
            _customerService.DeleteCustomer(id);
            Console.WriteLine("Customer deleted successfully! Press any key to continue...");
            Console.ReadKey();
        }
    }
}