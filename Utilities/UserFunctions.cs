using Lab_10_Anropa_databasen.Data;
using Lab_10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10_Anropa_databasen.Utilities
{
    internal class UserFunctions
    {
        //Method to print selected info for all customers
        public static void PrintCustomersInfo(List<Customer> listAllCustomers, NorthContext context)
        {
            int i = 1; //Declare new variable to make choice to view a specific customer easier
            for (var j = 0; j < listAllCustomers.Count(); j++)
            {
                var customer = listAllCustomers[j];

                Console.WriteLine($"{i}" +
                    $"\nCompany name: {customer.CompanyName}" +
                    $"\nCountry: {customer.Country}" +
                    $"\nRegion: {customer.Region}" +
                    $"\nPhone: {customer.Phone}" +
                    $"\nNumber of orders: {customer.Orders.Count}");
                Console.WriteLine();
                i++;
            }
            PrintAllCustomerInfo(listAllCustomers, context);
        }

        //Method to print all info in customer + all orders for that customer
        public static void PrintAllCustomerInfo(List<Customer> listAllCustomerInfo, NorthContext context)
        {
            while (true)
            {
                Console.Write("Write the number for the customer you want more information on: ");
                string input = Console.ReadLine(); 

                if (int.TryParse(input, out int chooseCustomer)) //Takes customer input and checks i able to conver to int, if yes, then converting to int
                {
                    var customer = listAllCustomerInfo[chooseCustomer-1];
                    //Prints all info on chosen customer
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
                    break;
                }
                else
                {
                    Helpers.InvalidInput();
                }
            }
        }
        //Method
        public static void ChooseHowPrintAllCustomerInfo(NorthContext context)
        {
            bool keepTrying = true;

            while (keepTrying)
            {
                //User gets to choose how customers are to be ordered
                Console.Write("Do you want to show the customers in ascending (1) or descending (2) order? ");
                string customerListOrder = Console.ReadLine(); //Converts input to int

                Console.Clear(); //Clears console

                if (customerListOrder == "1") //If-statement to print the list in chosen order
                {
                    Helpers.CustomersByAscendingOrder(context);
                    keepTrying = false;
                }
                else if (customerListOrder == "2")
                {
                    Helpers.CustomersByDescendingOrder(context);
                    keepTrying = false;
                }
                else
                {
                    Helpers.InvalidInput();
                }
            }
        }

        //Method to create new customer in the database
        public static void CreateNewCustomer(NorthContext context)
        {
            Console.Clear();
            string[] columnsCustomerInfoArray =
            {
                    "Company name",
                    "Contact person's name",
                    "Contact person's title",
                    "Address",
                    "City",
                    "Region",
                    "Postal code",
                    "Country",
                    "Phone",
                    "Fax"
                };
            string[] newCustomerInfoArray = new string[columnsCustomerInfoArray.Length]; //Create array

            Console.WriteLine("Enter customer details below:");

            for (int i = 0; i < columnsCustomerInfoArray.Length; i++)
            {
                Console.Write($"{columnsCustomerInfoArray[i]}: ");
                newCustomerInfoArray[i] = Console.ReadLine();
            }

            for (int i = 0; i < 10; i++) //Loop to change empty string in array to null
            {
                if (newCustomerInfoArray[i] == "")
                {
                    newCustomerInfoArray[i] = null;
                }
            }

            var allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //Characters allowed in CustomerId
            var maxNoCharacters = new char[5]; //Array for number of characters in CustomerId
            var random = new Random();
            string customerId = null;

            bool existingCustomerId = true;

            while (existingCustomerId)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < maxNoCharacters.Length; j++) //Loop to randomly select allowed chararcters with the length of max number of characters
                    {
                        maxNoCharacters[j] = allowedCharacters[random.Next(allowedCharacters.Length)];
                    }
                    customerId = new String(maxNoCharacters); //Convert the created array to string
                }

                existingCustomerId = context.Customers.Any(p => p.CustomerId.Equals(customerId));
            }

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
