using System;
using System.Linq;
using System.Windows.Forms;
using PokeGamingStore.Models;
using PokeGamingStore.Services;
using PokeGamingStore.GUI;

namespace PokeGamingStore
{
    public partial class LoginForm : Form
    {
        private ITransactionService _sharedTransactionService;
        private IUserService _userService; // Db JSON

        // Menyimpan data akun pendaftaran baru secara dinamis di memori
        private string _registeredCustomerUsername = "";
        private string _registeredCustomerPassword = "";

        public LoginForm()
        {
            InitializeComponent();
            _sharedTransactionService = new TransactionService();
            _userService = new UserService();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string usernameInput = txtUsername.Text.Trim();
            string passwordInput = txtPassword.Text;

            if (string.IsNullOrEmpty(usernameInput) || string.IsNullOrEmpty(passwordInput))
            {
                MessageBox.Show("Username dan Password tidak boleh kosong!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User loggedInUser = AuthenticateUser(usernameInput, passwordInput);

            if (loggedInUser != null)
            {
                MainForm mainDashboard = new MainForm(loggedInUser, _sharedTransactionService, this);

                this.Hide();
                mainDashboard.Show();

                txtUsername.Clear();
                txtPassword.Clear();
            }
            else
            {
                MessageBox.Show("Username atau password salah! Pastikan Anda sudah mendaftarkan akun terlebih dahulu.", "Login Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fitur tombol Daftar Akun Baru (Register)
        private void BtnRegister_Click(object sender, EventArgs e)
        {
            string regUser = txtUsername.Text.Trim();
            string regPass = txtPassword.Text;

            if (string.IsNullOrEmpty(regUser) || string.IsNullOrEmpty(regPass))
            {
                MessageBox.Show("Silakan isi kolom Username dan Password di atas untuk mendaftarkan akun baru Anda!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (regUser.ToLower() == "admin")
            {
                MessageBox.Show("Username 'admin' sudah dicadangkan oleh sistem pusat toko!", "Pendaftaran Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Menyimpan data akun pendaftaran baru secara dinamis di memori
            _registeredCustomerUsername = regUser;
            _registeredCustomerPassword = regPass;

            // Simpan ke JSON
            bool isSuccess = _userService.RegisterUserWithPassword(regUser, regPass, UserRole.Regular);

            if (isSuccess)
            {
                MessageBox.Show($"Akun Customer dengan username '{regUser}' berhasil dibuat! Silakan klik tombol 'Masuk' untuk melanjutkan.", "Pendaftaran Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Username tersebut sudah terpakai!", "Pendaftaran Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User AuthenticateUser(string username, string password)
        {
            // Baca dari JSON
            var usersResponse = _userService.GetAllUsers();
            User dbUser = usersResponse.Data?.FirstOrDefault(u =>
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                u.Password == password);

            if (dbUser != null) return dbUser;

            // Opsi 1: Akun Admin Utama Konstan
            if (username.ToLower() == "admin" && password == "admin123")
            {
                return new User
                {
                    Username = "Admin",
                    Role = UserRole.Admin
                };
            }

            // Opsi 2: Akun Customer Harus Melalui Registrasi Terlebih Dahulu
            if (!string.IsNullOrEmpty(_registeredCustomerUsername) &&
                username == _registeredCustomerUsername &&
                password == _registeredCustomerPassword)
            {
                return new User
                {
                    Username = _registeredCustomerUsername,
                    Role = UserRole.Regular
                };
            }

            return null;
        }
    }
}