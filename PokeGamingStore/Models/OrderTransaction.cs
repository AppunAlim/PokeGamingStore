namespace PokeGamingStore.Models;

public sealed class OrderTransaction
{
    public Guid Id { get; }
    public string CustomerId { get; }
    public decimal Amount { get; }
    public StatusPesanan Status { get; private set; }
    public DateTime CreatedAtUtc { get; }

    public OrderTransaction(Guid id, string customerId, decimal amount)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("ID pesanan tidak boleh kosong.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(customerId))
        {
            throw new ArgumentException("ID pelanggan wajib diisi.", nameof(customerId));
        }

        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Jumlah harus lebih besar dari nol.");
        }

        Id = id;
        CustomerId = customerId;
        Amount = amount;
        Status = StatusPesanan.Dibuat;
        CreatedAtUtc = DateTime.UtcNow;
    }

    public void TetapkanStatus(StatusPesanan newStatus)
    {
        Status = newStatus;
    }
}
