
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore;

public partial class Form3 : Form
{
    private readonly ToodedContext _db = new();
    private readonly TabControl tabPood = new();

    public Form3()
    {
        InitializeComponent();

        Text = "Pood";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(930, 630);

        tabPood.Dock = DockStyle.Fill;
        Controls.Add(tabPood);

        LaePood();
    }

    // Загрузка категорий и товаров магазина
    private void LaePood()
    {
        tabPood.TabPages.Clear();

        var kategooriad = _db.Kategooriatabel
            .Include(k => k.Tooted)
            .OrderBy(k => k.KategooriaNimetus)
            .ToList();

        foreach (var kat in kategooriad)
        {
            var leht = new TabPage(kat.KategooriaNimetus)
            {
                AutoScroll = true
            };

            int i = 0;

            foreach (var toode in kat.Tooted
                         .OrderBy(t => t.Toodenimetus))
            {
                var kaart = LooTootekaart(toode);

                // По 5 карточек в одном ряду
                kaart.Location = new Point(
                    10 + (i % 5) * 175,
                    10 + (i / 5) * 250);

                leht.Controls.Add(kaart);
                i++;
            }

            tabPood.TabPages.Add(leht);
        }
    }

    // Создание карточки товара
    private Panel LooTootekaart(Toode t)
    {
        var kaart = new Panel
        {
            Size = new Size(160, 235),
            BorderStyle = BorderStyle.FixedSingle
        };

        var pilt = new PictureBox
        {
            Bounds = new Rectangle(5, 5, 148, 140),
            SizeMode = PictureBoxSizeMode.Zoom,
            Image = Kaustad.LaePilt(t.Pilt)
        };

        var silt = new Label
        {
            Bounds = new Rectangle(5, 150, 148, 40),
            TextAlign = ContentAlignment.MiddleCenter,
            Text = $"{t.Toodenimetus}\n{t.Hind:F2} €"
        };

        var kogus = new NumericUpDown
        {
            Bounds = new Rectangle(5, 197, 60, 27),
            Minimum = 1,
            Maximum = Math.Max(1, t.Kogus),
            Value = 1
        };

        var nupp = new Button
        {
            Bounds = new Rectangle(70, 195, 83, 30),
            Text = "Lisa korvi",
            Enabled = t.Kogus > 0
        };

        // Добавление товара в общую корзину
        nupp.Click += (s, e) =>
        {
            int valitudKogus = (int)kogus.Value;

            Ostukorv.Lisa(t, valitudKogus);

            MessageBox.Show(
                $"{t.Toodenimetus} ({valitudKogus} tk) " +
                "lisati ostukorvi.",
                "Ostukorv",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        };

        // Информация о товаре при нажатии на изображение
        pilt.Click += (s, e) =>
        {
            MessageBox.Show(
                $"{t.Toodenimetus}\n" +
                $"Hind: {t.Hind:F2} €\n" +
                $"Laos: {t.Kogus} tk",
                "Toote info");
        };

        kaart.Controls.AddRange(new Control[]
        {
            pilt,
            silt,
            kogus,
            nupp
        });

        return kaart;
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        _db.Dispose();
        base.OnFormClosed(e);
    }
}
