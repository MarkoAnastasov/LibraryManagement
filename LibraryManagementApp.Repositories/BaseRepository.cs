using LibraryManagementApp.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LibraryManagementApp.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
    {
        private List<T> Entities { get; set; }
        public BaseRepository()
        {
            var entities = JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(GetPath()));
            if (entities == null)
            {
                entities = new List<T>();
            }
            Entities = entities;
        }
        public abstract string GetPath();
        public void Add(T newEntity)
        {
            Entities.Add(newEntity);
        }
        public T GetFirstWhere(Func<T, bool> predicate)
        {
            return Entities.FirstOrDefault(predicate);
        }
        public List<T> GetAll()
        {
            return Entities;
        }
        public List<T> GetWhere(Func<T, bool> predicate)
        {
            return Entities.Where(predicate).ToList();
        }
        public void RemoveFirstWhere(Func<T, bool> predicate)
        {
            var entity = Entities.FirstOrDefault(predicate);
            if (entity != null)
            {
                Entities.Remove(entity);
            }
        }
        public void RemoveAllWhere(Func<T, bool> predicate)
        {
            var entities = Entities.Where(predicate);
            if (entities.Any())
            {
                foreach (var entity in entities)
                {
                    Entities.Remove(entity);
                }
            }
        }
        public void SaveEntities()
        {
            var entities = JsonConvert.SerializeObject(Entities);
            File.WriteAllText(GetPath(), entities);
        }
    }
}
