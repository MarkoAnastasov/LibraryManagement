using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagementApp.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public List<int> RentedBooks { get; set; } = new List<int>(0);

        public void PrintInfo()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Member ID: {Id}\n Name: {Name}\n Surname: {Surname}\n Email: {EMail.ToLower()}");
            if(RentedBooks.Count == 0)
            {
                Console.WriteLine(" Member has no rented books!");
            }
            else if (RentedBooks.Count > 0)
            {
                Console.WriteLine($" Rented books ID: {string.Join(",", RentedBooks)}");
            }
            Console.WriteLine("----------------------------------------------------");
        }
    }
}
