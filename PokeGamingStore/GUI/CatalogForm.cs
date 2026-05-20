using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Catalog;

namespace PokeGamingStore.GUI
{
    public partial class CatalogForm : Form
    {
        private TextBox txtSearch;
        private ComboBox cmbField;
        private Button btnSearch;
        private FlowLayoutPanel flpCards;
        private Label lblStatus;

        private readonly ProductCatalog<Product> catalog = ProductCatalog<Product>.Instance;

        public CatalogForm()
        {
            InitializeComponent();
            BuildUI();
            ShowAll();
        }

        private void BuildUI()
        {
            this.Text = "PokeGamingStore - Katalog";
            this.Size = new Size(860, 620);
            this.MinimumSize = new Size(640, 480);
            this.BackColor = SystemColors.Control;
            this.ForeColor = SystemColors.ControlText;
            this.Font = new Font("Segoe UI", 9f);
            this.StartPosition = FormStartPosition.CenterScreen;

            var pnlStatus = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 28,
                BackColor = SystemColors.ControlLight
            };

            lblStatus = new Label
            {
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 10, 0),
                ForeColor = Color.DimGray,
                Font = new Font("Segoe UI", 8.5f)
            };
            pnlStatus.Controls.Add(lblStatus);
            this.Controls.Add(pnlStatus);

            var pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 52,
                BackColor = SystemColors.Window,
                Padding = new Padding(12, 11, 12, 0),
                BorderStyle = BorderStyle.FixedSingle
            };

            var lblTitle = new Label
            {
                Text = "Katalog Produk",
                AutoSize = true,
                Location = new Point(12, 15),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.Black
            };

            cmbField = new ComboBox
            {
                Location = new Point(190, 13),
                Size = new Size(110, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Standard
            };
            cmbField.Items.AddRange(new object[] { "Name", "Category", "Id" });
            cmbField.SelectedIndex = 0;

            txtSearch = new TextBox
            {
                Location = new Point(308, 13),
                Size = new Size(310, 28),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Ketik keyword lalu Enter...",
                Font = new Font("Segoe UI", 9.5f)
            };
            txtSearch.KeyDown += (s, e) => { if (e.KeyCode == Keys.Enter) DoSearch(); };

            btnSearch = new Button
            {
                Location = new Point(626, 12),
                Size = new Size(80, 26),
                Text = "Cari",
                FlatStyle = FlatStyle.Standard,
                Cursor = Cursors.Hand
            };
            btnSearch.Click += (s, e) => DoSearch();

            var btnAll = new Button
            {
                Location = new Point(714, 12),
                Size = new Size(80, 26),
                Text = "Semua",
                FlatStyle = FlatStyle.Standard,
                Cursor = Cursors.Hand
            };
            btnAll.Click += (s, e) => ShowAll();

            pnlTop.Controls.AddRange(new Control[]
                { lblTitle, cmbField, txtSearch, btnSearch, btnAll });
            this.Controls.Add(pnlTop);

            flpCards = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = SystemColors.Control,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(10)
            };
            this.Controls.Add(flpCards);

            flpCards.BringToFront();
        }

        private void ShowAll()
        {
            Render(catalog.GetAllProducts());
            txtSearch.Clear();
        }

        private void DoSearch()
        {
            try
            {
                var field = cmbField.SelectedItem.ToString();
                var keyword = txtSearch.Text;
                var results = catalog.SearchProduct(field, keyword);
                Render(results);
            }
            catch (Exception ex)
            {
                lblStatus.Text = $"Error: {ex.Message}";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void Render(List<Product> list)
        {
            flpCards.Controls.Clear();
            foreach (var p in list)
            {
                var card = new ProductCard(p);
                flpCards.Controls.Add(card);
            }
            lblStatus.Text = $"Menampilkan {list.Count} produk";
            lblStatus.ForeColor = Color.DimGray;
        }

        private void CatalogForm_Load(object sender, EventArgs e)
        {
        }
    }
}