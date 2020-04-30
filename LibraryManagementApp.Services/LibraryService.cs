using LibraryManagementApp.Repositories;
using LibraryManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using LibraryManagementApp.Common.Exceptions;
using System.Linq;

namespace LibraryManagementApp.Services
{
    public class LibraryService
    {
        public BookRepository BookRepository { get; set; }
        public MemberRepository MemberRepository { get; set; }

        public LibraryService()
        {
            BookRepository = new BookRepository();
            MemberRepository = new MemberRepository();
        }

        public void RentBook()
        {
            Console.WriteLine("Please enter member Id:");
            var checkMember = int.TryParse(Console.ReadLine(), out int memberId);
            CheckInput(checkMember);
            var getMember = MemberRepository.GetFirstWhere(x => x.Id == memberId && x.RentedBooks.Count < 3);
            if(getMember == null)
            {
                throw new FlowException("Member not found!(Or member has more than 3 rented books!)");
            }
            ShowAllBooks();
            Console.WriteLine("Please choose book ID from above:");
            var checkBook = int.TryParse(Console.ReadLine(), out int bookId);
            var getBook = BookRepository.GetFirstWhere(x => x.Id == bookId);
            if(getBook == null)
            {
                throw new FlowException("Book not found!");
            }
            else if (getBook.RentedToMembers.Contains(getMember.Id))
            {
                throw new FlowException("Book is already rented to this member!");
            }
            else if (getBook.NumberOfCopies <= 0)
            {
                throw new FlowException("Book is out of stock!");
            }
            getBook.RentedToMembers.Add(getMember.Id);
            getBook.DecreaseQuantity();
            getMember.RentedBooks.Add(getBook.Id);
            BookRepository.SaveEntities();
            MemberRepository.SaveEntities();
            Console.WriteLine("Book is rented successfully!");
        }

        public void CloseRent()
        {
            Console.WriteLine("Please enter member Id:");
            var checkMember = int.TryParse(Console.ReadLine(), out int memberId);
            CheckInput(checkMember);
            var getMember = MemberRepository.GetFirstWhere(x => x.Id == memberId && x.RentedBooks.Count != 0);
            if (getMember == null)
            {
                throw new FlowException("Member not found!(Or member doesn't have rents!");
            }
            Console.WriteLine($" Rented books ID: {string.Join(",",getMember.RentedBooks)}");
            Console.WriteLine("Please enter book ID:");
            var checkBook = int.TryParse(Console.ReadLine(), out int bookId);
            CheckInput(checkBook);
            var getBook = BookRepository.GetFirstWhere(x => x.Id == bookId && x.RentedToMembers.Contains(getMember.Id));
            if(getBook == null)
            {
                throw new Exception("Book not found!");
            }
            getBook.RentedToMembers.Remove(getMember.Id);
            getMember.RentedBooks.Remove(getBook.Id);
            getBook.IncreaseQuantity();
            BookRepository.SaveEntities();
            MemberRepository.SaveEntities();
            Console.WriteLine("Rent successfully closed!");
        }

        public void ShowAllMembers()
        {
            var members = MemberRepository.GetAll();
            members.ForEach(x => x.PrintInfo());
        }

        public void CreateNewMember()
        {
            Console.WriteLine("Please enter member Name:");
            var memberName = Console.ReadLine();
            Console.WriteLine("Please enter member Surname:");
            var memberSurname = Console.ReadLine();
            Console.WriteLine("Please enter member E-mail:");
            var memberEmail = Console.ReadLine().ToUpper();
            var member = MemberRepository.GetFirstWhere(x => x.EMail == memberEmail);
            if(member != null)
            {
                throw new FlowException("Member with this E-mail already exist!");
            }
            var memberId = GenerateMemberId();
            var createMember = new Member()
            {
                Id = memberId,
                Name = memberName,
                Surname = memberSurname,
                EMail = memberEmail
            };
            MemberRepository.Add(createMember);
            MemberRepository.SaveEntities();
            Console.WriteLine($"Member created successfully! His unique ID is {memberId} !");
        }

        public void DeleteMember()
        {
            Console.WriteLine("Please enter member ID:");
            var checkId = int.TryParse(Console.ReadLine(), out int memberId);
            CheckInput(checkId);
            var member = MemberRepository.GetFirstWhere(x => x.Id == memberId && x.RentedBooks.Count == 0);
            if (member == null)
            {
                throw new FlowException("Member not found!(Or member haven't returned books)!");
            }
            MemberRepository.RemoveFirstWhere(x => x.Id == memberId);
            MemberRepository.SaveEntities();
            Console.WriteLine("Member successfully removed!");
        }

        public void ShowAllBooks()
        {
            var getBooks = BookRepository.GetAll();
            getBooks.ForEach(x => x.PrintInfo());
        }

        public void AddBook()
        {
            Console.WriteLine("Please enter book Title:");
            var bookTitle = Console.ReadLine().ToUpper();
            var checkBook = BookRepository.GetFirstWhere(x => x.Title == bookTitle);
            if(checkBook != null)
            {
                throw new FlowException("Book already exist!");
            }
            Console.WriteLine("Please enter number of copies:");
            var checkCopies = int.TryParse(Console.ReadLine(), out int bookCopies);
            CheckInput(checkCopies);
            if (bookCopies <= 0)
            {
                throw new FlowException("Please enter amount above 0");
            }
            var bookId = GenerateBookId();
            var createBook = new Book()
            {
                Id = bookId,
                Title = bookTitle,
                NumberOfCopies = bookCopies
            };
            BookRepository.Add(createBook);
            BookRepository.SaveEntities();
            Console.WriteLine("Book successfully added!");
        }

        public void RemoveBook()
        {
            Console.WriteLine("Please enter Book ID:");
            var checkId = int.TryParse(Console.ReadLine(), out int bookId);
            CheckInput(checkId);
            var book = BookRepository.GetFirstWhere(x => x.Id == bookId && x.RentedToMembers.Count == 0);
            if (book == null)
            {
                throw new FlowException("Book not found!(Or book is still rented to someone!)");
            }
            BookRepository.RemoveFirstWhere(x => x.Id == bookId);
            BookRepository.SaveEntities();
            Console.WriteLine("Book successfully removed!");
        }

        public void PrintRentedBooks()
        {
            Console.WriteLine("Rented Books:");
            var rentedBooks = BookRepository.GetWhere(x => x.RentedToMembers.Count != 0);
            rentedBooks.ForEach(x => x.PrintInfo());
        }

        public void EditQuantity()
        {
            Console.WriteLine("Please enter Book ID:");
            var checkId = int.TryParse(Console.ReadLine(), out int bookId);
            CheckInput(checkId);
            var book = BookRepository.GetFirstWhere(x => x.Id == bookId);
            if (book == null)
            {
                throw new FlowException("Book not found!");
            }
            Console.WriteLine("Enter new quantity:");
            var checkQuantity = int.TryParse(Console.ReadLine(), out int bookQuantity);
            CheckInput(checkQuantity);
            if (bookQuantity <= 0)
            {
                throw new FlowException("Please enter quantity above 0!");
            }
            book.NumberOfCopies = bookQuantity;
            BookRepository.SaveEntities();
            Console.WriteLine("Quantity changed successfully!");
        }

        private int GenerateMemberId()
        {
            var allTickets = MemberRepository.GetAll();
            var maxId = 0;
            if (allTickets.Any())
            {
                maxId = allTickets.Max(x => x.Id);
            }
            maxId += 1;
            return maxId;
        }

        private int GenerateBookId()
        {
            var allTickets = BookRepository.GetAll();
            var maxId = 0;
            if (allTickets.Any())
            {
                maxId = allTickets.Max(x => x.Id);
            }
            maxId += 1;
            return maxId;
        }

        private static void CheckInput(bool input)
        {
            if (input == false)
            {
                throw new FlowException("Input is invalid!Please enter a number!");
            }
        }
    }
}
