using PokeGamingStore.Models;
using PokeGamingStore.Services;

var service = new TransactionService();
var running = true;

Console.WriteLine("PokeGamingStore - Transaksi & Status Pesanan");

while (running)
{
	ShowMenu();
	Console.Write("Pilih menu: ");
	var choice = Console.ReadLine();

	switch (choice)
	{
		case "1":
			break;
		case "2":
			break;
		case "3":
			break;
		case "0":
			running = false;
			break;
		default:
			Console.WriteLine("Menu tidak dikenal.");
			break;
	}

	Console.WriteLine();
}

static void ShowMenu()
{
	Console.WriteLine("1. Buat transaksi");
	Console.WriteLine("2. Ubah status pesanan (automata)");
	Console.WriteLine("3. Lihat semua pesanan");
	Console.WriteLine("0. Keluar");
}
