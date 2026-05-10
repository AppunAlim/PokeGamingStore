using System;
using PokeGamingStore.Performance;

Console.WriteLine("=== Selamat Datang di Poke Gaming Store ===");
Console.WriteLine("Menjalankan Performance Testing untuk Modul Katalog...\n");

// Memanggil class Performance
PerformanceTester.RunTest();

Console.ReadLine();