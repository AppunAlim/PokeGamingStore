using PokeGamingStore;
using PokeGamingStore.Models;
using PokeGamingStore.Services;
using System;
using System.Collections.Generic;

var service = new TransactionService();
var running = true;

Console.WriteLine("PokeGamingStore - Sistem Gabungan");

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
            TampilkanPesanan(service);
            break;
        case "4":
            DemoKeranjangDanStok(service);
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
    Console.WriteLine("1. Buat transaksi manual");
    Console.WriteLine("2. Ubah status pesanan (automata)");
    Console.WriteLine("3. Lihat semua pesanan");
    Console.WriteLine("4. Menu Keranjang & Stok Barang");
    Console.WriteLine("0. Keluar");
}

static void DemoKeranjangDanStok(ITransactionService service)
{
    ConfigLoader loader = new ConfigLoader();
    AppConfig config = loader.LoadConfig("config.json");

    StockManager stockManager = new StockManager();
    stockManager.AddCatalogItem(new Item { Id = "G01", Name = "Keyboard Mechanical RGB", Price = 1500000 }, 10);
    stockManager.AddCatalogItem(new Item { Id = "G02", Name = "Mouse Gaming Wireless", Price = 850000 }, 15);
    stockManager.AddCatalogItem(new Item { Id = "G03", Name = "Headset Gaming 7.1", Price = 1200000 }, 8);
    stockManager.AddCatalogItem(new Item { Id = "G04", Name = "Monitor Gaming 144Hz", Price = 3500000 }, 5);

    Cart cart = new Cart(stockManager, config.MaxCartItems);
    bool subRunning = true;

    while (subRunning)
    {
        Console.WriteLine("\n--- Keranjang & Stok Barang ---");
        Console.WriteLine("1. Lihat Katalog Barang");
        Console.WriteLine("2. Tambah Barang ke Keranjang");
        Console.WriteLine("3. Hapus Barang dari Keranjang");
        Console.WriteLine("4. Lihat Isi Keranjang");
        Console.WriteLine("5. Checkout Pesanan");
        Console.WriteLine("0. Kembali ke Menu Utama");
        Console.Write("Pilih aksi: ");
        var aksi = Console.ReadLine();

        switch (aksi)
        {
            case "1":
                Console.WriteLine("\nKatalog Barang:");
                foreach (Item item in stockManager.GetCatalog())
                {
                    Console.WriteLine($"ID: {item.Id} | Nama: {item.Name} | Harga: Rp{item.Price} | Stok: {stockManager.GetStock(item.Id)}");
                }
                break;
            case "2":
                Console.Write("Masukkan ID Barang: ");
                string idTambah = Console.ReadLine();
                Console.Write("Masukkan Jumlah: ");
                if (int.TryParse(Console.ReadLine(), out int qtyTambah))
                {
                    Item itemTambah = stockManager.GetItem(idTambah);
                    if (itemTambah != null)
                    {
                        try
                        {
                            cart.AddToCart(itemTambah, qtyTambah);
                            Console.WriteLine("Berhasil dimasukkan ke keranjang.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Gagal: " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Barang tidak ditemukan.");
                    }
                }
                break;
            case "3":
                Console.Write("Masukkan ID Barang: ");
                string idHapus = Console.ReadLine();
                Console.Write("Masukkan Jumlah yang dihapus: ");
                if (int.TryParse(Console.ReadLine(), out int qtyHapus))
                {
                    Item itemHapus = stockManager.GetItem(idHapus);
                    if (itemHapus != null)
                    {
                        try
                        {
                            cart.RemoveFromCart(itemHapus, qtyHapus);
                            Console.WriteLine("Berhasil dihapus dari keranjang.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Gagal: " + ex.Message);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Barang tidak ditemukan.");
                    }
                }
                break;
            case "4":
                Console.WriteLine("\nIsi Keranjang:");
                var items = cart.GetItems();
                decimal totalHarga = 0;
                if (items.Count == 0)
                {
                    Console.WriteLine("Keranjang kosong.");
                }
                else
                {
                    foreach (var kvp in items)
                    {
                        Item item = stockManager.GetItem(kvp.Key);
                        decimal subTotal = item.Price * kvp.Value;
                        totalHarga += subTotal;
                        Console.WriteLine($"{item.Name} (x{kvp.Value}) - Subtotal: Rp{subTotal}");
                    }
                    Console.WriteLine($"Total Harga Keranjang: Rp{totalHarga}");
                }
                break;
            case "5":
                var checkoutItems = cart.GetItems();
                if (checkoutItems.Count == 0)
                {
                    Console.WriteLine("Keranjang kosong, tidak bisa checkout.");
                    break;
                }
                decimal totalBayar = 0;
                foreach (var kvp in checkoutItems)
                {
                    Item item = stockManager.GetItem(kvp.Key);
                    totalBayar += item.Price * kvp.Value;
                }

                Console.Write("Masukkan ID Pelanggan untuk pesanan: ");
                string custId = Console.ReadLine() ?? "Anonim";

                try
                {
                    var order = service.BuatTransaksi(custId, totalBayar);
                    cart.ClearCart();
                    Console.WriteLine($"Checkout sukses. ID pesanan: {order.Id}, Total Bayar: Rp{totalBayar}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Gagal checkout: " + ex.Message);
                }
                break;
            case "0":
                Console.WriteLine("\n------------------Kembali Ke Menu Utama--------------------");
                subRunning = false;
                break;
            default:
                Console.WriteLine("Pilihan tidak dikenal.");
                break;
        }
    }
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

static void TampilkanPesanan(ITransactionService service)
{
    var orders = service.AmbilSemua();

    if (orders.Count == 0)
    {
        Console.WriteLine("Belum ada transaksi.");
        return;
    }

    foreach (var order in orders)
    {
        Console.WriteLine(
            $"ID pesanan: {order.Id} | Pelanggan: {order.CustomerId} | Jumlah: {order.Amount} | Status: {order.Status}");
    }
}