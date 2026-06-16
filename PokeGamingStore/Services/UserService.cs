using System;
using System.Collections.Generic;
using System.Linq;
using PokeGamingStore.Models;

namespace PokeGamingStore.Services
{
    public class UserService : IUserService
    {
        private static readonly IGenericRepository<User> _userRepo = new GenericRepository<User>();
        private static readonly IGenericRepository<History<PurchaseInfo>> _historyRepo = new GenericRepository<History<PurchaseInfo>>();

        public User ValidateLogin(string username, string password, UserRole role)
        {
            return _userRepo.GetAll().FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password &&
                u.Role == role);
        }

        public bool RegisterUserWithPassword(string username, string password, UserRole role)
        {
            if (_userRepo.GetAll().Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase))) return false;

            // Membuat suffix acak unik sepanjang 5 karakter hex/string
            string userSuffix = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            string custSuffix = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();

            var user = new User
            {
                Id = "USR-" + userSuffix,
                Username = username,
                Password = password,
                Role = role,
                // Jika Pelanggan maka CUST-XXXXX, jika Admin biarkan NULL
                CustomerId = (role == UserRole.Regular) ? "CUST-" + custSuffix : null
            };

            _userRepo.Add(user);
            return true;
        }

        public List<History<PurchaseInfo>> GetAllHistory() => _historyRepo.GetAll();

        public List<History<PurchaseInfo>> SearchHistory(string query)
        {
            var allHistory = _historyRepo.GetAll();
            if (string.IsNullOrWhiteSpace(query)) return allHistory;

            //Pencarian histori dengan membaca ID_Pelanggan (yang disimpan pada h.UserId)
            return allHistory.Where(h =>
                (h.Data != null && h.Data.OrderId != null && h.Data.OrderId.Equals(query, StringComparison.OrdinalIgnoreCase)) ||
                (h.UserId != null && h.UserId.Equals(query, StringComparison.OrdinalIgnoreCase))
            ).ToList();
        }

        public ApiResponse<User> RegisterUser(string username, UserRole role)
        {
            string userSuffix = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();
            string custSuffix = Guid.NewGuid().ToString().Substring(0, 5).ToUpper();

            var user = new User
            {
                Id = "USR-" + userSuffix,
                Username = username,
                Password = "123",
                Role = role,
                CustomerId = (role == UserRole.Regular) ? "CUST-" + custSuffix : null
            };
            _userRepo.Add(user);
            return new ApiResponse<User> { Success = true, Message = "User berhasil terdaftar.", Data = user };
        }

        public ApiResponse<List<User>> GetAllUsers()
        {
            return new ApiResponse<List<User>> { Success = true, Message = "Daftar user berhasil ditarik.", Data = _userRepo.GetAll() };
        }

        public void RecordPurchase(string customerId, string orderId, decimal amount)
        {
            _historyRepo.Add(new History<PurchaseInfo>
            {
                LogId = "LOG-" + new Random().Next(1000, 9999),
                UserId = customerId, // Menggunakan ID_Pelanggan untuk mencatat transaksi
                Action = "Pembelian Barang",
                Timestamp = DateTime.Now,
                Data = new PurchaseInfo { OrderId = orderId, TotalAmount = amount }
            });
        }

        public ApiResponse<List<History<PurchaseInfo>>> GetPurchaseHistory(string customerId)
        {
            var results = _historyRepo.GetAll().Where(h => h.UserId == customerId).ToList();
            return new ApiResponse<List<History<PurchaseInfo>>>
            {
                Success = results.Any(),
                Message = results.Any() ? "Histori ditemukan." : "Pelanggan ini belum memiliki riwayat pembelian.",
                Data = results
            };
        }
    }
}