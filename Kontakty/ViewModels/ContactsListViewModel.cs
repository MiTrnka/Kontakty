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
    private readonly DatabaseContext _context;

    // ObservableCollection je speciální typ kolekce, který automaticky
    // informuje UI o přidání nebo odebrání položky.
    public ObservableCollection<ContactViewModel> Contacts { get; } = new();

    // Příkaz pro načtení kontaktů. Bude se volat, když se stránka zobrazí.
    public ICommand LoadContactsCommand { get; }

    public ICommand GoToDetailsCommand { get; }

    public ICommand CreateNewContactCommand { get; }

    // Konstruktor, kde si přes Dependency Injection vyžádáme DatabaseContext.
    public ContactsListViewModel(DatabaseContext context)
    {
        _context = context;
        // Inicializace příkazu a přiřazení metody, která se má vykonat.
        LoadContactsCommand = new Command(async (param) => await LoadContactsAsync());

        // Inicializace nového příkazu.
        // Očekává jako parametr objekt, na který se kliklo.
        GoToDetailsCommand = new Command(async (contact) => await GoToDetailsAsync(contact));

        // Inicializace příkazu pro vytvoření nového kontaktu
        CreateNewContactCommand = new Command(async (param) => await CreateNewContactAsync());

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

    // Metoda pro provedení navigace.
    private async Task GoToDetailsAsync(object contact)
    {
        // Zkontrolujeme, zda je předaný parametr správného typu.
        if (contact is ContactViewModel contactViewModel)
        {
            // Provedeme navigaci na stránku "ContactDetailPage" a v adrese
            // předáme ID vybraného kontaktu jako tzv. "query parameter".
            // Např. "ContactDetailPage?id=1"
            await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}?id={contactViewModel.Id}");
        }
    }

    private async Task CreateNewContactAsync()
    {
        // Provedeme navigaci na stránku "ContactDetailPage", ale tentokrát
        // bez jakéhokoliv ID. To bude signál pro detail, že vytváříme nový záznam.
        await Shell.Current.GoToAsync(nameof(ContactDetailPage));
    }
}
