using Microsoft.Extensions.Logging;

namespace Kontakty
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // Registrace databázového kontextu. AddSingleton znamená,
            // že se v celé aplikaci vytvoří pouze jedna instance.
            // Tento řádek zůstává.
            builder.Services.AddSingleton<DatabaseContext>();

            // Registrace View a ViewModelů pro seznam kontaktů.
            // AddTransient znamená, že se vždy vytvoří nová instance, když je potřeba.
            builder.Services.AddTransient<ContactsListPage>();
            builder.Services.AddTransient<ContactsListViewModel>();

            // Registrace View a ViewModelů pro detail kontaktu.
            builder.Services.AddTransient<ContactDetailPage>();
            builder.Services.AddTransient<ContactDetailViewModel>();

            return builder.Build();
        }
    }
}
