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
        private string _selectedUserId = "";

        public UserHistoryForm()
        {
            InitializeComponent();
            _userService = new UserService();

            LoadDataUsers();
            dgvHistory.DataSource = null; // Histori awal kosong sampai user dipilih
        }

        private void BindGridHistory(System.Collections.Generic.List<History<PurchaseInfo>> list)
        {
            dgvHistory.DataSource = list.Select(h => new
            {
                Waktu = h.Timestamp.ToString("dd-MM-yyyy HH:mm"),
                Aksi = h.Action,
                ID_Pelanggan = h.UserId,
                ID_Order = h.Data?.OrderId ?? "-",
                Total_Bayar = $"Rp {h.Data?.TotalAmount:N0}"
            }).ToList();
        }

        private void LoadDataUsers()
        {
            var response = _userService.GetAllUsers();
            if (response.Success && response.Data != null)
            {
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = response.Data.Select(u => new
                {
                    ID_User = u.Id,
                    ID_Pelanggan = u.CustomerId ?? "Admin - No Cust ID",
                    Username = u.Username,
                    Role = u.Role
                }).ToList();
            }
        }

        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // simpan ID user yang dipilih untuk keperluan perubahan password
                _selectedUserId = dgvUsers.Rows[e.RowIndex].Cells["ID_User"].Value?.ToString();
                txtUsername.Text = dgvUsers.Rows[e.RowIndex].Cells["Username"].Value?.ToString();

                // id pelanggan hanya ditampilkan di txtCariUserId jika bukan admin, karena admin tidak memiliki ID_Pelanggan
                var val = dgvUsers.Rows[e.RowIndex].Cells["ID_Pelanggan"].Value?.ToString();
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

        private void btnSimpanPassword_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedUserId))
            {
                MessageBox.Show("Pilih user dari tabel terlebih dahulu dengan mengklik salah satu baris!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPasswordBaru.Text))
            {
                MessageBox.Show("Masukkan password baru terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool isSuccess = _userService.ChangePassword(_selectedUserId, txtPasswordBaru.Text);

            if (isSuccess)
            {
                MessageBox.Show($"Password untuk akun '{txtUsername.Text}' berhasil diubah dan disimpan permanen!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPasswordBaru.Clear();
            }
            else
            {
                MessageBox.Show("Terjadi kesalahan saat mengubah password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}