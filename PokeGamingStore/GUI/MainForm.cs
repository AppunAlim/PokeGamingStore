using System;
using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Services;
using PokeGamingStore.Catalog;
using PokeGamingStore.Models;

namespace PokeGamingStore.GUI
{
    public partial class MainForm : Form
    {
        private StockManager stockManager;
        private Cart cart;
        private ITransactionService transactionService;

        public static User LoggedInUser { get; private set; }
        private User _currentUser;

        private Panel pnlSidebar;
        private Panel pnlMainContainer;
        private Label lblLogo;
        private Button btnCatalog;
        private Button btnTransactions;
        private Button btnStockManager;
        private Button btnUserHistory;
        private Button btnExit;

        public MainForm(User loggedInUser)
        {
            LoggedInUser = loggedInUser;
            _currentUser = loggedInUser;

            stockManager = new StockManager();
            cart = new Cart(stockManager, 10);
            transactionService = new TransactionService();

            LoadInitialData();
            InitializeComponents();
            ShowWelcomePage();
        }

        private void LoadInitialData()
        {
            stockManager.AddCatalogItem(new Item { Id = "P001", Name = "Elden Ring", Price = 600000 }, 10);
            stockManager.AddCatalogItem(new Item { Id = "P002", Name = "PS5 Controller", Price = 1200000 }, 15);
            stockManager.AddCatalogItem(new Item { Id = "P003", Name = "Persona 5 Royal", Price = 750000 }, 8);
            stockManager.AddCatalogItem(new Item { Id = "P004", Name = "Xbox 360", Price = 3000000 }, 5);
        }

        private void InitializeComponents()
        {
            this.pnlSidebar = new Panel();
            this.pnlMainContainer = new Panel();
            this.lblLogo = new Label();
            this.btnCatalog = new Button();
            this.btnTransactions = new Button();
            this.btnStockManager = new Button();
            this.btnUserHistory = new Button();
            this.btnExit = new Button();

            this.SuspendLayout();

            this.ClientSize = new Size(1200, 650);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "PokeGamingStore - Management Dashboard";

            this.pnlSidebar.BackColor = Color.FromArgb(45, 45, 48);
            this.pnlSidebar.Controls.Add(this.btnExit);
            this.pnlSidebar.Controls.Add(this.btnUserHistory);
            this.pnlSidebar.Controls.Add(this.btnStockManager);
            this.pnlSidebar.Controls.Add(this.btnTransactions);
            this.pnlSidebar.Controls.Add(this.btnCatalog);
            this.pnlSidebar.Controls.Add(this.lblLogo);
            this.pnlSidebar.Dock = DockStyle.Left;
            this.pnlSidebar.Width = 220;

            this.lblLogo.ForeColor = Color.White;
            this.lblLogo.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            this.lblLogo.Location = new Point(0, 0);
            this.lblLogo.Size = new Size(220, 70);
            this.lblLogo.Text = "PokeGamingStore";
            this.lblLogo.TextAlign = ContentAlignment.MiddleCenter;

            SetupSidebarButton(this.btnCatalog, "Katalog Produk", 80);
            this.btnCatalog.Click += (s, e) => SwitchPage(new CatalogForm(cart, stockManager, transactionService));

            SetupSidebarButton(this.btnTransactions, "Manajemen Transaksi", 135);
            this.btnTransactions.Click += (s, e) => SwitchPage(new TransactionManagementForm(transactionService));

            SetupSidebarButton(this.btnStockManager, "Gudang && Admin", 190);
            this.btnStockManager.Click += (s, e) =>
            {
                if (_currentUser.Role == UserRole.Admin)
                {
                    SwitchPage(new StockManagerForm(stockManager));
                }
                else
                {
                    MessageBox.Show("Anda bukan admin.", "Akses Ditolak", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            };

            SetupSidebarButton(this.btnUserHistory, "Manajemen User && Histori", 245);
            this.btnUserHistory.Click += (s, e) => SwitchPage(new UserHistoryForm());

            SetupSidebarButton(this.btnExit, "Keluar Aplikasi", 580);
            this.btnExit.BackColor = Color.FromArgb(211, 47, 47);
            this.btnExit.Click += (s, e) => Application.Exit();

            this.pnlMainContainer.Dock = DockStyle.Fill;
            this.pnlMainContainer.BackColor = Color.FromArgb(240, 240, 240);
            this.pnlMainContainer.Location = new Point(220, 0);

            this.Controls.Add(this.pnlMainContainer);
            this.Controls.Add(this.pnlSidebar);

            this.ResumeLayout(false);
        }

        private void SetupSidebarButton(Button btn, string text, int topPosition)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10f);
            btn.Location = new Point(10, topPosition);
            btn.Size = new Size(200, 40);
            btn.Text = text;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(10, 0, 0, 0);
            btn.BackColor = Color.FromArgb(63, 63, 65);
        }

        private void ShowWelcomePage()
        {
            pnlMainContainer.Controls.Clear();
            Label lblWelcome = new Label
            {
                Text = $"Selamat Datang di PokeGamingStore Panel, {_currentUser.Username}!\n\nHak Akses: {_currentUser.Role}\nSilakan pilih menu di samping.",
                Font = new Font("Segoe UI", 14f),
                ForeColor = Color.DimGray,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlMainContainer.Controls.Add(lblWelcome);
        }

        private void SwitchPage(Form childForm)
        {
            pnlMainContainer.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            // ini direvisi bapaknya suruh hapus (susah nyarinya ternyata di mainform :v)
            // AddBackButtonToChild(childForm);
            pnlMainContainer.Controls.Add(childForm);
            childForm.Show();
        }

        private void AddBackButtonToChild(Form childForm)
        {
            Control header = null;
            foreach (Control c in childForm.Controls)
            {
                if (c is Panel && (c.Name == "pnlHeader" || c.Height <= 70))
                {
                    header = c;
                    break;
                }
            }
            if (header != null)
            {
                Button btnBack = new Button
                {
                    Text = "Kembali",
                    Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                    BackColor = Color.White,
                    ForeColor = Color.Black,
                    Location = new Point(header.Width - 120, (header.Height / 2) - 15),
                    Size = new Size(100, 30),
                    FlatStyle = FlatStyle.Flat
                };
                btnBack.FlatAppearance.BorderSize = 0;
                btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Right;
                btnBack.Click += (s, e) => ShowWelcomePage();
                header.Controls.Add(btnBack);
                btnBack.BringToFront();
            }
        }
    }
}