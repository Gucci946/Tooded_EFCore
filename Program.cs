
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

            // Создаём папки для фотографий и счетов
            Kaustad.Loo();

            // Проверяем и применяем миграции базы данных
            using (var db = new ToodedContext())
            {
                db.Database.Migrate();
            }

            // Открываем форму управления товарами
            Application.Run(new Form2());
        }
    }
}
