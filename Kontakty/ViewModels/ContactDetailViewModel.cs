using Microsoft.EntityFrameworkCore; // Přidáme using pro EF Core
using System.ComponentModel;
using System.Windows.Input;

namespace Kontakty;

// Přidáme atribut QueryProperty. Říká MAUI, aby hodnotu z query parametru "id"
// zapsal do naší veřejné vlastnosti "ContactId".
[QueryProperty(nameof(ContactId), "id")]
public class ContactDetailViewModel : ViewModelBase
{
    private readonly DatabaseContext _context;
    private int _contactId;
    private Contact _contact; // Pole pro uchování načteného kontaktu

    // Veřejná vlastnost, do které MAUI zapíše ID z navigační adresy
    public int ContactId
    {
        get => _contactId;
        set
        {
            _contactId = value;
            // Jakmile dostaneme ID, načteme data kontaktu
            LoadContactAsync(value);
        }
    }

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

    public ICommand SaveCommand { get; }

    // Konstruktor si vyžádá DatabaseContext
    public ContactDetailViewModel(DatabaseContext context)
    {
        _context = context;

        SaveCommand = new Command(async (param) => await SaveContactAsync());
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
        }
    }

    private async Task SaveContactAsync()
    {
        // Převezmeme hodnoty z UI (které jsou nabindované na vlastnosti FirstName, LastName)
        // a aktualizujeme jimi náš načtený datový model.
        _contact.FirstName = FirstName;
        _contact.LastName = LastName;
        // Zde by se doplňovaly další vlastnosti...

        // Řekneme Entity Frameworku, aby aktualizoval záznam v databázi.
        _context.Contacts.Update(_contact);
        await _context.SaveChangesAsync();

        // Po uložení se vrátíme zpět na předchozí stránku (seznam kontaktů).
        // ".." je v MAUI Shellu speciální syntaxe pro "jít o úroveň výš" neboli zpět.
        await Shell.Current.GoToAsync("..");
    }
}