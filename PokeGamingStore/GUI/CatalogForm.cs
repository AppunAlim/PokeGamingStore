using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Catalog;
using PokeGamingStore.Services;

namespace PokeGamingStore.GUI
{
    internal partial class CatalogForm : Form
    {
        private ProductCatalog<Product> catalog;
        private Cart _cart;
        private StockManager _stockManager;
        private ITransactionService _transactionService;

        private Panel pnlHeader;
        private Label lblTitle;
        private TextBox txtSearch;
        private Button btnSearch;
        private Button btnShowAll;
        private Button btnOpenCart;
        private FlowLayoutPanel flpCards;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblStatus;

        public CatalogForm(Cart cart, StockManager stockManager, ITransactionService transactionService)
        {
            catalog = ProductCatalog<Product>.Instance;
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
            _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));

            InitializeCustomComponents();
            ShowAll();
        }

        private void InitializeCustomComponents()
        {
            this.pnlHeader = new Panel();
            this.lblTitle = new Label();
            this.txtSearch = new TextBox();
            this.btnSearch = new Button();
            this.btnShowAll = new Button();
            this.btnOpenCart = new Button();
            this.flpCards = new FlowLayoutPanel();
            this.statusStrip = new StatusStrip();
            this.lblStatus = new ToolStripStatusLabel();

            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(76, 175, 80);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.txtSearch);
            this.pnlHeader.Controls.Add(this.btnSearch);
            this.pnlHeader.Controls.Add(this.btnShowAll);
            this.pnlHeader.Controls.Add(this.btnOpenCart);
            this.pnlHeader.Dock = DockStyle.Top;
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Size = new Size(980, 60);
            this.pnlHeader.Name = "pnlHeader";

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(15, 16);
            this.lblTitle.Text = "Katalog Produk";

            // txtSearch 
            this.txtSearch.Location = new Point(180, 18);
            this.txtSearch.Size = new Size(350, 23);
            this.txtSearch.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    HandleSearch();
                }
            };

            // btnSearch
            this.btnSearch.Location = new Point(540, 17);
            this.btnSearch.Size = new Size(70, 25);
            this.btnSearch.Text = "Cari";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += (s, e) => HandleSearch();

            // btnShowAll
            this.btnShowAll.Location = new Point(620, 17);
            this.btnShowAll.Size = new Size(160, 25);
            this.btnShowAll.Text = "Tampilkan Semua Produk";
            this.btnShowAll.UseVisualStyleBackColor = true;
            this.btnShowAll.Click += (s, e) => ShowAll();

            // btnOpenCart
            this.btnOpenCart.Location = new Point(790, 17);
            this.btnOpenCart.Size = new Size(40, 25);
            this.btnOpenCart.Text = "🛒";
            this.btnOpenCart.UseVisualStyleBackColor = true;
            this.btnOpenCart.Click += (s, e) => OpenCart();

            // flpCards
            this.flpCards.AutoScroll = true;
            this.flpCards.Dock = DockStyle.Fill;
            this.flpCards.Location = new Point(0, 60);
            this.flpCards.Padding = new Padding(15);

            // statusStrip
            this.statusStrip.Items.AddRange(new ToolStripItem[] { this.lblStatus });
            this.statusStrip.Location = new Point(0, 539);
            this.statusStrip.Size = new Size(980, 22);

            // lblStatus
            this.lblStatus.ForeColor = Color.DimGray;
            this.lblStatus.Text = "Menampilkan produk";

            // CatalogForm Settings
            this.AutoScaleDimensions = new SizeF(7f, 15f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.flpCards);
            this.Controls.Add(this.pnlHeader);
            this.Font = new Font("Segoe UI", 9f);

            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private List<Product> GetCombinedProducts()
        {
            List<Product> combinedList = new List<Product>();

            if (_stockManager != null)
            {
                foreach (Item item in _stockManager.GetCatalog())
                {
                    string finalCategory = StockManagerForm.GetItemCategory(item.Id);

                    if (string.IsNullOrEmpty(finalCategory))
                    {
                        var originalProduct = catalog.GetAllProducts().Find(p => p.Id == item.Id);
                        finalCategory = originalProduct != null ? originalProduct.Category : "GAME";
                    }

                    Product newProduct = new Product
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Category = finalCategory,
                        Price = item.Price,
                        Stock = _stockManager.GetStock(item.Id)
                    };

                    combinedList.Add(newProduct);
                }
            }
            return combinedList;
        }

        private void ShowAll()
        {
            txtSearch.Clear();
            List<Product> allProducts = GetCombinedProducts();
            Render(allProducts);
        }

        private void HandleSearch()
        {
            string rawKeyword = txtSearch.Text;

            if (string.IsNullOrWhiteSpace(rawKeyword))
            {
                MessageBox.Show("Silakan masukkan kata kunci pencarian terlebih dahulu!", "Pencarian Kosong", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            try
            {
                string safeKeyword = catalog.SanitizeInput(rawKeyword);

                if (string.IsNullOrWhiteSpace(safeKeyword))
                {
                    throw new ArgumentException("Kata kunci tidak valid! Jangan gunakan simbol atau karakter khusus.");
                }

                List<Product> sourceList = GetCombinedProducts();
                List<Product> searchResults = new List<Product>();

                foreach (var product in sourceList)
                {
                    bool matchName = product.Name != null && product.Name.Contains(safeKeyword, StringComparison.OrdinalIgnoreCase);
                    bool matchCategory = product.Category != null && product.Category.Contains(safeKeyword, StringComparison.OrdinalIgnoreCase);
                    bool matchId = product.Id != null && product.Id.Contains(safeKeyword, StringComparison.OrdinalIgnoreCase);

                    if (matchName || matchCategory || matchId)
                    {
                        searchResults.Add(product);
                    }
                }

                if (searchResults.Count == 0)
                {
                    MessageBox.Show($"Produk dengan kata kunci '{rawKeyword}' tidak ditemukan.", "Hasil Tidak Ditemukan", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ShowAll();
                    return;
                }

                Render(searchResults);
            }
            catch (ArgumentException argEx)
            {
                MessageBox.Show(argEx.Message, "Input Bermasalah", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.SelectAll();
                txtSearch.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Terjadi kesalahan sistem saat mencari: {ex.Message}", "Error Sistem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Render(List<Product> list)
        {
            flpCards.Controls.Clear();
            foreach (var p in list)
            {
                var card = new ProductCard(p);

                card.AddToCartClicked += (s, e) => AddProductToCart(p);
                card.BuyNowClicked += (s, e) => BuyProductNow(p);

                flpCards.Controls.Add(card);
            }
            lblStatus.Text = $"Menampilkan {list.Count} produk";
            lblStatus.ForeColor = Color.DimGray;
        }

        private bool AddProductToCart(Product p)
        {
            if (_cart == null || _stockManager == null)
            {
                MessageBox.Show("Sistem keranjang belanja belum siap.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            try
            {
                Item targetItem = _stockManager.GetItem(p.Id);

                if (targetItem == null)
                {
                    targetItem = new Item { Id = p.Id, Name = p.Name, Price = p.Price };
                    _stockManager.AddCatalogItem(targetItem, 10);
                }

                _cart.AddToCart(targetItem, 1);

                lblStatus.Text = $"Berhasil menambahkan '{p.Name}' ke keranjang.";
                lblStatus.ForeColor = Color.Green;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Gagal Menambah", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void BuyProductNow(Product p)
        {
            if (AddProductToCart(p))
            {
                OpenCart();
            }
        }

        private void OpenCart()
        {
            using (var cartForm = new CartForm(_cart, _stockManager, _transactionService))
            {
                cartForm.ShowDialog();
            }
            ShowAll();
        }
    }
}