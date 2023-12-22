using Lab_10_Anropa_databasen.Data;
using Lab_10_Anropa_databasen.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_10_Anropa_databasen
{
    internal class Menu
    {
        public static void StartMenu(NorthContext context)
        {
            bool goToMenu = true;

            while (goToMenu) //Loop to keep showing menu choices in case of invalid input
            {
                Console.Clear();

                Console.WriteLine("1 Show all customers"); //Menu choice
                Console.WriteLine("2 Add customer");
                Console.WriteLine("3 Quit");
                Console.Write("Choose what you want to do: ");

                string input = Console.ReadLine();

                switch (input) //Switch to handle user menu choice
                {
                    case "1":
                        UserFunctions.ChooseHowPrintAllCustomerInfo(context);
                        goToMenu = Helpers.ContinueToMenu();
                        break;
                    case "2":
                        UserFunctions.CreateNewCustomer(context); //Sends context to method to create new customer
                        goToMenu = Helpers.ContinueToMenu();
                        break;
                    case "3":
                        return; //Exists the program
                    default:
                        Helpers.InvalidInput(); //States invalid input if user input doesn't match options
                        break;
                }
            }
        }
    }
}
