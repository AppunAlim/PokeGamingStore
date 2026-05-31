using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using PokeGamingStore.Models;
using PokeGamingStore.Services;

namespace PokeGamingStore.GUI
{
    public partial class TransactionManagementForm : Form
    {
        private readonly ITransactionService _transactionService;
        private DataGridView dgvTransactions;
        private Button btnCreateTransaction;
        private Button btnApplyEvent;
        private Button btnRefresh;
        private TextBox txtCustomerId;
        private TextBox txtAmount;
        private ComboBox cmbEventType;
        private Label lblStatus;
        private Panel pnlTopBar;
        private Panel pnlControlPanel;
        private Panel pnlStatusBar;

        private const int DefaultFormWidth = 1200;
        private const int DefaultFormHeight = 700;
        private const int ControlPadding = 12;
        private const int ButtonHeight = 36;
        private const int RowHeight = 28;

        // Color scheme for professional appearance
        private static readonly Color PrimaryColor = Color.FromArgb(33, 150, 243);
        private static readonly Color SecondaryColor = Color.FromArgb(63, 81, 181);
        private static readonly Color SuccessColor = Color.FromArgb(76, 175, 80);
        private static readonly Color WarningColor = Color.FromArgb(255, 152, 0);
        private static readonly Color ErrorColor = Color.FromArgb(244, 67, 54);
        private static readonly Color BackgroundColor = Color.FromArgb(245, 245, 245);
        private static readonly Color TextColor = Color.FromArgb(33, 33, 33);

        public TransactionManagementForm(ITransactionService transactionService)
        {
            _transactionService = transactionService ?? throw new ArgumentNullException(nameof(transactionService));
            InitializeComponent();
            BuildUI();
            LoadTransactions();
        }

        private void BuildUI()
        {
            ConfigureForm();
            CreateStatusBar();
            CreateDataGridView();
            CreateControlPanel();
            CreateTopBar();
        }

        private void ConfigureForm()
        {
            Text = "Manajemen Transaksi Pesanan";
            Size = new Size(DefaultFormWidth, DefaultFormHeight);
            MinimumSize = new Size(900, 500);
            BackColor = BackgroundColor;
            ForeColor = TextColor;
            Font = new Font("Segoe UI", 9f);
            StartPosition = FormStartPosition.CenterScreen;
            Icon = null;
        }

        private void CreateTopBar()
        {
            pnlTopBar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = PrimaryColor,
                Padding = new Padding(ControlPadding)
            };

            var lblTitle = new Label
            {
                Text = "Sistem Manajemen Transaksi & Status Pesanan",
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(ControlPadding, ControlPadding)
            };

            var lblSubtitle = new Label
            {
                Text = "Kelola status pesanan dengan automata state machine",
                Font = new Font("Segoe UI", 9f),
                ForeColor = Color.FromArgb(255, 255, 255, 200),
                AutoSize = true,
                Location = new Point(ControlPadding, 40)
            };

            pnlTopBar.Controls.AddRange(new Control[] { lblTitle, lblSubtitle });
            Controls.Add(pnlTopBar);
        }

        private void CreateControlPanel()
        {
            pnlControlPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = Color.White,
                Padding = new Padding(ControlPadding),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Customer ID input
            var lblCustomerId = new Label
            {
                Text = "ID Pelanggan:",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(ControlPadding, ControlPadding),
                AutoSize = true
            };

            txtCustomerId = new TextBox { Location = new Point(ControlPadding, ControlPadding + 25), Size = new Size(200, 28), BorderStyle = BorderStyle.FixedSingle, PlaceholderText = "Masukkan ID pelanggan", Font = new Font("Segoe UI", 9f) };

            // Amount input
            var lblAmount = new Label
            {
                Text = "Jumlah (Rp):",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(220, ControlPadding),
                AutoSize = true
            };

            txtAmount = new TextBox
            {
                Location = new Point(220, ControlPadding + 25),
                Size = new Size(200, 28),
                BorderStyle = BorderStyle.FixedSingle,
                PlaceholderText = "Masukkan jumlah",
                Font = new Font("Segoe UI", 9f)
            };

            // Event type selection
            var lblEvent = new Label
            {
                Text = "Pilih Event:",
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Location = new Point(440, ControlPadding),
                AutoSize = true
            };

            cmbEventType = new ComboBox
            {
                Location = new Point(440, ControlPadding + 25),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Standard,
                Font = new Font("Segoe UI", 9f)
            };

            PopulateEventComboBox();

            // Create Transaction Button
            btnCreateTransaction = CreateStyledButton(
                "Buat Transaksi",
                660, ControlPadding + 25,
                120, ButtonHeight,
                SuccessColor,
                (s, e) => CreateTransaction()
            );

            // Apply Event Button
            btnApplyEvent = CreateStyledButton(
                "Terapkan Event",
                790, ControlPadding + 25,
                120, ButtonHeight,
                SecondaryColor,
                (s, e) => ApplyEvent()
            );

            // Refresh Button
            btnRefresh = CreateStyledButton(
                "Refresh",
                920, ControlPadding + 25,
                100, ButtonHeight,
                WarningColor,
                (s, e) => LoadTransactions()
            );

            pnlControlPanel.Controls.AddRange(new Control[]
            {
                lblCustomerId, txtCustomerId,
                lblAmount, txtAmount,
                lblEvent, cmbEventType,
                btnCreateTransaction, btnApplyEvent, btnRefresh
            });

            Controls.Add(pnlControlPanel);
        }

        private Button CreateStyledButton(string text, int x, int y, int width, int height, Color backgroundColor, EventHandler onClick)
        {
            var button = new Button
            {
                Text = text,
                Location = new Point(x, y),
                Size = new Size(width, height),
                BackColor = backgroundColor,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                FlatAppearance = { BorderSize = 0 }
            };
            button.Click += onClick;
            button.MouseEnter += (s, e) => button.BackColor = DarkenColor(backgroundColor, 20);
            button.MouseLeave += (s, e) => button.BackColor = backgroundColor;
            return button;
        }

        private Color DarkenColor(Color color, int amount)
        {
            return Color.FromArgb(
                Math.Max(0, color.R - amount),
                Math.Max(0, color.G - amount),
                Math.Max(0, color.B - amount)
            );
        }

        private void PopulateEventComboBox()
        {
            var events = Enum.GetValues(typeof(EventPesanan))
                .Cast<EventPesanan>()
                .Select(e => e.ToString())
                .ToList();

            cmbEventType.DataSource = events;
        }

        private void CreateDataGridView()
        {
            dgvTransactions = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = true,
                MultiSelect = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(240, 240, 240)
                },
                BorderStyle = BorderStyle.Fixed3D,
                GridColor = Color.FromArgb(200, 200, 200)
            };

            dgvTransactions.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = PrimaryColor,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 9f, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };

            // Hidden column untuk menyimpan full GUID
            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderIdFull",
                HeaderText = "OrderIdFull",
                Width = 0,
                ReadOnly = true,
                Visible = false
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderId",
                HeaderText = "ID Pesanan",
                Width = 150,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CustomerId",
                HeaderText = "ID Pelanggan",
                Width = 150,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                HeaderText = "Jumlah (Rp)",
                Width = 150,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0", Alignment = DataGridViewContentAlignment.MiddleRight }
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 150,
                ReadOnly = true
            });

            dgvTransactions.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedAt",
                HeaderText = "Dibuat",
                Width = 180,
                ReadOnly = true
            });

            Controls.Add(dgvTransactions);
        }

        private void CreateStatusBar()
        {
            pnlStatusBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 35,
                BackColor = Color.FromArgb(240, 240, 240),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(ControlPadding, 5, ControlPadding, 5)
            };

            lblStatus = new Label
            {
                Text = "Siap",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 9f),
                ForeColor = TextColor
            };

            pnlStatusBar.Controls.Add(lblStatus);
            Controls.Add(pnlStatusBar);
        }

        private void LoadTransactions()
        {
            try
            {
                dgvTransactions.Rows.Clear();
                var transactions = _transactionService.AmbilSemua();

                foreach (var transaction in transactions)
                {
                    int rowIndex = dgvTransactions.Rows.Add(
                        transaction.Id.ToString(),
                        transaction.Id.ToString("D").Substring(0, 8) + "...",
                        transaction.CustomerId,
                        transaction.Amount,
                        transaction.Status.ToString(),
                        transaction.CreatedAtUtc.ToString("yyyy-MM-dd HH:mm:ss")
                    );

                    var statusCell = dgvTransactions.Rows[rowIndex].Cells["Status"];
                    statusCell.Style.ForeColor = GetStatusColor(transaction.Status);
                    statusCell.Style.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                }

                UpdateStatusBar($"Total transaksi: {transactions.Count}");
            }
            catch (Exception ex)
            {
                ShowError($"Gagal memuat transaksi: {ex.Message}");
                UpdateStatusBar("Gagal memuat data");
            }
        }

        private void CreateTransaction()
        {
            if (!ValidateInputs(out var customerId, out var amount))
                return;

            try
            {
                var transaction = _transactionService.BuatTransaksi(customerId, amount);
                ShowSuccess($"Transaksi berhasil dibuat.\nID: {transaction.Id}");
                ClearInputs();
                LoadTransactions();
                UpdateStatusBar("Transaksi baru berhasil dibuat");
            }
            catch (Exception ex)
            {
                ShowError($"Gagal membuat transaksi: {ex.Message}");
                UpdateStatusBar("Gagal membuat transaksi");
            }
        }

        private void ApplyEvent()
        {
            if (dgvTransactions.SelectedRows.Count == 0)
            {
                ShowWarning("Silakan pilih transaksi terlebih dahulu");
                return;
            }

            if (cmbEventType.SelectedItem == null)
            {
                ShowWarning("Silakan pilih event terlebih dahulu");
                return;
            }

            try
            {
                var selectedRow = dgvTransactions.SelectedRows[0];
                var orderIdStr = selectedRow.Cells["OrderIdFull"].Value?.ToString();

                if (!Guid.TryParse(orderIdStr, out var orderId))
                {
                    ShowError("ID pesanan tidak valid");
                    return;
                }

                if (!Enum.TryParse<EventPesanan>(cmbEventType.SelectedItem.ToString(), out var orderEvent))
                {
                    ShowError("Event tidak valid");
                    return;
                }

                var updatedTransaction = _transactionService.TerapkanEvent(orderId, orderEvent);
                ShowSuccess($"Status berhasil diubah menjadi: {updatedTransaction.Status}");
                LoadTransactions();
                UpdateStatusBar("Event berhasil diterapkan");
            }
            catch (InvalidOperationException ex)
            {
                ShowWarning($"Transisi tidak valid: {ex.Message}");
                UpdateStatusBar("Transisi tidak valid");
            }
            catch (Exception ex)
            {
                ShowError($"Gagal menerapkan event: {ex.Message}");
                UpdateStatusBar("Gagal menerapkan event");
            }
        }

        private bool ValidateInputs(out string customerId, out decimal amount)
        {
            customerId = txtCustomerId.Text?.Trim();
            amount = 0;

            if (string.IsNullOrWhiteSpace(customerId))
            {
                ShowWarning("ID pelanggan tidak boleh kosong");
                return false;
            }

            if (!decimal.TryParse(txtAmount.Text, out amount) || amount <= 0)
            {
                ShowWarning("Jumlah harus berupa angka positif");
                return false;
            }

            return true;
        }

        private void ClearInputs()
        {
            txtCustomerId.Clear();
            txtAmount.Clear();
            cmbEventType.SelectedIndex = 0;
        }

        private void UpdateStatusBar(string message)
        {
            lblStatus.Text = $"[{DateTime.Now:HH:mm:ss}] {message}";
        }

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private Color GetStatusColor(StatusPesanan status)
        {
            return status switch
            {
                StatusPesanan.Dibuat => Color.FromArgb(158, 158, 158),
                StatusPesanan.Dibayar => Color.FromArgb(33, 150, 243),
                StatusPesanan.Dikemas => Color.FromArgb(103, 58, 183),
                StatusPesanan.Dikirim => Color.FromArgb(255, 152, 0),
                StatusPesanan.Diterima => Color.FromArgb(76, 175, 80),
                StatusPesanan.Dibatalkan => Color.FromArgb(244, 67, 54),
                _ => TextColor
            };
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // TransactionManagementForm
            // 
            ClientSize = new Size(284, 261);
            Name = "TransactionManagementForm";
            Load += TransactionManagementForm_Load;
            ResumeLayout(false);
            // This is called by the designer pattern but we're building UI programmatically
        }

        private void TransactionManagementForm_Load(object sender, EventArgs e)
        {

        }
    }
}
