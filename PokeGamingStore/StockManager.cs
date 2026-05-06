using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGamingStore
{
    internal class StockManager
    {
        private Dictionary<string, int> stocks = new Dictionary<string, int>();

        public void AddStock(string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException("Id item tidak boleh kosong.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Kuantitas tambah stok harus lebih dari 0.");
            }

            if (stocks.ContainsKey(itemId))
            {
                stocks[itemId] += quantity;
            }
            else
            {
                stocks.Add(itemId, quantity);
            }
        }

        public int GetStock(string itemId)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException("Id item tidak boleh kosong.");
            }
            return stocks.ContainsKey(itemId) ? stocks[itemId] : 0;
        }

        public void ReduceStock(string itemId, int quantity)
        {
            if (string.IsNullOrEmpty(itemId))
            {
                throw new ArgumentNullException("Id item tidak boleh kosong.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Kuantitas pengurangan stok harus lebih dari 0.");
            }
            if (GetStock(itemId) < quantity)
            {
                throw new InvalidOperationException("Stok tidak mencukupi untuk dikurangi.");
            }

            stocks[itemId] -= quantity;
        }
    }
}
