
using Microsoft.EntityFrameworkCore;

namespace Tooded_EFCore;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Создаём папки для фотографий и PDF-счетов.
        Kaustad.Loo();

        // Применяем миграции базы данных.
        using (var db = new ToodedContext())
        {
            db.Database.Migrate();
        }

        // Открываем главное меню.
        Application.Run(new FormMain());
    }
}
