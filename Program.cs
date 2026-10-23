using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var db = new ToodedContext())
            {
                db.Database.Migrate();
            }

            Application.Run(new Form1());
        }
    }
}