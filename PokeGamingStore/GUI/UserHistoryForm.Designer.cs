namespace PokeGamingStore.GUI
{
    partial class UserHistoryForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblDaftarUser = new System.Windows.Forms.Label();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.lblHistory = new System.Windows.Forms.Label();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.groupBoxForm = new System.Windows.Forms.GroupBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPasswordBaru = new System.Windows.Forms.Label();
            this.txtPasswordBaru = new System.Windows.Forms.TextBox();
            this.btnSimpanPassword = new System.Windows.Forms.Button();
            this.labelGaris = new System.Windows.Forms.Label();
            this.lblCariUser = new System.Windows.Forms.Label();
            this.txtCariUserId = new System.Windows.Forms.TextBox();
            this.btnCariHistori = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.groupBoxForm.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Size = new System.Drawing.Size(800, 70);

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Text = "Panel Manajemen User && Histori";

            // pnlContent
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlContent.Controls.Add(this.lblDaftarUser);
            this.pnlContent.Controls.Add(this.dgvUsers);
            this.pnlContent.Controls.Add(this.lblHistory);
            this.pnlContent.Controls.Add(this.dgvHistory);
            this.pnlContent.Controls.Add(this.groupBoxForm);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 70);

            // dgvUsers
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.BackgroundColor = System.Drawing.Color.White;
            this.dgvUsers.Location = new System.Drawing.Point(20, 40);
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.Size = new System.Drawing.Size(460, 180);
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);

            // lblDaftarUser
            this.lblDaftarUser.AutoSize = true;
            this.lblDaftarUser.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDaftarUser.Location = new System.Drawing.Point(20, 15);
            this.lblDaftarUser.Text = "Daftar User";

            // dgvHistory
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AllowUserToDeleteRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.Location = new System.Drawing.Point(20, 260);
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.Size = new System.Drawing.Size(460, 180);

            // lblHistory
            this.lblHistory.AutoSize = true;
            this.lblHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistory.Location = new System.Drawing.Point(20, 235);
            this.lblHistory.Text = "Histori Pembelian";

            // groupBoxForm
            this.groupBoxForm.BackColor = System.Drawing.Color.White;
            this.groupBoxForm.Controls.Add(this.lblUsername);
            this.groupBoxForm.Controls.Add(this.txtUsername);
            this.groupBoxForm.Controls.Add(this.lblPasswordBaru);
            this.groupBoxForm.Controls.Add(this.txtPasswordBaru);
            this.groupBoxForm.Controls.Add(this.btnSimpanPassword);
            this.groupBoxForm.Controls.Add(this.labelGaris);
            this.groupBoxForm.Controls.Add(this.lblCariUser);
            this.groupBoxForm.Controls.Add(this.txtCariUserId);
            this.groupBoxForm.Controls.Add(this.btnCariHistori);
            this.groupBoxForm.Location = new System.Drawing.Point(500, 20);
            this.groupBoxForm.Size = new System.Drawing.Size(270, 420);
            this.groupBoxForm.Text = "Form Operasi";

            // lblUsername
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(15, 35);
            this.lblUsername.Text = "User Terpilih:";

            // txtUsername
            this.txtUsername.Location = new System.Drawing.Point(15, 55);
            this.txtUsername.Size = new System.Drawing.Size(235, 23);
            this.txtUsername.ReadOnly = true;
            this.txtUsername.BackColor = System.Drawing.Color.WhiteSmoke;

            // lblPasswordBaru
            this.lblPasswordBaru.AutoSize = true;
            this.lblPasswordBaru.Location = new System.Drawing.Point(15, 90);
            this.lblPasswordBaru.Text = "Password Baru:";

            // txtPasswordBaru
            this.txtPasswordBaru.Location = new System.Drawing.Point(15, 110);
            this.txtPasswordBaru.Size = new System.Drawing.Size(235, 23);
            this.txtPasswordBaru.UseSystemPasswordChar = true;

            // btnSimpanPassword
            this.btnSimpanPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnSimpanPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSimpanPassword.ForeColor = System.Drawing.Color.White;
            this.btnSimpanPassword.Location = new System.Drawing.Point(15, 150);
            this.btnSimpanPassword.Size = new System.Drawing.Size(235, 35);
            this.btnSimpanPassword.Text = "Simpan Password Baru";
            this.btnSimpanPassword.UseVisualStyleBackColor = false;
            this.btnSimpanPassword.Click += new System.EventHandler(this.btnSimpanPassword_Click);

            // labelGaris
            this.labelGaris.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelGaris.Location = new System.Drawing.Point(15, 210);
            this.labelGaris.Size = new System.Drawing.Size(235, 2);

            // lblCariUser
            this.lblCariUser.AutoSize = true;
            this.lblCariUser.Location = new System.Drawing.Point(15, 235);
            this.lblCariUser.Text = "Masukkan ID Pelanggan:";

            // txtCariUserId
            this.txtCariUserId.Location = new System.Drawing.Point(15, 255);
            this.txtCariUserId.Size = new System.Drawing.Size(235, 23);

            // btnCariHistori
            this.btnCariHistori.BackColor = System.Drawing.Color.LightGray;
            this.btnCariHistori.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCariHistori.Location = new System.Drawing.Point(15, 295);
            this.btnCariHistori.Size = new System.Drawing.Size(235, 35);
            this.btnCariHistori.Text = "Cari Histori Pembelian";
            this.btnCariHistori.UseVisualStyleBackColor = false;
            this.btnCariHistori.Click += new System.EventHandler(this.btnCariHistori_Click);

            // UserHistoryForm
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Text = "Manajemen User & Histori";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.groupBoxForm.ResumeLayout(false);
            this.groupBoxForm.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblDaftarUser;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.Label lblHistory;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.GroupBox groupBoxForm;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPasswordBaru;
        private System.Windows.Forms.TextBox txtPasswordBaru;
        private System.Windows.Forms.Button btnSimpanPassword;
        private System.Windows.Forms.Label labelGaris;
        private System.Windows.Forms.Label lblCariUser;
        private System.Windows.Forms.TextBox txtCariUserId;
        private System.Windows.Forms.Button btnCariHistori;
    }
}