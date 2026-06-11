using System.Collections.Generic;

namespace PokeGamingStore.Services
{
    public interface IGenericRepository<T>
    {
        void Add(T item);
        List<T> GetAll();
    }

    public class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly List<T> _items = new List<T>();
        public void Add(T item) => _items.Add(item);
        public List<T> GetAll() => _items;
    }
}