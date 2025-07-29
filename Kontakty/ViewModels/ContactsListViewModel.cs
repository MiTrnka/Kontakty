using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kontakty;

public class ContactsListViewModel : ViewModelBase
{
    /*
            // Navigace na existující kontakt
            await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}?id={contact.Id}");

            // Navigace pro vytvoření nového
            await Shell.Current.GoToAsync(nameof(ContactDetailPage));

    */
    private readonly DatabaseContext _context;

    // ObservableCollection je speciální typ kolekce, který automaticky
    // informuje UI o přidání nebo odebrání položky.
    public ObservableCollection<ContactViewModel> Contacts { get; } = new();

    // Příkaz pro načtení kontaktů. Bude se volat, když se stránka zobrazí.
    public ICommand LoadContactsCommand { get; }

    // Konstruktor, kde si přes Dependency Injection vyžádáme DatabaseContext.
    public ContactsListViewModel(DatabaseContext context)
    {
        _context = context;
        // Inicializace příkazu a přiřazení metody, která se má vykonat.
        LoadContactsCommand = new Command(async (param) => await LoadContactsAsync());
    }

    private async Task LoadContactsAsync()
    {
        Contacts.Clear();

        // Načteme všechny kontakty z databáze pomocí EF Core.
        // Seřadíme je podle příjmení a jména.
        var contactsFromDb = await _context.Contacts
                                           .OrderBy(c => c.LastName)
                                           .ThenBy(c => c.FirstName)
                                           .ToListAsync();

        // Každý datový model "zabalíme" do jeho ViewModelu a přidáme do kolekce.
        foreach (var contact in contactsFromDb)
        {
            Contacts.Add(new ContactViewModel(contact));
        }
    }
}
