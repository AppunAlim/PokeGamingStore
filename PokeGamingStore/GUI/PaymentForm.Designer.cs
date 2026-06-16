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
            this.lblTotalPayment = new System.Windows.Forms.Label();
            this.pnlPaymentDetails = new System.Windows.Forms.Panel();
            this.lblSelectMethod = new System.Windows.Forms.Label();
            this.rbQris = new System.Windows.Forms.RadioButton();
            this.rbTransfer = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lblTotalPayment
            // 
            this.lblTotalPayment.AutoSize = true;
            this.lblTotalPayment.Font = new System.Drawing.Font("Segoe UI", 13f, System.Drawing.FontStyle.Bold);
            this.lblTotalPayment.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblTotalPayment.Location = new System.Drawing.Point(15, 15);
            this.lblTotalPayment.Name = "lblTotalPayment";
            this.lblTotalPayment.Size = new System.Drawing.Size(193, 25);
            this.lblTotalPayment.TabIndex = 0;
            this.lblTotalPayment.Text = "Total Tagihan: Rp 0";
            // 
            // pnlPaymentDetails
            // 
            this.pnlPaymentDetails.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPaymentDetails.Location = new System.Drawing.Point(15, 125);
            this.pnlPaymentDetails.Name = "pnlPaymentDetails";
            this.pnlPaymentDetails.Size = new System.Drawing.Size(390, 310);
            this.pnlPaymentDetails.TabIndex = 3;
            // 
            // lblSelectMethod
            // 
            this.lblSelectMethod.AutoSize = true;
            this.lblSelectMethod.Font = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.lblSelectMethod.Location = new System.Drawing.Point(15, 55);
            this.lblSelectMethod.Name = "lblSelectMethod";
            this.lblSelectMethod.Size = new System.Drawing.Size(150, 17);
            this.lblSelectMethod.TabIndex = 4;
            this.lblSelectMethod.Text = "Pilih Cara Pembayaran:";
            // 
            // rbQris
            // 
            this.rbQris.AutoSize = true;
            this.rbQris.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.rbQris.Location = new System.Drawing.Point(20, 85);
            this.rbQris.Name = "rbQris";
            this.rbQris.Size = new System.Drawing.Size(97, 21);
            this.rbQris.TabIndex = 5;
            this.rbQris.TabStop = true;
            this.rbQris.Text = "QRIS Digital";
            this.rbQris.UseVisualStyleBackColor = true;
            // 
            // rbTransfer
            // 
            this.rbTransfer.AutoSize = true;
            this.rbTransfer.Font = new System.Drawing.Font("Segoe UI", 9.5f);
            this.rbTransfer.Location = new System.Drawing.Point(140, 85);
            this.rbTransfer.Name = "rbTransfer";
            this.rbTransfer.Size = new System.Drawing.Size(193, 21);
            this.rbTransfer.TabIndex = 6;
            this.rbTransfer.TabStop = true;
            this.rbTransfer.Text = "Bank Transfer (Virtual Account)";
            this.rbTransfer.UseVisualStyleBackColor = true;
            // 
            // PaymentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7f, 15f);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 451);
            this.Controls.Add(this.rbTransfer);
            this.Controls.Add(this.rbQris);
            this.Controls.Add(this.lblSelectMethod);
            this.Controls.Add(this.pnlPaymentDetails);
            this.Controls.Add(this.lblTotalPayment);
            this.Font = new System.Drawing.Font("Segoe UI", 9f);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PaymentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Metode Pembayaran Toko";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTotalPayment;
        private System.Windows.Forms.Panel pnlPaymentDetails;
        private System.Windows.Forms.Label lblSelectMethod;
        private System.Windows.Forms.RadioButton rbQris;
        private System.Windows.Forms.RadioButton rbTransfer;
    }
}