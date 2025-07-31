using Microsoft.EntityFrameworkCore; // Přidáme using pro EF Core
using System.ComponentModel;
using System.Windows.Input;

namespace Kontakty;

public class ContactDetailViewModel : ViewModelBase
{
    private readonly DatabaseContext _context;
    private Contact _contact; // Pole pro uchování načteného kontaktu

    // --- Vlastnosti pro binding do UI (zatím jen jméno a příjmení) ---
    private string _firstName;
    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    private string _lastName;
    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    private DateTime _dateOfBirth;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => SetProperty(ref _dateOfBirth, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand DeleteCommand { get; }

    // Konstruktor si vyžádá DatabaseContext
    public ContactDetailViewModel(DatabaseContext context)
    {
        _context = context;
        _contact = new Contact();

        SaveCommand = new Command(async (param) => await SaveContactAsync());
        DeleteCommand = new Command(async (param) => await DeleteContactAsync());
    }

    /// <summary>
    /// Zpracovává parametry předané během navigace na tuto stránku.
    /// </summary>
    /// <param name="idValue">Hodnota parametru "id" z navigační adresy, přijatá jako string.</param>
    /// <remarks>
    /// Tato metoda je klíčová pro propojení navigace a ViewModelu.
    /// Jelikož atribut [QueryProperty] musí být na třídě stránky (Page) a ne na ViewModelu,
    /// stránka ContactDetailPage přijme ID a prostřednictvím této metody ho předá ViewModelu ke zpracování.
    /// Metoda je volána ze setteru vlastnosti 'ContactId' v souboru ContactDetailPage.xaml.cs.
    ///
    /// Logika:
    /// - Pokud je 'idValue' platné číslo a je větší než 0, načte existujícího kontaktu z databáze.
    /// - Pokud je 'idValue' null, prázdné nebo 0, připraví ViewModel pro vytvoření nového kontaktu.
    /// </remarks>
    public void ApplyQueryAttributes(string idValue)
    {
        // Pokusíme se převést přijatou hodnotu na číslo.
        if (!string.IsNullOrEmpty(idValue) && int.TryParse(idValue, out int id))
        {
            // Pokud se to povedlo, načteme existující kontakt.
            LoadContactAsync(id);
        }
        else
        {
            // Pokud ID nepřišlo (navigace na nový kontakt),
            // připravíme prázdný formulář.
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now; // Nastavíme aktuální datum narození
        }
    }

    // Metoda pro načtení dat z databáze
    private async void LoadContactAsync(int id)
    {
        // Najdeme kontakt v databázi podle ID
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _contact = contact; // Uložíme si načtený model

            // Zkopírujeme hodnoty do vlastností, které jsou nabindované na UI
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            DateOfBirth = contact.DateOfBirth;
        }
    }

    private async Task SaveContactAsync()
    {
        // 1. Jednoduchá validace, aby nešel uložit prázdný kontakt
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            await Shell.Current.DisplayAlert("Chyba", "Jméno a příjmení musí být vyplněno.", "OK");
            return;
        }

        // 2. Zkopírujeme hodnoty z formuláře do našeho modelu _contact
        _contact.FirstName = FirstName;
        _contact.LastName = LastName;
        _contact.DateOfBirth = DateOfBirth;
        // Zde budeme doplňovat další vlastnosti...

        // 3. Klíčová část: Rozhodneme, zda přidáváme nový, nebo upravujeme existující
        if (_contact.Id == 0)
        {
            // Pokud je Id 0, jedná se o NOVÝ kontakt.
            // Použijeme metodu Add, která řekne EF Core, aby vygeneroval INSERT.
            _context.Contacts.Add(_contact);
        }
        else
        {
            // Pokud je Id jiné než 0, jde o EDITACI existujícího kontaktu.
            // Použijeme metodu Update pro vygenerování UPDATE.
            _context.Contacts.Update(_contact);
        }

        // Uložíme změny do databáze
        await _context.SaveChangesAsync();

        // Vrátíme se na seznam
        await Shell.Current.GoToAsync("..");
    }

    private async Task DeleteContactAsync()
    {
        // Ujistíme se, že máme co mazat
        if (_contact != null && _contact.Id != 0)
        {
            _context.Contacts.Remove(_contact);
            await _context.SaveChangesAsync();
        }

        // Po smazání se vrátíme na seznam
        await Shell.Current.GoToAsync("..");
    }
}