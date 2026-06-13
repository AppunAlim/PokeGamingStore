namespace PokeGamingStore.GUI
{
    partial class PaymentForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTotalTagihan = new System.Windows.Forms.Label();
            this.grpMethod = new System.Windows.Forms.GroupBox();
            this.rdbOvo = new System.Windows.Forms.RadioButton();
            this.rdbQris = new System.Windows.Forms.RadioButton();
            this.btnConfirmPayment = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpMethod.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12f, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(163, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Sistem Pembayaran";
            // 
            // lblTotalTagihan
            // 
            this.lblTotalTagihan.AutoSize = true;
            this.lblTotalTagihan.Font = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTotalTagihan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblTotalTagihan.Location = new System.Drawing.Point(20, 55);
            this.lblTotalTagihan.Name = "lblTotalTagihan";
            this.lblTotalTagihan.Size = new System.Drawing.Size(166, 25);
            this.lblTotalTagihan.TabIndex = 1;
            this.lblTotalTagihan.Text = "Total Tagihan: Rp0";
            // 
            // grpMethod
            // 
            this.grpMethod.Controls.Add(this.rdbOvo);
            this.grpMethod.Controls.Add(this.rdbQris);
            this.grpMethod.Location = new System.Drawing.Point(25, 100);
            this.grpMethod.Name = "grpMethod";
            this.grpMethod.Size = new System.Drawing.Size(335, 110);
            this.grpMethod.TabIndex = 2;
            this.grpMethod.TabStop = false;
            this.grpMethod.Text = "Pilih Metode Pembayaran";
            // 
            // rdbOvo
            // 
            this.rdbOvo.AutoSize = true;
            this.rdbOvo.Location = new System.Drawing.Point(20, 65);
            this.rdbOvo.Name = "rdbOvo";
            this.rdbOvo.Size = new System.Drawing.Size(147, 19);
            this.rdbOvo.TabIndex = 1;
            this.rdbOvo.Text = "Transfer Bank";
            this.rdbOvo.UseVisualStyleBackColor = true;
            // 
            // rdbQris
            // 
            this.rdbQris.AutoSize = true;
            this.rdbQris.Checked = true;
            this.rdbQris.Location = new System.Drawing.Point(20, 30);
            this.rdbQris.Name = "rdbQris";
            this.rdbQris.Size = new System.Drawing.Size(155, 19);
            this.rdbQris.TabIndex = 0;
            this.rdbQris.TabStop = true;
            this.rdbQris.Text = "QRIS";
            this.rdbQris.UseVisualStyleBackColor = true;
            // 
            // btnConfirmPayment
            // 
            this.btnConfirmPayment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnConfirmPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmPayment.ForeColor = System.Drawing.Color.White;
            this.btnConfirmPayment.Location = new System.Drawing.Point(210, 230);
            this.btnConfirmPayment.Name = "btnConfirmPayment";
            this.btnConfirmPayment.Size = new System.Drawing.Size(150, 35);
            this.btnConfirmPayment.TabIndex = 3;
            this.btnConfirmPayment.Text = "Konfirmasi & Bayar";
            this.btnConfirmPayment.UseVisualStyleBackColor = false;
            this.btnConfirmPayment.Click += new System.EventHandler(this.BtnConfirmPayment_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(25, 230);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Batal";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirmPayment);
            this.Controls.Add(this.grpMethod);
            this.Controls.Add(this.lblTotalTagihan);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pembayaran - PokeGamingStore";
            this.grpMethod.ResumeLayout(false);
            this.grpMethod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTotalTagihan;
        private System.Windows.Forms.GroupBox grpMethod;
        private System.Windows.Forms.RadioButton rdbOvo;
        private System.Windows.Forms.RadioButton rdbQris;
        private System.Windows.Forms.Button btnConfirmPayment;
        private System.Windows.Forms.Button btnCancel;
    }
}