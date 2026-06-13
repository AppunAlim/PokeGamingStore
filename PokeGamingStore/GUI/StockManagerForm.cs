using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace PokeGamingStore.GUI
{
    internal partial class StockManagerForm : Form
    {
        private StockManager _stockManager;
        private bool _isClearing = false;

        private static System.Collections.Generic.Dictionary<string, string> itemCategories =
            new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "P001", "GAME" },
                { "P002", "ACCESSORY" },
                { "P003", "GAME" },
                { "P004", "CONSOLE" }
            };

        public static string GetItemCategory(string id)
        {
            if (id != null && itemCategories.TryGetValue(id, out string category))
            {
                return category;
            }
            return "GAME";
        }

        public StockManagerForm(StockManager stockManager)
        {
            _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));

            InitializeComponent();
            RefreshStockTable();

            BtnClear_Click(this, EventArgs.Empty);
        }

        private void RefreshStockTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID Barang");
            dt.Columns.Add("Nama Barang");
            dt.Columns.Add("Kategori");
            dt.Columns.Add("Harga Gudang", typeof(decimal));
            dt.Columns.Add("Sisa Stok", typeof(int));

            foreach (var item in _stockManager.GetCatalog())
            {
                string cat = GetItemCategory(item.Id);
                int stockCount = _stockManager.GetStock(item.Id);
                dt.Rows.Add(item.Id, item.Name, cat, item.Price, stockCount);
            }

            dgvStock.DataSource = dt;
            dgvStock.Columns["Harga Gudang"].DefaultCellStyle.Format = "Rp#,0";
        }

        private string GenerateAutoId()
        {
            var catalogList = _stockManager.GetCatalog();
            if (catalogList == null || catalogList.Count == 0)
            {
                return "P001";
            }

            int maxIdNumber = 0;

            foreach (var item in catalogList)
            {
                // Memastikan ID diawali dengan 'P' dan memiliki panjang yang cukup
                if (!string.IsNullOrEmpty(item.Id) && item.Id.StartsWith("P", StringComparison.OrdinalIgnoreCase))
                {
                    string numberPart = item.Id.Substring(1);
                    if (int.TryParse(numberPart, out int currentNumber))
                    {
                        if (currentNumber > maxIdNumber)
                        {
                            maxIdNumber = currentNumber;
                        }
                    }
                }
            }

            int nextNumber = maxIdNumber + 1;

            // Kembalikan dalam format string berpola "P00X"
            return "P" + nextNumber.ToString("D3");
        }

        private void DgvStock_SelectionChanged(object sender, EventArgs e)
        {
            if (_isClearing) return;

            if (dgvStock.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvStock.SelectedRows[0];
                txtItemId.Text = row.Cells["ID Barang"].Value?.ToString();
                txtName.Text = row.Cells["Nama Barang"].Value?.ToString();
                txtPrice.Text = row.Cells["Harga Gudang"].Value?.ToString();
                txtStock.Text = row.Cells["Sisa Stok"].Value?.ToString();

                txtItemId.ReadOnly = true;
                txtItemId.BackColor = SystemColors.Control;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            _isClearing = true;

            dgvStock.ClearSelection();

            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            if (cmbCategory.Items.Count > 0) cmbCategory.SelectedIndex = 0;

            txtItemId.Text = GenerateAutoId();
            txtItemId.ReadOnly = true;
            txtItemId.BackColor = SystemColors.Control;

            txtName.Focus();

            _isClearing = false;
        }

        // ==================== CREATE ====================
        private void BtnCreate_Click(object sender, EventArgs e)
        {
            string id = txtItemId.Text.Trim();
            string name = txtName.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string stockText = txtStock.Text.Trim();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(priceText) || string.IsNullOrEmpty(stockText))
            {
                MessageBox.Show("Semua kolom input (Nama, Harga, dan Stok) wajib diisi!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(priceText, out decimal price) || !int.TryParse(stockText, out int stockQuantity))
            {
                MessageBox.Show("Harga dan Jumlah Stok harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (stockQuantity < 0)
            {
                MessageBox.Show("Jumlah stok tidak boleh minus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_stockManager.GetItem(id) != null)
            {
                id = GenerateAutoId();
            }

            Item newItem = new Item { Id = id, Name = name, Price = price };
            _stockManager.AddCatalogItem(newItem, stockQuantity);

            itemCategories[id] = cmbCategory.SelectedItem.ToString();

            MessageBox.Show($"Produk baru '{name}' sukses ditambahkan dengan ID otomatis: [{id}]!", "Sukses CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshStockTable();
            BtnClear_Click(this, EventArgs.Empty);
        }

        // ==================== UPDATE ====================
        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            string id = txtItemId.Text.Trim();
            string name = txtName.Text.Trim();
            string priceText = txtPrice.Text.Trim();
            string stockText = txtStock.Text.Trim();

            if (dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih terlebih dahulu produk yang ingin diubah dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Item existingItem = _stockManager.GetItem(id);
            if (existingItem == null) return;

            if (!decimal.TryParse(priceText, out decimal price) || !int.TryParse(stockText, out int stockQuantity))
            {
                MessageBox.Show("Harga dan Jumlah Stok harus berupa angka valid!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (stockQuantity < 0)
            {
                MessageBox.Show("Jumlah stok tidak boleh minus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            existingItem.Name = name;
            existingItem.Price = price;
            itemCategories[id] = cmbCategory.SelectedItem.ToString();

            int currentStock = _stockManager.GetStock(id);
            if (stockQuantity > currentStock)
            {
                _stockManager.ReturnStock(id, stockQuantity - currentStock);
            }
            else if (stockQuantity < currentStock)
            {
                _stockManager.ReduceStock(id, currentStock - stockQuantity);
            }

            MessageBox.Show($"Data produk ID '{id}' berhasil diperbarui!", "Sukses CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshStockTable();
            BtnClear_Click(this, EventArgs.Empty);
        }

        // ==================== DELETE ====================
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string id = txtItemId.Text.Trim();

            if (dgvStock.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih terlebih dahulu produk yang ingin dihapus dari tabel!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialog = MessageBox.Show($"Apakah Anda yakin ingin menghapus produk dengan ID '{id}' dari sistem?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.No) return;

            var catalogList = _stockManager.GetCatalog();
            Item targetItem = _stockManager.GetItem(id);
            if (targetItem != null)
            {
                catalogList.Remove(targetItem);
                itemCategories.Remove(id);
            }

            MessageBox.Show($"Produk ID '{id}' sukses dihapus dari katalog gudang!", "Sukses CRUD", MessageBoxButtons.OK, MessageBoxIcon.Information);
            RefreshStockTable();
            BtnClear_Click(this, EventArgs.Empty);
        }
    }
}