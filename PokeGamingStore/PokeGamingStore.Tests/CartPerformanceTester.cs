using PokeGamingStore;
using System;
using System.Diagnostics;

namespace PokeGamingStore.Performance
{
    public class CartPerformanceTester
    {
        public static void RunTest()
        {
            StockManager manager = new StockManager();
            Cart cart = new Cart(manager, 100005);
            Item item = new Item { Id = "I001" };
            manager.AddCatalogItem(item, 100000);

            Console.WriteLine("Memulai Performance Testing untuk Cart AddToCart...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 100000; i++)
            {
                cart.AddToCart(item, 1);
            }

            stopwatch.Stop();
            Console.WriteLine("Penambahan 100000 item selesai.");
            Console.WriteLine("Waktu eksekusi AddToCart: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}