namespace PokeGamingStore.GUI
{
    partial class CatalogForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // CatalogForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(983, 827);
            Margin = new Padding(3, 4, 3, 4);
            Name = "CatalogForm";
            Text = "PokeGamingStore";
            Load += CatalogForm_Load;
            ResumeLayout(false);
        }
    }
}