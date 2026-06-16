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
        private Button btnLogout; 

        private Form currentChildForm;
        private Form _loginFormParent; 

        public MainForm(User loggedInUser, ITransactionService sharedTransactionService, Form loginFormParent)
        {
            LoggedInUser = loggedInUser;
            _currentUser = loggedInUser;
            _loginFormParent = loginFormParent;

            stockManager = new StockManager();
            cart = new Cart(stockManager, 10);

            transactionService = sharedTransactionService ?? new TransactionService();

            LoadInitialData();
            InitializeComponents();

            SwitchPage(new CatalogForm(cart, stockManager, transactionService));
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
            this.btnLogout = new Button();

            this.SuspendLayout();

            this.ClientSize = new Size(1200, 650);
            this.Name = "MainForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "PokeGamingStore - Management Dashboard";

            this.pnlSidebar.BackColor = Color.FromArgb(45, 45, 48);
            this.pnlSidebar.Controls.Add(this.btnLogout);

            if (_currentUser.Role == UserRole.Admin)
            {
                this.pnlSidebar.Controls.Add(this.btnUserHistory);
                this.pnlSidebar.Controls.Add(this.btnStockManager);
                this.pnlSidebar.Controls.Add(this.btnTransactions);
            }

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

            if (_currentUser.Role == UserRole.Admin)
            {
                SetupSidebarButton(this.btnTransactions, "Manajemen Transaksi", 135);
                this.btnTransactions.Click += (s, e) => SwitchPage(new TransactionManagementForm(transactionService));

                SetupSidebarButton(this.btnStockManager, "Gudang && Admin", 190);
                this.btnStockManager.Click += (s, e) => SwitchPage(new StockManagerForm(stockManager));

                SetupSidebarButton(this.btnUserHistory, "Manajemen User && Histori", 245);
                this.btnUserHistory.Click += (s, e) => SwitchPage(new UserHistoryForm());
            }

            SetupSidebarButton(this.btnLogout, "Logout", 580);
            this.btnLogout.BackColor = Color.FromArgb(211, 47, 47);
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);

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

        private void SwitchPage(Form childForm)
        {
            pnlMainContainer.Controls.Clear();
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlMainContainer.Controls.Add(childForm);
            childForm.Show();
        }


        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin keluar?", "Konfirmasi Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Hide(); 
                _loginFormParent.Show(); 
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menutup dan keluar dari aplikasi?", "Konfirmasi Keluar",MessageBoxButtons.YesNo,MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}