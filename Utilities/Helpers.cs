using Lab_10_Anropa_databasen.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab_10_Anropa_databasen.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab_10_Anropa_databasen.Utilities
{
    internal class Helpers
    {
        public static bool ContinueToMenu()
        {
            Console.Write("Do you want to return to the main menu (y) or quit (q)? ");
            string continueToMenu = Console.ReadLine().ToUpper();
            if (continueToMenu == "Q")
            {
                return false; //To break out of while-loop and ends program
            }
            else
            {
                return true;
            }
        }

        public static void CustomersByAscendingOrder(NorthContext context)
        {
            var listCustomerInfo = context.Customers //Declaring new list from Customers in database
                .Include(c => c.Orders) //Also includes table orders
                .OrderBy(c => c.CompanyName) //Sorts ascending by CompanyName
                .ToList();
            UserFunctions.PrintCustomersInfo(listCustomerInfo, context); //Sends list to method
        }

        public static void CustomersByDescendingOrder(NorthContext context)
        {
            var listCustomerInfo = context.Customers
                .Include(c => c.Orders)
                .OrderByDescending(c => c.CompanyName) //Sorts descending by CompanyName
                .ToList();
            UserFunctions.PrintCustomersInfo(listCustomerInfo, context);
        }

        public static void InvalidInput()
        {
            Console.Clear();
            Console.WriteLine("Invalid input, try again.");
            Thread.Sleep(2000);
        }
    }
}
