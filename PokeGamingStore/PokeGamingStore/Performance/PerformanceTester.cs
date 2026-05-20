using PokeGamingStore.Catalog;
using System;
using System.Diagnostics;

namespace PokeGamingStore.Performance
{
    public class PerformanceTester
    {
        public static void RunTest()
        {
            var catalog = ProductCatalog<Product>.Instance;
            Console.WriteLine("Memasukkan 100.000 data produk untuk Performance Testing...");

            for (int i = 0; i < 100000; i++)
            {
                catalog.AddProduct(new Product
                {
                    Id = $"ID{i}",
                    Name = $"GameTitle {i}",
                    Category = (i % 2 == 0) ? "RPG" : "Action"
                });
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var results = catalog.SearchProduct("Category", "RPG");

            stopwatch.Stop();
            Console.WriteLine($"Pencarian selesai. Ditemukan {results.Count} produk RPG.");
            Console.WriteLine($"Waktu eksekusi pencarian: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}