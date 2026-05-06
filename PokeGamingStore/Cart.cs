using System;
using System.Collections.Generic;
using System.Text;

namespace PokeGamingStore
{
    internal class Cart
    {
        private Dictionary<string, int> items = new Dictionary<string, int>();
        private StockManager stockManager;
        private int maxItems;

        public Cart(StockManager manager, int maxCartItems)
        {
            stockManager = manager;
            maxItems = maxCartItems;
        }

        public void AddToCart(Item item, int quantity)
        {
            int currentTotal = GetTotalItems();
            if (currentTotal + quantity > maxItems)
            {
                throw new InvalidOperationException("Kapasitas keranjang penuh.");
            }

            stockManager.ReduceStock(item.Id, quantity);

            if (items.ContainsKey(item.Id))
            {
                items[item.Id] += quantity;
            }
            else
            {
                items.Add(item.Id, quantity);
            }
        }

        public void RemoveFromCart(Item item, int quantity)
        {
            if (!items.ContainsKey(item.Id))
            {
                throw new InvalidOperationException("Item tidak ditemukan di keranjang.");
            }
            if (items[item.Id] < quantity)
            {
                throw new InvalidOperationException("Kuantitas hapus terlalu banyak.");
            }

            items[item.Id] -= quantity;
            if (items[item.Id] == 0)
            {
                items.Remove(item.Id);
            }

            stockManager.ReturnStock(item.Id, quantity);
        }

        public int GetTotalItems()
        {
            int total = 0;
            foreach (int qty in items.Values)
            {
                total += qty;
            }
            return total;
        }

        public Dictionary<string, int> GetItems()
        {
            return items;
        }

        public void ClearCart()
        {
            items.Clear();
        }
    }
    }
