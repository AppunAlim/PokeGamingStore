using PokeGamingStore.Models;
using PokeGamingStore.Services;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace PokeGamingStore.GUI
{
    internal class TransactionManagementForm : Form
    {
        private ITransactionService _transactionService;

        // Komponen UI
        private Panel pnlHeader;
        private DataGridView dgvTransactions;
        private Label lblTitle;
        private GroupBox grpStatus;
        private Label lblOrderId;
        private TextBox txtOrderId;
        private Label lblEvent;
        private ComboBox cmbEvents;
        private Button btnUpdateStatus;
        private Button btnRefresh;

        public TransactionManagementForm(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));

            InitializeCustomComponents();

            LoadEventComboBox();
            RefreshTransactionTable();
        }

        private void InitializeCustomComponents()
        {
            this.pnlHeader = new Panel();
            this.dgvTransactions = new DataGridView();
            this.lblTitle = new Label();
            this.grpStatus = new GroupBox();
            this.lblOrderId = new Label();
            this.txtOrderId = new TextBox();
            this.lblEvent = new Label();
            this.cmbEvents = new ComboBox();
            this.btnUpdateStatus = new Button();
            this.btnRefresh = new Button();

            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = Color.FromArgb(76, 175, 80);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Location = new Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new Size(980, 60);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Segoe UI", 14f, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(15, 16);
            this.lblTitle.Text = "Panel Manajemen Transaksi Toko";

            // dgvTransactions
            this.dgvTransactions.AllowUserToAddRows = false;
            this.dgvTransactions.AllowUserToDeleteRows = false;
            this.dgvTransactions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTransactions.BackgroundColor = SystemColors.Window;
            this.dgvTransactions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTransactions.Location = new Point(15, 75);
            this.dgvTransactions.MultiSelect = false;
            this.dgvTransactions.Name = "dgvTransactions";
            this.dgvTransactions.ReadOnly = true;
            this.dgvTransactions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvTransactions.Size = new Size(600, 480);
            this.dgvTransactions.SelectionChanged += new EventHandler(this.DgvTransactions_SelectionChanged);

            // grpStatus
            this.grpStatus.Controls.Add(this.btnUpdateStatus);
            this.grpStatus.Controls.Add(this.cmbEvents);
            this.grpStatus.Controls.Add(this.lblEvent);
            this.grpStatus.Controls.Add(this.txtOrderId);
            this.grpStatus.Controls.Add(this.lblOrderId);
            this.grpStatus.Font = new Font("Segoe UI", 9f);
            this.grpStatus.Location = new Point(630, 75);
            this.grpStatus.Size = new Size(300, 325);
            this.grpStatus.Text = "Ubah Status Pesanan";

            // lblOrderId
            this.lblOrderId.AutoSize = true;
            this.lblOrderId.Location = new Point(15, 35);
            this.lblOrderId.Text = "ID Pesanan:";

            // txtOrderId
            this.txtOrderId.Location = new Point(15, 55);
            this.txtOrderId.ReadOnly = true;
            this.txtOrderId.Size = new Size(270, 23);

            // lblEvent
            this.lblEvent.AutoSize = true;
            this.lblEvent.Location = new Point(15, 105);
            this.lblEvent.Text = "Pilih Aksi Automata:";

            // cmbEvents
            this.cmbEvents.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbEvents.Location = new Point(15, 125);
            this.cmbEvents.Size = new Size(270, 23);

            // btnUpdateStatus
            this.btnUpdateStatus.Location = new Point(15, 180);
            this.btnUpdateStatus.Size = new Size(270, 35);
            this.btnUpdateStatus.Text = "Terapkan Perubahan";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new EventHandler(this.BtnUpdateStatus_Click);

            // btnRefresh
            this.btnRefresh.Location = new Point(630, 420);
            this.btnRefresh.Size = new Size(300, 35);
            this.btnRefresh.Text = "Segarkan Tabel (Refresh)";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new EventHandler(this.BtnRefresh_Click);

            // Form Base Settings
            this.AutoScaleDimensions = new SizeF(7f, 15f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.dgvTransactions);
            this.Font = new Font("Segoe UI", 9f);

            ((System.ComponentModel.ISupportInitialize)(this.dgvTransactions)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadEventComboBox()
        {
            cmbEvents.Items.Clear();
            foreach (EventPesanan evt in Enum.GetValues(typeof(EventPesanan)))
            {
                // Jangan tampilkan opsi 'Bayar' pada combobox manajemen transaksi
                if (evt == EventPesanan.Bayar) continue;
                cmbEvents.Items.Add(evt);
            }
            if (cmbEvents.Items.Count > 0) cmbEvents.SelectedIndex = 0;
        }

        private void RefreshTransactionTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID Pesanan");
            dt.Columns.Add("ID Pelanggan");
            dt.Columns.Add("Total Bayar", typeof(decimal));
            dt.Columns.Add("Status Saat Ini");

            var orders = _transactionService.AmbilSemua();
            foreach (var order in orders)
            {
                string shortOrderId = "ORD-" + order.Id.ToString().Substring(0, 8).ToUpper();
                dt.Rows.Add(order.Id, order.CustomerId, order.Amount, order.Status);
            }

            dgvTransactions.DataSource = dt;
            dgvTransactions.Columns["ID Pesanan"].Visible = false;
            dgvTransactions.Columns["Total Bayar"].DefaultCellStyle.Format = "Rp#,0";
        }

        private void DgvTransactions_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTransactions.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvTransactions.SelectedRows[0];
                if (row.Cells["ID Pesanan"].Value != null)
                {
                    txtOrderId.Text = row.Cells["ID Pesanan"].Value.ToString();
                }
            }
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            string orderIdText = txtOrderId.Text;

            if (string.IsNullOrEmpty(orderIdText) || !Guid.TryParse(orderIdText, out Guid orderId))
            {
                MessageBox.Show("Silakan pilih salah satu baris transaksi pada tabel terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            EventPesanan selectedEvent = (EventPesanan)cmbEvents.SelectedItem;

            try
            {
                // Jika event adalah Kemas, tampilkan popup input nomor resi sebagai gimmick
                string resi = null;
                if (selectedEvent == EventPesanan.Kemas)
                {
                    resi = Interaction.InputBox("Masukkan nomor resi:", "Input Resi", "");

                    // Jika pengguna menekan Cancel atau tidak mengisi, tanyakan konfirmasi
                    if (string.IsNullOrWhiteSpace(resi))
                    {
                        var ask = MessageBox.Show("Nomor resi kosong. Lanjutkan tanpa memasukkan resi?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (ask == DialogResult.No) return;
                    }
                }

                var updatedOrder = _transactionService.TerapkanEvent(orderId, selectedEvent);

                // Jika ada resi yang dimasukkan, tampilkan sebagai bagian dari feedback
                if (!string.IsNullOrWhiteSpace(resi))
                {
                    MessageBox.Show($"Status Pesanan Berhasil Diubah Menjadi: [{updatedOrder.Status}]\nNomor Resi: {resi}", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Status Pesanan Berhasil Diubah Menjadi: [{updatedOrder.Status}]", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                RefreshTransactionTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal mengubah status: {ex.Message}", "Transisi Aturan Gagal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            RefreshTransactionTable();
            txtOrderId.Clear();
            if (dgvTransactions.SelectedRows.Count > 0) dgvTransactions.ClearSelection();
        }
    }
}