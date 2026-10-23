using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "Tooted - EF Core 8";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(500, 250);

            var statusLabel = new Label
            {
                AutoSize = true,
                Location = new Point(30, 30),
                Font = new Font("Arial", 12),
                Text = "Andmebaas on loodud!"
            };

            var infoLabel = new Label
            {
                AutoSize = true,
                Location = new Point(30, 80),
                Font = new Font("Arial", 11)
            };

            Controls.Add(statusLabel);
            Controls.Add(infoLabel);

            using (var db = new AppDbContext())
            {
                int toodeteArv = db.Tooted.Count();
                int kategooriateArv = db.Kategooriad.Count();

                infoLabel.Text =
                    $"Tooteid: {toodeteArv}\nKategooriaid: {kategooriateArv}";
            }
        }
    }
}