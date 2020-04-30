using LibraryManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementApp.Repositories
{
    public class MemberRepository : BaseRepository<Member>
    {
        public override string GetPath()
        {
            return "./../../../../LibraryManagementApp.Repositories/Database/Members.txt";
        }
    }
}
