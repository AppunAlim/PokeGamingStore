using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGamingStore
{
    internal class Cart
    {
        private List<Item> items = new List<Item>();
        private StockManager stockManager;
        private int maxItems;

        public Cart(StockManager manager, int maxCartItems)
        {
            if (manager == null)
            {
                throw new ArgumentNullException("Stock manager tidak boleh kosong.");
            }
            if (maxCartItems <= 0)
            {
                throw new ArgumentException("Batas maksimal keranjang harus lebih dari 0.");
            }

            stockManager = manager;
            maxItems = maxCartItems;
        }

        public void AddToCart(Item item, int quantity)
        {
            if (item == null)
            {
                throw new ArgumentNullException("Item tidak valid.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Kuantitas harus lebih dari 0.");
            }
            if (items.Count + quantity > maxItems)
            {
                throw new InvalidOperationException("Kapasitas keranjang penuh.");
            }

            int currentStock = stockManager.GetStock(item.Id);
            if (currentStock < quantity)
            {
                throw new InvalidOperationException("Stok barang tidak cukup.");
            }

            for (int i = 0; i < quantity; i++)
            {
                items.Add(item);
            }

            stockManager.ReduceStock(item.Id, quantity);
        }

        public List<Item> GetItems()
        {
            return items;
        }
    }
}
