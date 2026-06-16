using System;
using System.Windows.Forms;
using PokeGamingStore.Models;
using PokeGamingStore.Services;

namespace PokeGamingStore.GUI
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;

        public LoginForm()
        {
            InitializeComponent();
            _userService = new UserService();

            cmbRole.Items.Clear();
            cmbRole.Items.Add(UserRole.Admin);
            cmbRole.Items.Add(UserRole.Regular);
            cmbRole.SelectedIndex = 1;

            // Suntik akun Admin bawaan otomatis
            _userService.RegisterUserWithPassword("Admin", "admin123", UserRole.Admin);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text; // Asumsi kamu bikin TextBox password
            UserRole selectedRole = (UserRole)cmbRole.SelectedItem;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username dan Password wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loggedInUser = _userService.ValidateLogin(username, password, selectedRole);
            if (loggedInUser != null)
            {
                MainForm mainForm = new MainForm(loggedInUser); // Lempar data ke MainForm
                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kredensial salah atau akun tidak ditemukan!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            UserRole selectedRole = (UserRole)cmbRole.SelectedItem;

            if (selectedRole == UserRole.Admin)
            {
                MessageBox.Show("Registrasi mandiri untuk Admin tidak diizinkan!", "Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Isi Username dan Password!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_userService.RegisterUserWithPassword(username, password, UserRole.Regular))
            {
                MessageBox.Show($"Akun '{username}' sukses dibuat. Silakan Login.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Username sudah terpakai!", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}