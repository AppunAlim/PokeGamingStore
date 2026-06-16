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
            if (item == null)
            {
                throw new ArgumentNullException("Item tidak boleh kosong.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Jumlah item yang ditambahkan harus lebih dari 0.");
            }

            // Menggunakan nama variabel lama yang sudah disesuaikan
            int currentStock = stockManager.GetStock(item.Id);

            if (currentStock < quantity)
            {
                throw new InvalidOperationException($"Gagal menambah ke keranjang! Stok untuk {item.Name} tidak mencukupi (Sisa Stok: {currentStock}).");
            }

            if (GetTotalItems() + quantity > maxItems)
            {
                throw new InvalidOperationException("Keranjang sudah penuh! Tidak dapat menambahkan item lagi.");
            }

            if (items.ContainsKey(item.Id))
            {
                items[item.Id] += quantity;
            }
            else
            {
                items.Add(item.Id, quantity);
            }

            // Menggunakan stockManager lama kamu untuk mengurangi stok
            stockManager.ReduceStock(item.Id, quantity);
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
