using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibraryManagementApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title{ get; set; }
        public int NumberOfCopies { get; set; }
        public List<int> RentedToMembers { get; set; } = new List<int>(0);

        public void PrintInfo()
        {
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine($" Book ID: {Id}\n Title: {Title.ToLower()}\n Number of copies: {NumberOfCopies}");
            if(RentedToMembers.Count > 0)
            {
                Console.WriteLine($" Rented to member Id: {string.Join(", ", RentedToMembers)}");
            }
            else if (RentedToMembers.Count == 0)
            {
                Console.WriteLine(" Book is not rented!");
            }
            Console.WriteLine("----------------------------------------------------");
        }

        public void DecreaseQuantity()
        {
            NumberOfCopies -= 1;
        }

        public void IncreaseQuantity()
        {
            NumberOfCopies += 1;
        }
    }
}
