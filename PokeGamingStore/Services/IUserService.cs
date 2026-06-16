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

        User ValidateLogin(string username, string password, UserRole role);
        bool RegisterUserWithPassword(string username, string password, UserRole role);
        List<History<PurchaseInfo>> GetAllHistory();
        List<History<PurchaseInfo>> SearchHistory(string query);
    }
}