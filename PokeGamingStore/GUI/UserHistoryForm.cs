using System;
using System.Linq;
using System.Windows.Forms;
using PokeGamingStore.Models;
using PokeGamingStore.Services;

namespace PokeGamingStore.GUI
{
    public partial class UserHistoryForm : Form
    {
        private readonly IUserService _userService;

        public UserHistoryForm()
        {
            InitializeComponent();
            _userService = new UserService();

            cmbRole.Items.Clear();
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.Items.Add(UserRole.Regular);
            cmbRole.SelectedIndex = 0;

            LoadDataUsers();

            dgvHistory.DataSource = null; // Memastikan tabel histori benar-benar kosong di awal
        }

        private void CheckAndLoadInitialHistory()
        {
            var totalHistory = _userService.GetAllHistory();
            if (totalHistory == null || !totalHistory.Any())
            {
                dgvHistory.DataSource = null;
            }
            else
            {
                BindGridHistory(totalHistory);
            }
        }

        private void BindGridHistory(System.Collections.Generic.List<History<PurchaseInfo>> list)
        {
            dgvHistory.DataSource = list.Select(h => new
            {
                Waktu = h.Timestamp.ToString("dd-MM-yyyy HH:mm"),
                Aksi = h.Action,
                ID_Pelanggan = h.UserId, // Menggunakan ID_Pelanggan sesuai data yang masuk dari transaksi
                ID_Order = h.Data?.OrderId ?? "-",
                Total_Bayar = $"Rp {h.Data?.TotalAmount:N0}"
            }).ToList();
        }

        private void LoadDataUsers()
        {
            var response = _userService.GetAllUsers();
            if (response.Success && response.Data != null)
            {
                // Menambahkan kolom ID_Pelanggan ke tabel
                dgvUsers.DataSource = response.Data.Select(u => new
                {
                    ID_User = u.Id,
                    ID_Pelanggan = u.CustomerId ?? "Admin - No Cust ID",
                    Username = u.Username,
                    Role = u.Role
                }).ToList();
            }
        }

        private void btnTambahUser_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) return;
            UserRole selectedRole = (UserRole)cmbRole.SelectedItem;

            _userService.RegisterUserWithPassword(txtUsername.Text, "123", selectedRole);
            txtUsername.Clear();
            LoadDataUsers();
        }

        private void btnCariHistori_Click(object sender, EventArgs e)
        {
            string keyword = txtCariUserId.Text.Trim();

            var searchResult = _userService.SearchHistory(keyword);

            if (!searchResult.Any())
            {
                dgvHistory.DataSource = null;
         
                MessageBox.Show("Pelanggan ini belum memiliki riwayat pembelian.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                BindGridHistory(searchResult);
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Mengambil nilai ID_Pelanggan, bukan ID_User
                var val = dgvUsers.Rows[e.RowIndex].Cells["ID_Pelanggan"].Value?.ToString();

                // Jika yang diklik adalah Admin, kosongkan kotak pencariannya
                if (val != null && !val.Contains("Admin"))
                {
                    txtCariUserId.Text = val;
                }
                else
                {
                    txtCariUserId.Text = "";
                }
            }
        }
    }
}