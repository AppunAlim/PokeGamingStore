using System;
using System.Data;
using System.Windows.Forms;
using PokeGamingStore.Services;

namespace PokeGamingStore.GUI
{
    internal partial class CartForm : Form
    {
        private Cart _cart;
        private StockManager _stockManager;
        private ITransactionService _transactionService;
        private bool _isRefreshing = false;

        public CartForm(Cart cart, StockManager stockManager, ITransactionService transactionService)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
            _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));

            InitializeComponent();
            LoadCartData();

            dgvCart.CellValueChanged += DgvCart_CellValueChanged;
            dgvCart.CurrentCellDirtyStateChanged += DgvCart_CurrentCellDirtyStateChanged;
        }

        private void LoadCartData()
        {
            _isRefreshing = true;

            DataTable dt = new DataTable();
            dt.Columns.Add("Pilih", typeof(bool));
            dt.Columns.Add("ID Produk", typeof(string));
            dt.Columns.Add("Nama Produk", typeof(string));
            dt.Columns.Add("Harga Satuan", typeof(decimal));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Total Harga", typeof(decimal));

            foreach (var item in _cart.GetItems())
            {
                string productId = item.Key;
                int quantity = item.Value;

                var targetItem = _stockManager.GetItem(productId);
                if (targetItem != null)
                {
                    decimal unitPrice = targetItem.Price;
                    decimal totalItemPrice = unitPrice * quantity;
                    dt.Rows.Add(true, productId, targetItem.Name, unitPrice, quantity, totalItemPrice);
                }
            }

            dgvCart.DataSource = dt;

            foreach (DataGridViewColumn col in dgvCart.Columns)
            {
                if (col.Name == "Pilih") col.ReadOnly = false;
                else col.ReadOnly = true;
            }

            dgvCart.Columns["Harga Satuan"].DefaultCellStyle.Format = "Rp#,0";
            dgvCart.Columns["Total Harga"].DefaultCellStyle.Format = "Rp#,0";

            UpdateGrandTotal();
            _isRefreshing = false;
        }

        private void DgvCart_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvCart.IsCurrentCellDirty && dgvCart.CurrentCell.ColumnIndex == 0)
            {
                dgvCart.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DgvCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!_isRefreshing && e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                UpdateGrandTotal();
            }
        }

        private void BtnPlus_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
            {
                MessageBox.Show("Silakan pilih salah satu produk di dalam tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvCart.CurrentRow;
            string productId = row.Cells["ID Produk"].Value.ToString();
            string productName = row.Cells["Nama Produk"].Value.ToString();
            int currentQty = _cart.GetItems()[productId];
            int availableStock = _stockManager.GetStock(productId);

            if (currentQty + 1 > availableStock)
            {
                MessageBox.Show($"Kuantitas melebihi sisa stok di gudang!\n\nSisa stok {productName} saat ini: {availableStock} pcs.",
                                "Stok Tidak Mencukupi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return;
            }

            int newQty = currentQty + 1;
            _cart.GetItems()[productId] = newQty;

            row.Cells["Qty"].Value = newQty;
            decimal unitPrice = Convert.ToDecimal(row.Cells["Harga Satuan"].Value);
            row.Cells["Total Harga"].Value = unitPrice * newQty;

            UpdateGrandTotal();
        }

        private void BtnMin_Click(object sender, EventArgs e)
        {
            if (dgvCart.CurrentRow == null)
            {
                MessageBox.Show("Silakan pilih salah satu produk di dalam tabel terlebih dahulu!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvCart.CurrentRow;
            string productId = row.Cells["ID Produk"].Value.ToString();
            int currentQty = _cart.GetItems()[productId];

            if (currentQty > 1)
            {
                int newQty = currentQty - 1;
                _cart.GetItems()[productId] = newQty;

                row.Cells["Qty"].Value = newQty;
                decimal unitPrice = Convert.ToDecimal(row.Cells["Harga Satuan"].Value);
                row.Cells["Total Harga"].Value = unitPrice * newQty;

                UpdateGrandTotal();
            }
            else
            {
                MessageBox.Show("Kuantitas minimal adalah 1 pcs! Gunakan tombol 'Hapus Item' jika ingin membatalkan produk.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            int checkedCount = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Pilih"].Value != null && Convert.ToBoolean(row.Cells["Pilih"].Value) == true)
                {
                    checkedCount++;
                }
            }

            if (checkedCount == 0)
            {
                MessageBox.Show("Silakan centang (ceklis) terlebih dahulu item yang ingin dihapus dari keranjang!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Apakah Anda yakin ingin menghapus {checkedCount} item terpilih dari keranjang belanja?","Konfirmasi Hapus Terpilih",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                // Iterasi mundur dari bawah ke atas agar indeks tabel tidak rusak saat penghapusan berjalan
                for (int i = dgvCart.Rows.Count - 1; i >= 0; i--)
                {
                    var row = dgvCart.Rows[i];
                    if (row.Cells["Pilih"].Value != null && Convert.ToBoolean(row.Cells["Pilih"].Value) == true)
                    {
                        string productId = row.Cells["ID Produk"].Value.ToString();
                        _cart.GetItems().Remove(productId); 
                    }
                }

                LoadCartData();
            }
        }

        private void UpdateGrandTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Pilih"].Value != null && Convert.ToBoolean(row.Cells["Pilih"].Value) == true)
                {
                    decimal rowTotal = Convert.ToDecimal(row.Cells["Total Harga"].Value);
                    total += rowTotal;
                }
            }
            lblGrandTotal.Text = $"Total Pembayaran: Rp {total:N0}";
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            DataTable dtCheckedItems = new DataTable();
            dtCheckedItems.Columns.Add("ID Produk");
            dtCheckedItems.Columns.Add("Nama Produk");
            dtCheckedItems.Columns.Add("Harga Satuan", typeof(decimal));
            dtCheckedItems.Columns.Add("Qty", typeof(int));
            dtCheckedItems.Columns.Add("Total Harga", typeof(decimal));

            decimal checkoutTotal = 0;

            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                if (row.Cells["Pilih"].Value != null && Convert.ToBoolean(row.Cells["Pilih"].Value) == true)
                {
                    string pId = row.Cells["ID Produk"].Value.ToString();
                    string pName = row.Cells["Nama Produk"].Value.ToString();
                    decimal pPrice = Convert.ToDecimal(row.Cells["Harga Satuan"].Value);
                    int pQty = Convert.ToInt32(row.Cells["Qty"].Value);
                    decimal pTotal = Convert.ToDecimal(row.Cells["Total Harga"].Value);

                    dtCheckedItems.Rows.Add(pId, pName, pPrice, pQty, pTotal);
                    checkoutTotal += pTotal;
                }
            }

            if (dtCheckedItems.Rows.Count == 0)
            {
                MessageBox.Show("Silakan pilih minimal satu produk dengan mencentang kotak untuk melanjutkan proses checkout!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Guid pelangganId = Guid.NewGuid();
            PaymentForm paymentForm = new PaymentForm(dtCheckedItems, checkoutTotal, _transactionService, pelangganId);

            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow checkedRow in dtCheckedItems.Rows)
                {
                    string checkedProductId = checkedRow["ID Produk"].ToString();
                    _cart.GetItems().Remove(checkedProductId);
                }

                LoadCartData();
                this.Close();
            }
        }
    }
}