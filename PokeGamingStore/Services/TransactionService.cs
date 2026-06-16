using PokeGamingStore.Models;

namespace PokeGamingStore.Services;

public sealed class TransactionService : ITransactionService
{
    private readonly Dictionary<Guid, OrderTransaction> _orders = new();

    private static readonly IReadOnlyDictionary<(StatusPesanan Status, EventPesanan Event), StatusPesanan> Transitions =
        new Dictionary<(StatusPesanan Status, EventPesanan Event), StatusPesanan>
        {
            { (StatusPesanan.Dibuat, EventPesanan.Bayar), StatusPesanan.Dibayar },
            { (StatusPesanan.Dibuat, EventPesanan.Batal), StatusPesanan.Dibatalkan },
            { (StatusPesanan.Dibayar, EventPesanan.Kemas), StatusPesanan.Dikemas },
            { (StatusPesanan.Dibayar, EventPesanan.Batal), StatusPesanan.Dibatalkan },
            { (StatusPesanan.Dikemas, EventPesanan.Kirim), StatusPesanan.Dikirim },
            { (StatusPesanan.Dikirim, EventPesanan.Selesai), StatusPesanan.Diterima }
        };

    public OrderTransaction BuatTransaksi(string customerId, decimal amount)
    {
        var order = new OrderTransaction(Guid.NewGuid(), customerId, amount);
        _orders.Add(order.Id, order);
        return order;
    }

    public OrderTransaction AmbilBerdasarkanId(Guid orderId)
    {
        if (orderId == Guid.Empty)
        {
            throw new ArgumentException("ID pesanan tidak boleh kosong.", nameof(orderId));
        }

        if (!_orders.TryGetValue(orderId, out var order))
        {
            throw new KeyNotFoundException($"Pesanan dengan id '{orderId}' tidak ditemukan.");
        }

        return order;
    }

    public IReadOnlyCollection<OrderTransaction> AmbilSemua()
    {
        return _orders.Values.ToArray();
    }

    public OrderTransaction TerapkanEvent(Guid orderId, EventPesanan orderEvent)
    {
        var order = AmbilBerdasarkanId(orderId);

        if (!Transitions.TryGetValue((order.Status, orderEvent), out var nextStatus))
        {
            throw new InvalidOperationException(
                $"Transisi tidak valid dari '{order.Status}' dengan event '{orderEvent}'.");
        }

        order.TetapkanStatus(nextStatus);
        return order;
    }
}
