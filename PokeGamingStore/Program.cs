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
			UbahStatusPesanan(service);
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

static void UbahStatusPesanan(ITransactionService service)
{
	Console.Write("ID pesanan: ");
	var orderIdText = Console.ReadLine();

	if (!Guid.TryParse(orderIdText, out var orderId))
	{
		Console.WriteLine("ID pesanan tidak valid.");
		return;
	}

	Console.WriteLine("Event: Bayar, Kemas, Kirim, Antar, Batal");
	Console.Write("Pilih event: ");
	var eventText = Console.ReadLine();

	if (!Enum.TryParse<EventPesanan>(eventText, ignoreCase: true, out var orderEvent))
	{
		Console.WriteLine("Event tidak valid.");
		return;
	}

	try
	{
		var order = service.TerapkanEvent(orderId, orderEvent);
		Console.WriteLine($"Status baru: {order.Status}");
	}
	catch (Exception ex)
	{
		Console.WriteLine($"Gagal ubah status: {ex.Message}");
	}
}