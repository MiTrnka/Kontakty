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


            // --- Přidání testovacích dat ---

            // Získáme si instanci našeho databázového kontextu
            var dbContext = new DatabaseContext();

            // Zkontrolujeme, zda v tabulce kontaktů nejsou žádné záznamy
            if (!dbContext.Contacts.Any())
            {
                // Pokud je tabulka prázdná, přidáme dva testovací kontakty
                dbContext.Contacts.Add(new Contact
                {
                    FirstName = "Jan",
                    LastName = "Novák",
                    Email = "jan.novak@email.cz",
                    PhoneNumber = "123 456 789",
                    DateOfBirth = new DateTime(1990, 1, 15),
                    Gender = Gender.Male
                });

                dbContext.Contacts.Add(new Contact
                {
                    FirstName = "Eva",
                    LastName = "Svobodová",
                    Email = "eva.svobodova@email.cz",
                    PhoneNumber = "987 654 321",
                    DateOfBirth = new DateTime(1985, 5, 20),
                    Gender = Gender.Female
                });

                // Uložíme změny do databáze
                dbContext.SaveChanges();
            }

            // --- Konec přidání testovacích dat ---

            return builder.Build();
        }
    }
}
