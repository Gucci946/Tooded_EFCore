
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Windows.Forms;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Tooded_EFCore;

public partial class ArveForm : Form
{
    private readonly ListBox listOstukorv = new();
    private readonly Label lblKokku = new();
    private readonly TextBox txtEmail = new();

    private readonly Button btnEemalda = new();
    private readonly Button btnGenPDF = new();
    private readonly Button btnSaadaEmail = new();

    private string? viimanePdf;

    public ArveForm()
    {
        Text = "Ostukorv ja arve";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(600, 380);

        listOstukorv.SetBounds(20, 20, 330, 300);

        lblKokku.SetBounds(20, 330, 330, 30);
        lblKokku.Font = new Font(Font, FontStyle.Bold);

        txtEmail.SetBounds(370, 20, 200, 27);
        txtEmail.PlaceholderText = "Saaja e-post";

        SeaNupp(btnEemalda, "Eemalda valitud rida", 70,
            btnEemalda_Click);

        SeaNupp(btnGenPDF, "Genereeri PDF", 115,
            btnGenPDF_Click);

        SeaNupp(btnSaadaEmail, "Saada e-postiga", 160,
            btnSaadaEmail_Click);

        Controls.AddRange(new Control[]
        {
            listOstukorv,
            lblKokku,
            txtEmail,
            btnEemalda,
            btnGenPDF,
            btnSaadaEmail
        });

        NaitaKorvi();
    }

    private void SeaNupp(
        Button nupp,
        string tekst,
        int y,
        EventHandler kasitleja)
    {
        nupp.Text = tekst;
        nupp.SetBounds(370, y, 200, 35);
        nupp.Click += kasitleja;
    }

    private void NaitaKorvi()
    {
        listOstukorv.Items.Clear();

        foreach (var rida in Ostukorv.Ridad)
            listOstukorv.Items.Add(rida);

        lblKokku.Text = $"Kokku: {Ostukorv.Kokku:F2} €";
    }

    private void btnEemalda_Click(object? sender, EventArgs e)
    {
        if (listOstukorv.SelectedItem is OstukorviRida rida)
        {
            Ostukorv.Ridad.Remove(rida);
            viimanePdf = null;
            NaitaKorvi();
        }
        else
        {
            MessageBox.Show("Vali ostukorvist rida.");
        }
    }

    private void btnGenPDF_Click(object? sender, EventArgs e)
    {
        if (Ostukorv.Ridad.Count == 0)
        {
            MessageBox.Show("Ostukorv on tühi.");
            return;
        }

        try
        {
            Kaustad.Loo();

            viimanePdf = Path.Combine(
                Kaustad.Arved,
                $"Arve_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");

            using var dokument = new PdfDocument();
            PdfPage leht = dokument.AddPage();

            using (XGraphics gfx = XGraphics.FromPdfPage(leht))
            {
                var pealkiri = new XFont(
                    "Arial", 20, XFontStyle.Bold);

                var tavaline = new XFont(
                    "Arial", 12, XFontStyle.Regular);

                var paks = new XFont(
                    "Arial", 14, XFontStyle.Bold);

                gfx.DrawString(
                    "ARVE",
                    pealkiri,
                    XBrushes.Black,
                    new XRect(0, 30, leht.Width, 30),
                    XStringFormats.TopCenter);

                gfx.DrawString(
                    $"Kuupäev: {DateTime.Now:dd.MM.yyyy HH:mm}",
                    tavaline,
                    XBrushes.Black,
                    40,
                    90);

                double y = 130;

                foreach (var rida in Ostukorv.Ridad)
                {
                    gfx.DrawString(
                        rida.ToString(),
                        tavaline,
                        XBrushes.Black,
                        40,
                        y);

                    y += 22;
                }

                gfx.DrawString(
                    $"Kokku: {Ostukorv.Kokku:F2} €",
                    paks,
                    XBrushes.Black,
                    40,
                    y + 20);
            }

            dokument.Save(viimanePdf);

            MessageBox.Show(
                $"PDF on loodud:\n{viimanePdf}",
                "Arve valmis");
        }
        catch (Exception ex)
        {
            viimanePdf = null;
            MessageBox.Show("PDF loomise viga:\n" + ex.Message);
        }
    }

    private void btnSaadaEmail_Click(object? sender, EventArgs e)
    {
        if (viimanePdf == null || !File.Exists(viimanePdf))
        {
            MessageBox.Show("Genereeri enne PDF.");
            return;
        }

        string saaja = txtEmail.Text.Trim();

        if (!MailAddress.TryCreate(saaja, out _))
        {
            MessageBox.Show("Sisesta korrektne e-posti aadress.");
            return;
        }

        try
        {
            string teema = Uri.EscapeDataString("Arve");

            string sisu = Uri.EscapeDataString(
                "Tere!\n\nSaadan teile arve.\n\n" +
                "Palun lisa PDF-arve kirjale manusena.\n\n" +
                "Parimate soovidega");

            string url =
                $"mailto:{saaja}?subject={teema}&body={sisu}";

            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });

            MessageBox.Show(
                "E-kirja aken avati.\n\nLisa PDF manusena:\n" +
                viimanePdf +
                "\n\nSeejärel saada kiri ise.");
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "E-kirja avamine ebaõnnestus:\n" + ex.Message);
        }
    }
}
