using System;
using System.Collections.Generic;
using System.Linq;
using PokeGamingStore.Models;

namespace PokeGamingStore.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepo = new GenericRepository<User>();

        private readonly IGenericRepository<History<PurchaseInfo>> _historyRepo = new GenericRepository<History<PurchaseInfo>>();

        public ApiResponse<User> RegisterUser(string username, UserRole role)
        {
            var user = new User
            {
                Id = "USR-" + Guid.NewGuid().ToString().Substring(0, 5).ToUpper(),
                Username = username,
                Role = role
            };

            _userRepo.Add(user);
            return new ApiResponse<User> { Success = true, Message = "User berhasil terdaftar.", Data = user };
        }

        public ApiResponse<List<User>> GetAllUsers()
        {
            return new ApiResponse<List<User>> { Success = true, Message = "Daftar user berhasil ditarik.", Data = _userRepo.GetAll() };
        }

        public void RecordPurchase(string userId, string orderId, decimal amount)
        {
            _historyRepo.Add(new History<PurchaseInfo>
            {
                LogId = "LOG-" + new Random().Next(1000, 9999),
                UserId = userId,
                Action = "Pembelian Barang",
                Timestamp = DateTime.Now,
                Data = new PurchaseInfo { OrderId = orderId, TotalAmount = amount }
            });
        }

        public ApiResponse<List<History<PurchaseInfo>>> GetPurchaseHistory(string userId)
        {
            var results = _historyRepo.GetAll().Where(h => h.UserId == userId).ToList();
            return new ApiResponse<List<History<PurchaseInfo>>>
            {
                Success = results.Any(),
                Message = results.Any() ? "Histori pembelian ditemukan." : "Belum ada histori pembelian.",
                Data = results
            };
        }
    }
}