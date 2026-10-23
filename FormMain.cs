
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tooded_EFCore;

public partial class FormMain : Form
{
    private readonly Button btnTooted = new();
    private readonly Button btnPood = new();
    private readonly Button btnArve = new();

    public FormMain()
    {
        Text = "Peamenüü";
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(260, 220);

        SeaNupp(btnTooted, "Tootehaldus", 30,
            (s, e) => Ava(new Form2()));

        SeaNupp(btnPood, "Pood", 80,
            (s, e) => Ava(new Form3()));

        SeaNupp(btnArve, "Ostukorv ja arve", 130,
            (s, e) => Ava(new ArveForm()));

        Controls.AddRange(new Control[]
        {
            btnTooted,
            btnPood,
            btnArve
        });
    }

    private void SeaNupp(
        Button nupp,
        string tekst,
        int y,
        EventHandler kasitleja)
    {
        nupp.Text = tekst;
        nupp.SetBounds(30, y, 200, 40);
        nupp.Click += kasitleja;
    }

    private static void Ava(Form vorm)
    {
        using (vorm)
        {
            vorm.ShowDialog();
        }
    }
}
