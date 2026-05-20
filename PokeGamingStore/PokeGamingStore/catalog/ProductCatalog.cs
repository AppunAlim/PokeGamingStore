using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PokeGamingStore.Catalog
{
    public class ProductCatalog<T> where T : Product
    {
        private static ProductCatalog<T> instance;
        public static ProductCatalog<T> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductCatalog<T>();
                }
                return instance;
            }
        }

        private readonly List<T> items;
        private readonly Dictionary<string, Func<T, string, bool>> searchStrategies;

        private ProductCatalog()
        {
            items = new List<T>();
            searchStrategies = new Dictionary<string, Func<T, string, bool>>(StringComparer.OrdinalIgnoreCase)
            {
                { "Name", (item, keyword) => item.Name != null && item.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) },
                { "Category", (item, keyword) => item.Category != null && item.Category.Equals(keyword, StringComparison.OrdinalIgnoreCase) },
                { "Id", (item, keyword) => item.Id != null && item.Id.Equals(keyword, StringComparison.OrdinalIgnoreCase) }
            };

            LoadDummyData();
        }

        private void LoadDummyData()
        {
            if (typeof(T) == typeof(Product))
            {
                var dummyItems = new List<Product>
                {
                    new Product { Id = "P001", Name = "Elden Ring", Category = "Game", Price = 600000 },
                    new Product { Id = "P002", Name = "PS5 Controller", Category = "Accessory", Price = 1200000 },
                    new Product { Id = "P003", Name = "Persona 5 Royal", Category = "RPG", Price = 750000 }
                };

                foreach (var item in dummyItems)
                {
                    items.Add((T)(object)item);
                }
            }
        }

        public string SanitizeInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            string cleanString = Regex.Replace(input, @"[^a-zA-Z0-9\s-]", "");
            return cleanString.Length > 50 ? cleanString.Substring(0, 50) : cleanString;
        }

        public void AddProduct(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            if (string.IsNullOrWhiteSpace(item.Id)) throw new ArgumentException("ID Invalid");
            items.Add(item);
        }

        public List<T> SearchProduct(string searchField, string rawKeyword)
        {
            if (string.IsNullOrWhiteSpace(searchField)) throw new ArgumentException("Field kosong");
            string safeKeyword = SanitizeInput(rawKeyword);
            if (string.IsNullOrWhiteSpace(safeKeyword)) throw new ArgumentException("Keyword invalid");

            if (searchStrategies.TryGetValue(searchField, out var strategy))
            {
                return items.Where(item => strategy(item, safeKeyword)).ToList();
            }
            throw new KeyNotFoundException("Kriteria tidak didukung");
        }

        public List<T> GetAllProducts()
        {
            return items.ToList();
        }
    }
}