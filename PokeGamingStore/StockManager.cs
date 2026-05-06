using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGamingStore
{
    internal class StockManager
    {
        private Dictionary<string, int> stocks = new Dictionary<string, int>();
        private List<Item> catalog = new List<Item>();

        public void AddCatalogItem(Item item, int quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item tidak boleh kosong.");
            }
            catalog.Add(item);
            stocks.Add(item.Id, quantity);
        }

        public Item GetItem(string itemId)
        {
            return catalog.FirstOrDefault(i => i.Id == itemId);
        }

        public int GetStock(string itemId)
        {
            return stocks.ContainsKey(itemId) ? stocks[itemId] : 0;
        }

        public void ReduceStock(string itemId, int quantity)
        {
            if (GetStock(itemId) < quantity)
            {
                throw new InvalidOperationException("Stok tidak mencukupi.");
            }
            stocks[itemId] -= quantity;
        }

        public void ReturnStock(string itemId, int quantity)
        {
            if (stocks.ContainsKey(itemId))
            {
                stocks[itemId] += quantity;
            }
        }

        public List<Item> GetCatalog()
        {
            return catalog;
        }
    }
}
