using PokeGamingStore.Services;
using System;
using System.Data;
using System.Windows.Forms;

namespace PokeGamingStore.GUI
{
    internal partial class PaymentForm : Form
    {
        private DataTable _dtSelectedItems;
        private decimal _totalAmount;
        private ITransactionService _transactionService;
        private Guid _orderId;

        public PaymentForm(DataTable dtSelectedItems, decimal totalAmount, ITransactionService transactionService, Guid orderId)
        {
            _dtSelectedItems = dtSelectedItems ?? throw new ArgumentNullException(nameof(dtSelectedItems));
            _totalAmount = totalAmount;
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _orderId = orderId;

            InitializeComponent();

            // Tampilkan jumlah total tagihan dengan format rupiah
            lblTotalTagihan.Text = $"Total Tagihan: Rp{_totalAmount:N0}";
        }

        private void BtnConfirmPayment_Click(object sender, EventArgs e)
        {
            string chosenMethod = rdbQris.Checked ? "QRIS" : "Transfer Bank";

            string message = $"Konfirmasi Pembayaran:\n\n" +
                             $"Total: Rp{_totalAmount:N0}\n" +
                             $"Metode: {chosenMethod}\n\n" +
                             $"Apakah Anda ingin melanjutkan?";

            DialogResult result = MessageBox.Show(message, "Konfirmasi Pembayaran", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Terapkan event "Bayar" langsung pada transaksi yang sudah dibuat
                try
                {
                    _transactionService.TerapkanEvent(_orderId, Models.EventPesanan.Bayar);
                    MessageBox.Show("Pembayaran Berhasil Dikonfirmasi!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menerapkan status pembayaran: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}