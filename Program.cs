using Lab_10_Anropa_databasen.Data;
using Lab_10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_10_Anropa_databasen
{
    internal class Program
    {
        static void Main(string[] args)
        {

            using (NorthContext context = new NorthContext())
            {
                Console.WriteLine("Choose what you want to do: ");
                Console.WriteLine("1 Show all customers");
                Console.WriteLine("2 Add customer");
                Console.WriteLine("3 Quit");

                int input = int.Parse(Console.ReadLine());

                switch (input)
                { 
                    case 1:
                        Console.Write("Do you want to show the customers in ascending (1) or descending (2) order? ");
                        int customerListOrder = int.Parse(Console.ReadLine());

                            if (customerListOrder == 1)
                            {
                                Console.Clear();
                                var listCustomerInfo = context.Customers
                                    .Include(c => c.Orders)
                                    .OrderBy(c => c.CompanyName)
                                    .ToList();
                                PrintCustomersInfo(listCustomerInfo);
                                PrintAllCustomerInfo(listCustomerInfo, context);
                            }
                            else if (customerListOrder == 2)
                            {
                                Console.Clear();
                                var listCustomerInfo = context.Customers
                                    .Include(c => c.Orders)
                                    .OrderByDescending(c => c.CompanyName)
                                    .ToList();
                                PrintCustomersInfo(listCustomerInfo);
                                PrintAllCustomerInfo(listCustomerInfo, context);
                            }
                            //else
                            //{
                            //    Console.Write("Invalid input, try again: ");
                            //}
                        break;
                    case 2:
                        CreateNewCustomer(context);
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default: 
                        Console.WriteLine("Invalid input, try again.");
                        input = int.Parse(Console.ReadLine());
                        break;
                }
            }
        }

        static void PrintCustomersInfo(IEnumerable<Customer> customers)
        {
            int i = 1;
            for (var j = 0; j < customers.Count(); j++)
            {
                var customer = customers.ElementAt(j);

                Console.WriteLine($"{i}" +
                    $"\nCompany name: {customer.CompanyName}" +
                    $"\nCountry: {customer.Country}" +
                    $"\nRegion: {customer.Region}" +
                    $"\nPhone: {customer.Phone}" +
                    $"\nNumber of orders: {customer.Orders.Count()}");
                Console.WriteLine();
                i++;
            }
        }

        static void PrintAllCustomerInfo(List<Customer> listAllCustomerInfo, NorthContext context)
        {
            //var listAllCustomerInfo = context.Customers
            //    .ToList();

            Console.Write("Write the number for the customer you want more information on: ");
            int chooseCustomer = int.Parse(Console.ReadLine()) - 1;
            var customer = listAllCustomerInfo[chooseCustomer];

            Console.WriteLine("\nAll customer information:");
            Console.WriteLine($"Company name: {customer.CompanyName}" +
                $"\nContact person's name: {customer.ContactName}" +
                $"\nContact person's title: {customer.ContactTitle}" +
                $"\nAddress: {customer.Address}" +
                $"\nCity: {customer.City}" +
                $"\nRegion: {customer.Region}" +
                $"\nPostal code: {customer.PostalCode}" +
                $"\nCountry: {customer.Country}" +
                $"\nPhone: {customer.Phone}" +
                $"\nFax: {customer.Fax}");

                var allCustomerOrders = context.Orders
                    .Where(o => o.CustomerId == customer.CustomerId)
                    .Select(o => new
                    {
                        o.OrderId,
                        o.OrderDate
                    })
                    .OrderBy(o => o.OrderDate)
                    .ToList();

                Console.WriteLine("\nCustomers orders:");
                foreach (var order in allCustomerOrders)
                {
                    Console.WriteLine($"Order ID: {order.OrderId}" +
                        $"\nOrder date: {order.OrderDate}");
                    Console.WriteLine();
                }
        }

        static void CreateNewCustomer(NorthContext context)
        {
            string[] newCustomerInfoArray = new string[9];

            Console.Clear();
            Console.WriteLine("Enter customer details below:");

            Console.Write("Company name: ");
            newCustomerInfoArray[0] = Console.ReadLine();

            Console.Write("Contact person's name: ");
            newCustomerInfoArray[1] = Console.ReadLine();

            Console.Write("Contact person's title: ");
            newCustomerInfoArray[2] = Console.ReadLine();

            Console.Write("Address: ");
            newCustomerInfoArray[3] = Console.ReadLine();

            Console.Write("City: ");
            newCustomerInfoArray[4] = Console.ReadLine();

            Console.Write("Region: ");
            newCustomerInfoArray[5] = Console.ReadLine();

            Console.Write("Postal code: ");
            newCustomerInfoArray[6] = Console.ReadLine();

            Console.Write("Country: ");
            newCustomerInfoArray[7] = Console.ReadLine();

            Console.Write("Phone: ");
            newCustomerInfoArray[8] = Console.ReadLine();

            Console.Write("Fax: ");
            newCustomerInfoArray[9] = Console.ReadLine();

            var allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var maxNoCharacters = new char[5];
            var random = new Random();

            for (int i = 0; i < maxNoCharacters.Length; i++)
            {
                maxNoCharacters[i] = allowedCharacters[random.Next(allowedCharacters.Length)];
            }

            string customerId = new String(maxNoCharacters);

            var newCustomer = new Customer
            {
                CustomerId = customerId,
                CompanyName = newCustomerInfoArray[0],
                ContactName = newCustomerInfoArray[1],
                ContactTitle = newCustomerInfoArray[2],
                Address = newCustomerInfoArray[3],
                City = newCustomerInfoArray[4],
                Region = newCustomerInfoArray[5],
                PostalCode = newCustomerInfoArray[6],
                Country = newCustomerInfoArray[7],
                Phone = newCustomerInfoArray[8],
                Fax = newCustomerInfoArray[9]
            };

            for (int i = 0; i < 9; i++)
            {
                newCustomerInfoArray[i] = string.IsNullOrEmpty(newCustomerInfoArray[i]) ? newCustomerInfoArray[i] : null;
            }

            context.Customers.Add(newCustomer);
            context.SaveChanges();

            Console.WriteLine("Success! New customer added.");
        }
    }
}