namespace PokeGamingStore.GUI
{
    partial class StockManagerForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgvStock = new System.Windows.Forms.DataGridView();
            this.grpEdit = new System.Windows.Forms.GroupBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblItemName = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtItemId = new System.Windows.Forms.TextBox();
            this.lblItemId = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).BeginInit();
            this.grpEdit.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(980, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(351, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Panel Manajemen Produk & Stok";
            this.pnlHeader.Controls.Add(this.lblTitle);
            // 
            // dgvStock
            // 
            this.dgvStock.AllowUserToAddRows = false;
            this.dgvStock.AllowUserToDeleteRows = false;
            this.dgvStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStock.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStock.Location = new System.Drawing.Point(15, 75);
            this.dgvStock.MultiSelect = false;
            this.dgvStock.Name = "dgvStock";
            this.dgvStock.ReadOnly = true;
            this.dgvStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStock.Size = new System.Drawing.Size(580, 480);
            this.dgvStock.TabIndex = 0;
            this.dgvStock.SelectionChanged += new System.EventHandler(this.DgvStock_SelectionChanged);
            // 
            // grpEdit
            // 
            this.grpEdit.Controls.Add(this.txtStock);
            this.grpEdit.Controls.Add(this.lblStock);
            this.grpEdit.Controls.Add(this.btnClear);
            this.grpEdit.Controls.Add(this.btnDelete);
            this.grpEdit.Controls.Add(this.btnUpdate);
            this.grpEdit.Controls.Add(this.btnCreate);
            this.grpEdit.Controls.Add(this.txtPrice);
            this.grpEdit.Controls.Add(this.lblPrice);
            this.grpEdit.Controls.Add(this.txtName);
            this.grpEdit.Controls.Add(this.lblItemName);
            this.grpEdit.Controls.Add(this.cmbCategory);
            this.grpEdit.Controls.Add(this.lblCategory);
            this.grpEdit.Controls.Add(this.txtItemId);
            this.grpEdit.Controls.Add(this.lblItemId);
            this.grpEdit.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.grpEdit.Location = new System.Drawing.Point(610, 75);
            this.grpEdit.Name = "grpEdit";
            this.grpEdit.Size = new System.Drawing.Size(320, 480);
            this.grpEdit.TabIndex = 2;
            this.grpEdit.TabStop = false;
            this.grpEdit.Text = "Form Operasi Produk";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(15, 225);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(290, 23);
            this.txtStock.TabIndex = 12;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(15, 207);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(79, 15);
            this.lblStock.TabIndex = 11;
            this.lblStock.Text = "Jumlah Stok:";
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
            this.btnClear.Location = new System.Drawing.Point(15, 310);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(290, 30);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(15, 435);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(290, 30);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Hapus Produk";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(15, 395);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(290, 30);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Simpan Perubahan";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.ForeColor = System.Drawing.Color.White;
            this.btnCreate.Location = new System.Drawing.Point(15, 355);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(290, 32);
            this.btnCreate.TabIndex = 7;
            this.btnCreate.Text = "Tambah Produk Baru";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.BtnCreate_Click);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(15, 175);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(290, 23);
            this.txtPrice.TabIndex = 6;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(15, 155);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(82, 15);
            this.lblPrice.TabIndex = 5;
            this.lblPrice.Text = "Harga Produk:";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(15, 115);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(290, 23);
            this.txtName.TabIndex = 4;
            // 
            // lblItemName
            // 
            this.lblItemName.AutoSize = true;
            this.lblItemName.Location = new System.Drawing.Point(15, 95);
            this.lblItemName.Name = "lblItemName";
            this.lblItemName.Size = new System.Drawing.Size(82, 15);
            this.lblItemName.TabIndex = 3;
            this.lblItemName.Text = "Nama Produk:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Items.AddRange(new object[] {
            "GAME",
            "ACCESSORY",
            "CONSOLE",
            "GEAR"});
            this.cmbCategory.Location = new System.Drawing.Point(15, 270);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(290, 23);
            this.cmbCategory.TabIndex = 3;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(15, 252);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(54, 15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Kategori:";
            // 
            // txtItemId
            // 
            this.txtItemId.Location = new System.Drawing.Point(15, 55);
            this.txtItemId.Name = "txtItemId";
            this.txtItemId.Size = new System.Drawing.Size(290, 23);
            this.txtItemId.TabIndex = 1;
            // 
            // lblItemId
            // 
            this.lblItemId.AutoSize = true;
            this.lblItemId.Location = new System.Drawing.Point(15, 35);
            this.lblItemId.Name = "lblItemId";
            this.lblItemId.Size = new System.Drawing.Size(62, 15);
            this.lblItemId.TabIndex = 0;
            this.lblItemId.Text = "ID Produk:";
            // 
            // StockManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(954, 571);
            this.Controls.Add(this.grpEdit);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.dgvStock);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.Name = "StockManagerForm";
            this.Text = "StockManagerForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStock)).EndInit();
            this.grpEdit.ResumeLayout(false);
            this.grpEdit.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvStock;
        private System.Windows.Forms.GroupBox grpEdit;
        private System.Windows.Forms.Label lblItemId;
        private System.Windows.Forms.TextBox txtItemId;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblItemName;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblStock;
    }
}