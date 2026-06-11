using System.Collections.Generic;
using PokeGamingStore.Models;

namespace PokeGamingStore.Services
{
    public interface IUserService
    {
        ApiResponse<User> RegisterUser(string username, UserRole role);
        ApiResponse<List<User>> GetAllUsers();
      
        ApiResponse<List<History<PurchaseInfo>>> GetPurchaseHistory(string userId);

        void RecordPurchase(string userId, string orderId, decimal amount);
    }
}