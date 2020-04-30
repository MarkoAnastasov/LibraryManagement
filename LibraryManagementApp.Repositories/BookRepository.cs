using LibraryManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementApp.Repositories
{
    public class BookRepository : BaseRepository<Book>
    {
        public override string GetPath()
        {
            return "./../../../../LibraryManagementApp.Repositories/Database/Books.txt";
        }
    }
}
