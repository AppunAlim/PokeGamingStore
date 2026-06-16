using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Services;
using PokeGamingStore.Models;

namespace PokeGamingStore.GUI
{
    public partial class PaymentForm : Form
    {
        private DataTable _cartData;
        private decimal _totalAmount;
        private ITransactionService _transactionService;
        private Guid _pelangganId;

        public PaymentForm(DataTable cartData, decimal totalAmount, ITransactionService transactionService, Guid pelangganId)
        {
            _cartData = cartData;
            _totalAmount = totalAmount;
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            _pelangganId = pelangganId;

            InitializeComponent();
            lblTotalPayment.Text = $"Total Tagihan: Rp {_totalAmount:N0}";
            pnlPaymentDetails.Visible = false;

            rbQris.CheckedChanged += RadioButtons_CheckedChanged;
            rbTransfer.CheckedChanged += RadioButtons_CheckedChanged;
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb == null || !rb.Checked) return;

            pnlPaymentDetails.Controls.Clear();
            pnlPaymentDetails.Visible = true;

            if (rb.Name == "rbQris")
            {
                RenderQrisLayout();
            }
            else if (rb.Name == "rbTransfer")
            {
                RenderBankTransferLayout();
            }
        }

        private void RenderQrisLayout()
        {
            Label lblInstruction = new Label
            {
                Text = "Scan QRIS",
                Location = new Point(10, 10),
                Size = new Size(360, 30),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            PictureBox pbQris = new PictureBox
            {
                Location = new Point(105, 50),
                Size = new Size(180, 180),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Bitmap bmp = new Bitmap(180, 180);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 15, 15, 150, 150);
                g.FillRectangle(Brushes.Black, 25, 25, 40, 40);
                g.FillRectangle(Brushes.Black, 115, 25, 40, 40);
                g.FillRectangle(Brushes.Black, 25, 115, 40, 40);
                g.DrawString("QRIS BAYAR", new Font("Arial", 9f, FontStyle.Bold), Brushes.DarkBlue, new PointF(52, 85));
            }
            pbQris.Image = bmp;

            Button btnConfirmScan = new Button
            {
                Text = "Saya Sudah Membayar via QRIS",
                Location = new Point(15, 250),
                Size = new Size(360, 35),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnConfirmScan.Click += (s, ev) => FinalizePaymentTransaction();

            pnlPaymentDetails.Controls.AddRange(new Control[] { lblInstruction, pbQris, btnConfirmScan });
        }

        private void RenderBankTransferLayout()
        {
            Label lblInstruction = new Label
            {
                Text = "Transfer tepat sesuai nominal ke nomor Virtual Account:",
                Location = new Point(10, 20),
                Size = new Size(360, 30),
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };

            TextBox txtVaNumber = new TextBox
            {
                Text = "88012340005899",
                Location = new Point(15, 60),
                Size = new Size(240, 30),
                Font = new Font("Consolas", 13f, FontStyle.Bold),
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Center
            };

            Button btnCopy = new Button
            {
                Text = "Salin No VA",
                Location = new Point(265, 59),
                Size = new Size(110, 28),
                Font = new Font("Segoe UI", 9f)
            };
            btnCopy.Click += (s, ev) => {
                Clipboard.SetText(txtVaNumber.Text);
                MessageBox.Show("Nomor Virtual Account berhasil disalin!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };

            Button btnConfirmTransfer = new Button
            {
                Text = "Konfirmasi Transfer Bank",
                Location = new Point(15, 120),
                Size = new Size(360, 35),
                BackColor = Color.FromArgb(33, 150, 243),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold)
            };
            btnConfirmTransfer.Click += (s, ev) => FinalizePaymentTransaction();

            pnlPaymentDetails.Controls.AddRange(new Control[] { lblInstruction, txtVaNumber, btnCopy, btnConfirmTransfer });
        }

        private void FinalizePaymentTransaction()
        {
            try
            {
                OrderTransaction newTx = _transactionService.BuatTransaksi(_pelangganId.ToString(), _totalAmount);

                MessageBox.Show("Pembayaran Sukses Terverifikasi! Data transaksi telah masuk ke dalam Manajemen Transaksi pusat.",
                                "Transaksi Sukses",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memproses data ke Manajemen Transaksi: {ex.Message}", "Sistem Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}