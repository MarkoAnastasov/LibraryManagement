using LibraryManagementApp.Common.Exceptions;
using LibraryManagementApp.Services;
using System;

namespace LibraryManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var libraryService = new LibraryService();
            bool continueProgram = true;
            while (continueProgram)
            {
                try
                {
                    ShowMenu();
                    int.TryParse(Console.ReadLine(), out int userChoice);
                    switch (userChoice)
                    {
                        case 1:
                            libraryService.RentBook();
                            break;
                        case 2:
                            libraryService.CloseRent();
                            break;
                        case 3:
                            ShowManageMemberMenu(libraryService);
                            break;
                        case 4:
                            ShowManageBookMenu(libraryService);
                            break;
                        default:
                            Console.WriteLine("Option not found!");
                            break;
                    }
                }
                catch (FlowException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("An error has occured!Please try again later!");
                }
                Console.WriteLine("Do you want to continue with program? Enter y if yes!Any other key to exit!");
                var toContinue = Console.ReadLine();
                continueProgram = toContinue.ToLower() == "y";
            }
            
        }

        private static void ShowManageBookMenu(LibraryService libraryService)
        {
            bool manageBooks = true;
            while (manageBooks)
            {
                Console.WriteLine("Please choose the following options:");
                Console.WriteLine(" 1.Show all books\n 2.Add new book\n 3.Delete existing book\n 4.Print all rented books\n 5.Edit number of copies");
                int.TryParse(Console.ReadLine(), out int manageBookOpt);
                switch (manageBookOpt)
                {
                    case 1:
                        libraryService.ShowAllBooks();
                        break;
                    case 2:
                        libraryService.AddBook();
                        break;
                    case 3:
                        libraryService.RemoveBook();
                        break;
                    case 4:
                        libraryService.PrintRentedBooks();
                        break;
                    case 5:
                        libraryService.EditQuantity();
                        break;
                    default:
                        Console.WriteLine("Option not found!");
                        break;
                }
                Console.WriteLine("Do you want to continue managing books? Enter y if yes!Any other key to exit section!");
                var manageBookChoice = Console.ReadLine();
                manageBooks = manageBookChoice.ToLower() == "y";
            }
        }

        private static void ShowManageMemberMenu(LibraryService libraryService)
        {
            bool manageMembers = true;
            while (manageMembers)
            {
                Console.WriteLine("Please choose the following options:");
                Console.WriteLine(" 1.Show all members\n 2.Create new member\n 3.Delete existing member");
                int.TryParse(Console.ReadLine(), out int manageMemberOpt);
                switch (manageMemberOpt)
                {
                    case 1:
                        libraryService.ShowAllMembers();
                        break;
                    case 2:
                        libraryService.CreateNewMember();
                        break;
                    case 3:
                        libraryService.DeleteMember();
                        break;
                    default:
                        Console.WriteLine("Option not found!");
                        break;
                }
                Console.WriteLine("Do you want to continue managing members? Enter y if yes!Any other key to exit section!");
                var manageMemberChoice = Console.ReadLine();
                manageMembers = manageMemberChoice.ToLower() == "y";
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Please choose the following options:");
            Console.WriteLine(" 1.Rent\n 2.Close rent\n 3.Manage members\n 4.Manage book");
        }

    }
}
