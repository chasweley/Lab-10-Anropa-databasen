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
                bool keepTrying = true;

                while (keepTrying) //Loop to keep showing menu choices in case of invalid input
                {
                    Console.WriteLine("Choose what you want to do: "); //Menu choice
                    Console.WriteLine("1 Show all customers");
                    Console.WriteLine("2 Add customer");
                    Console.WriteLine("3 Quit");

                    int input = int.Parse(Console.ReadLine()); //Convert user input to int and saves to variable

                    switch (input) //Switch to handle user menu choice
                    {
                        case 1:
                            //User gets to choose how custumers are to be ordered
                            Console.Write("Do you want to show the customers in ascending (1) or descending (2) order? ");
                            int customerListOrder = int.Parse(Console.ReadLine()); //Converts input to int

                            if (customerListOrder == 1) //If-statement to print the list in chosen order
                            {
                                Console.Clear(); //Clears console
                                var listCustomerInfo = context.Customers //Declaring new list from Customers in database
                                    .Include(c => c.Orders) //Also includes table orders
                                    .OrderBy(c => c.CompanyName) //Sorts ascending by CompanyName
                                    .ToList();
                                PrintCustomersInfo(listCustomerInfo); //Sends list to method
                                PrintAllCustomerInfo(listCustomerInfo, context);//Sends list and context to method
                            }
                            else if (customerListOrder == 2)
                            {
                                Console.Clear();
                                var listCustomerInfo = context.Customers
                                    .Include(c => c.Orders)
                                    .OrderByDescending(c => c.CompanyName) //Sorts descending by CompanyName
                                    .ToList();
                                PrintCustomersInfo(listCustomerInfo);
                                PrintAllCustomerInfo(listCustomerInfo, context);
                            }
                            keepTrying = false; //To break out of loop
                            break;
                        case 2:
                            CreateNewCustomer(context); //Sends context to method to create new customer
                            keepTrying = false;
                            break;
                        case 3:
                            Environment.Exit(0); //Quits the program
                            break;
                        default:
                            Console.Clear();
                            Console.WriteLine("Invalid input, try again."); //States invalid input if user input doesn't match options
                            break;
                    }
                }
            }
        }

        static void PrintCustomersInfo(IEnumerable<Customer> customers)
        {
            //Method to print selected info for all customers
            int i = 1; //Declare new variable to make choice to view a specific customer easier
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
            //Method to print all info in customer + all orders for that customer
            Console.Write("Write the number for the customer you want more information on: ");
            int chooseCustomer = int.Parse(Console.ReadLine()) - 1; //Takes customer input based on number printed (i) in PrintCustomersInfo
            var customer = listAllCustomerInfo[chooseCustomer];

            Console.Clear();
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

                var allCustomerOrders = context.Orders //Declaring new list from Orders in database
                    .Where(o => o.CustomerId == customer.CustomerId)
                    .Select(o => new
                    {
                        o.OrderId,
                        o.OrderDate
                    })
                    .OrderBy(o => o.OrderId)
                    .ToList();

                Console.WriteLine("\nCustomers orders:");
                foreach (var order in allCustomerOrders) //Loop to print out all orders
                {
                    Console.WriteLine($"Order ID: {order.OrderId}" +
                        $"\nOrder date: {order.OrderDate}");
                    Console.WriteLine();
                }
        }

        static void CreateNewCustomer(NorthContext context)
        {
            //Method to create new customer in the database
            string[] newCustomerInfoArray = new string[10]; //Create array

            Console.Clear();
            Console.WriteLine("Enter customer details below:");

            Console.Write("Company name: "); //Input for each column put in a certain place in the array
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

            for (int i = 0; i < 10; i++) //Loop to change empty string in array to null
            {
                newCustomerInfoArray[i] = newCustomerInfoArray[i] == "" ? newCustomerInfoArray[i] : null;
            }

            var allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //Characters allowed in CustomerId
            var maxNoCharacters = new char[5]; //Array for number of characters in CustomerId
            var random = new Random();

            for (int i = 0; i < maxNoCharacters.Length; i++) //Loop to randomly select allowed chararcters with the length of max number of characters
            {
                maxNoCharacters[i] = allowedCharacters[random.Next(allowedCharacters.Length)];
            }
            string customerId = new String(maxNoCharacters); //Convert the created array to string

            var newCustomer = new Customer //Insert all the new values to new customer
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

            context.Customers.Add(newCustomer); //Add to database
            context.SaveChanges(); //Saves the changes

            Console.WriteLine("Success! New customer added.");
        }
    }
}