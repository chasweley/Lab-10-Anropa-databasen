using Lab_10_Anropa_databasen.Data;
using Lab_10_Anropa_databasen.Models;
using Lab_10_Anropa_databasen.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Lab_10_Anropa_databasen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (NorthContext context = new NorthContext())
            {
                Menu.StartMenu(context);
            }
        }
    }
}