using System.Drawing;
using System.Windows.Forms;
using PokeGamingStore.Catalog;

namespace PokeGamingStore.GUI
{
    public class ProductCard : UserControl
    {
        public ProductCard(Product p)
        {
            this.Size = new Size(200, 165);
            this.BackColor = Color.White;
            this.Margin = new Padding(6);
            this.Cursor = Cursors.Hand;
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
                Height = 34,
                BackColor = Color.White
            };

            var lblId = new Label
            {
                Text = p.Id ?? "",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 7.5f),
                ForeColor = Color.Gray
            };

            var btnBuy = new Button
            {
                Text = "+ Beli",
                Size = new Size(58, 24),
                Location = new Point(122, 5),
                FlatStyle = FlatStyle.Standard,
                Font = new Font("Segoe UI", 8f),
                Cursor = Cursors.Hand
            };
            btnBuy.Click += (s, e) =>
                MessageBox.Show($"{p.Name} ditambahkan ke keranjang!");

            pnlBot.Controls.AddRange(new Control[] { lblId, btnBuy });

            this.Controls.Add(pnlBot);
            this.Controls.Add(lblPrice);
            this.Controls.Add(lblName);
            this.Controls.Add(lblCat);
        }
    }
}