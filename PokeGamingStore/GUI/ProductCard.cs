using System;
using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Catalog;

namespace PokeGamingStore.GUI
{
    public class ProductCard : UserControl
    {
        // Event ini akan ditangkap oleh CatalogForm saat tombol ditekan
        public event EventHandler AddToCartClicked;
        public event EventHandler BuyNowClicked;

        public ProductCard(Product p)
        {
            this.Size = new Size(200, 190);
            this.BackColor = Color.White;
            this.Margin = new Padding(6);
            this.BorderStyle = BorderStyle.FixedSingle;

            var lblCat = new Label
            {
                Text = p.Category?.ToUpper() ?? "",
                Dock = DockStyle.Top,
                Height = 20,
                Padding = new Padding(10, 10, 0, 0),
                Font = new Font("Segoe UI", 7.5f, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.White
            };

            var lblName = new Label
            {
                Text = p.Name ?? "-",
                Dock = DockStyle.Top,
                Height = 52,
                Padding = new Padding(10, 4, 10, 0),
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.Black,
                BackColor = Color.White
            };

            var lblPrice = new Label
            {
                Text = $"Rp {p.Price:N0}",
                Dock = DockStyle.Top,
                Height = 24,
                Padding = new Padding(10, 0, 0, 0),
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                BackColor = Color.White
            };

            var pnlBot = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 65,
                BackColor = Color.White
            };

            var lblId = new Label
            {
                Text = p.Id ?? "",
                Location = new Point(10, 5),
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = Color.Gray
            };

            var btnCart = new Button
            {
                Text = "+ Keranjang",
                Size = new Size(85, 26),
                Location = new Point(10, 30),
                FlatStyle = FlatStyle.Standard,
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand
            };
            btnCart.Click += (s, e) => AddToCartClicked?.Invoke(this, EventArgs.Empty);

            var btnBuyNow = new Button
            {
                Text = "+ Beli",
                Size = new Size(88, 26),
                Location = new Point(100, 30),
                FlatStyle = FlatStyle.Standard,
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand
            };
            btnBuyNow.Click += (s, e) => BuyNowClicked?.Invoke(this, EventArgs.Empty);

            pnlBot.Controls.AddRange(new Control[] { lblId, btnCart, btnBuyNow });

            this.Controls.Add(pnlBot);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblName);
            this.Controls.Add(lblCat);
        }
    }
}