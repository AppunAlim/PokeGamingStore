using PokeGamingStore.Catalog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGamingStore.catalog
{
    public class ProductCatalog<T> where T : Product
    {
        private readonly List<T> items;
        private readonly Dictionary<string, Func<T, string, bool>> searchStrategies;

        public ProductCatalog()
        {
            items = new List<T>();

            searchStrategies = new Dictionary<string, Func<T, string, bool>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Name", (item, keyword) => item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) },
                { "Category", (item, keyword) => item.Category.Equals(keyword, StringComparison.OrdinalIgnoreCase) },
                { "Id", (item, keyword) => item.Id.Equals(keyword, StringComparison.OrdinalIgnoreCase) }
            };
        }

        public void AddProduct(T item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item), "Produk tidak boleh null.");
            if (string.IsNullOrWhiteSpace(item.Id))
                throw new ArgumentException("ID Produk harus diisi valid.", nameof(item));

            items.Add(item);
        }

        public List<T> SearchProduct(string searchField, string keyword)
        {
            if (string.IsNullOrWhiteSpace(searchField))
                throw new ArgumentException("Kriteria pencarian tidak boleh kosong.");
            if (string.IsNullOrWhiteSpace(keyword))
                throw new ArgumentException("Keyword pencarian tidak boleh kosong.");

            if (searchStrategies.TryGetValue(searchField, out var strategy))
            {
                return items.Where(item => strategy(item, keyword)).ToList();
            }
            else
            {
                throw new KeyNotFoundException($"Kriteria pencarian '{searchField}' tidak didukung.");
            }
        }

        public int GetTotalProducts() => items.Count;
    }
}
