using PokeGamingStore.Models;

namespace PokeGamingStore.Services;

public interface ITransactionService
{
    OrderTransaction BuatTransaksi(string customerId, decimal amount);
    OrderTransaction AmbilBerdasarkanId(Guid orderId);
    IReadOnlyCollection<OrderTransaction> AmbilSemua();
    OrderTransaction TerapkanEvent(Guid orderId, EventPesanan orderEvent);
}
