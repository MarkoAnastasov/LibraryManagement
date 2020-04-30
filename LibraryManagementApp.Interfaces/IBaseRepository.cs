using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementApp.Interfaces
{
    public interface IBaseRepository<T>
    {
        abstract string GetPath();

        void Add(T newEntity);

        T GetFirstWhere(Func<T, bool> predicate);

        List<T> GetAll();

        List<T> GetWhere(Func<T, bool> predicate);

        void RemoveFirstWhere(Func<T, bool> predicate);

        void RemoveAllWhere(Func<T, bool> predicate);

        void SaveEntities();
    }
}
