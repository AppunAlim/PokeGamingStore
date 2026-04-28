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
			BuatPesanan(service);
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


static void BuatPesanan(ITransactionService service)
{
	Console.Write("ID pelanggan: ");
	var customerId = Console.ReadLine() ?? string.Empty;

	Console.Write("Jumlah: ");
	var amountText = Console.ReadLine();

	if (!decimal.TryParse(amountText, out var amount))
	{
		Console.WriteLine("Jumlah tidak valid.");
		return;
	}

	try
	{
		var order = service.BuatTransaksi(customerId, amount);
		Console.WriteLine($"Transaksi dibuat. ID pesanan: {order.Id}, Status: {order.Status}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Gagal buat transaksi: {ex.Message}");
	}
}