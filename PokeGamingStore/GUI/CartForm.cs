using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PokeGamingStore;
using PokeGamingStore.Catalog;
using PokeGamingStore.Services;
using PokeGamingStore.Models; 

namespace PokeGamingStore.GUI
{
    internal partial class CartForm : Form
    {
        private Cart _cart;
        private StockManager _stockManager;
        private ITransactionService _transactionService;

        public CartForm(Cart cart, StockManager stockManager, ITransactionService transactionService)
        {
            _cart = cart ?? throw new ArgumentNullException(nameof(cart));
            _stockManager = stockManager ?? throw new ArgumentNullException(nameof(stockManager));
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));

            InitializeComponent();
            SetupTableColumns();
            UpdateCartList();
        }

        private void SetupTableColumns()
        {
            dgvCartItems.Columns.Clear();
            var checkColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Pilih",
                HeaderText = "Pilih",
                Width = 50,
                TrueValue = true,
                FalseValue = false
            };
            dgvCartItems.Columns.Add(checkColumn);
            dgvCartItems.Columns.Add("ID", "ID Produk");
            dgvCartItems.Columns.Add("Nama", "Nama Produk");
            dgvCartItems.Columns.Add("Harga", "Harga Satuan");
            dgvCartItems.Columns.Add("Qty", "Kuantitas");
            dgvCartItems.Columns.Add("Total", "Total Harga");

            foreach (DataGridViewColumn col in dgvCartItems.Columns)
            {
                if (col.Name != "Pilih") col.ReadOnly = true;
            }
            dgvCartItems.Columns["Harga"].DefaultCellStyle.Format = "Rp#,0";
            dgvCartItems.Columns["Total"].DefaultCellStyle.Format = "Rp#,0";
        }

        private void UpdateCartList()
        {
            dgvCartItems.Rows.Clear();
            var cartItems = _cart.GetItems();

            foreach (var kvp in cartItems)
            {
                string itemId = kvp.Key;
                int quantity = kvp.Value;
                string displayName = itemId;
                decimal itemPrice = 0;

                Item item = _stockManager.GetItem(itemId);
                if (item != null)
                {
                    displayName = item.Name;
                    itemPrice = item.Price;
                }
                else
                {
                    var catalogProducts = ProductCatalog<Product>.Instance.GetAllProducts();
                    var matchedProduct = catalogProducts.Find(p => p.Id == itemId);
                    if (matchedProduct != null)
                    {
                        displayName = matchedProduct.Name;
                        if (itemId == "P001") itemPrice = 600000;
                        else if (itemId == "P002") itemPrice = 1200000;
                        else if (itemId == "P003") itemPrice = 750000;
                        else if (itemId == "P004") itemPrice = 3000000;
                    }
                }

                decimal totalProductPrice = itemPrice * quantity;
                dgvCartItems.Rows.Add(true, itemId, displayName, itemPrice, quantity, totalProductPrice);
            }

            lblTotalItems.Text = $"Total Item di Keranjang: {_cart.GetTotalItems()}";
            CalculateSelectedTotal();
        }

        private void CalculateSelectedTotal()
        {
            decimal selectedTotal = 0;
            foreach (DataGridViewRow row in dgvCartItems.Rows)
            {
                if (row.Cells["Pilih"].Value != null && (bool)row.Cells["Pilih"].Value == true)
                {
                    selectedTotal += Convert.ToDecimal(row.Cells["Total"].Value);
                }
            }
            lblTotalHarga.Text = $"Total Terpilih: Rp{selectedTotal:N0}";
        }

        private void dgvCartItems_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) CalculateSelectedTotal();
        }

        private void dgvCartItems_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0) dgvCartItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvCartItems.SelectedRows.Count == 0) return;
            DataGridViewRow selectedRow = dgvCartItems.SelectedRows[0];
            string itemId = selectedRow.Cells["ID"].Value.ToString();
            string displayName = selectedRow.Cells["Nama"].Value.ToString();

            DialogResult result = MessageBox.Show($"Kurangi '{displayName}' dari keranjang?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            try
            {
                Item item = _stockManager.GetItem(itemId) ?? new Item { Id = itemId };
                _cart.RemoveFromCart(item, 1);
                UpdateCartList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }
        }

        private void BtnCheckout_Click(object sender, EventArgs e)
        {
            DataTable dtSelected = new DataTable();
            dtSelected.Columns.Add("ID Produk");
            dtSelected.Columns.Add("Nama Produk");
            dtSelected.Columns.Add("Harga Satuan", typeof(decimal));
            dtSelected.Columns.Add("Kuantitas", typeof(int));
            dtSelected.Columns.Add("Total Harga", typeof(decimal));

            decimal checkoutTotal = 0;
            List<string> itemIdsToRemove = new List<string>();

            foreach (DataGridViewRow row in dgvCartItems.Rows)
            {
                if (row.Cells["Pilih"].Value != null && (bool)row.Cells["Pilih"].Value == true)
                {
                    string id = row.Cells["ID"].Value.ToString();
                    string nama = row.Cells["Nama"].Value.ToString();
                    decimal harga = Convert.ToDecimal(row.Cells["Harga"].Value);
                    int qty = Convert.ToInt32(row.Cells["Qty"].Value);
                    decimal total = Convert.ToDecimal(row.Cells["Total"].Value);

                    dtSelected.Rows.Add(id, nama, harga, qty, total);
                    checkoutTotal += total;
                    itemIdsToRemove.Add(id);
                }
            }

            if (dtSelected.Rows.Count == 0)
            {
                MessageBox.Show("Silakan centang minimal satu produk untuk dicheckout!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var paymentForm = new PaymentForm(dtSelected, checkoutTotal))
            {
                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
 
                    User currentUser = MainForm.LoggedInUser;
                    string idPelangganAktif = currentUser?.CustomerId;

                    // Berjaga-jaga jika sesi hilang atau admin lolos
                    if (string.IsNullOrEmpty(idPelangganAktif))
                    {
                        idPelangganAktif = "CUST-UNKNOWN";
                    }
                    _transactionService.BuatTransaksi(idPelangganAktif, checkoutTotal);


                    IUserService userService = new UserService();
                    string generateOrderId = "ORD-" + new Random().Next(100, 999).ToString("D3");
                    userService.RecordPurchase(idPelangganAktif, generateOrderId, checkoutTotal);

                    // Bersihkan barang yang sukses dibayar dari keranjang
                    foreach (string id in itemIdsToRemove)
                    {
                        _cart.GetItems().Remove(id);
                    }
                    UpdateCartList();

                    MessageBox.Show("Pembayaran Berhasil! Histori transaksi telah tersimpan di sistem.", "Checkout Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}