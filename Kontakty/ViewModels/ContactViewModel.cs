using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontakty;

public class ContactViewModel : ViewModelBase
{
    // Odkaz na původní datový model si stále ponecháme,
    // budeme ho potřebovat později pro ukládání změn.
    private readonly Contact _contactModel;

    // --- ZMĚNA ZDE ---
    // Vytvoříme privátní pole (backing fields) přímo v tomto ViewModelu
    // pro každou vlastnost, kterou chceme v UI editovat.
    private string _firstName;
    private string _lastName;

    // Veřejné vlastnosti, na které se bude bindovat UI.
    // Nyní pracují s privátními poli této třídy.
    public int Id => _contactModel.Id;

    public string FirstName
    {
        get => _firstName;
        set
        {
            // Metoda SetProperty nyní správně pracuje s naším privátním polem _firstName.
            if (SetProperty(ref _firstName, value))
            {
                // Notifikace pro FullName zůstává, to je správně.
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            if (SetProperty(ref _lastName, value))
            {
                OnPropertyChanged(nameof(FullName));
            }
        }
    }

    // Tato vlastnost je v pořádku, protože jen čte z ostatních vlastností.
    public string FullName => $"{FirstName} {LastName}";


    public ContactViewModel(Contact contact)
    {
        _contactModel = contact;

        // --- ZMĚNA ZDE ---
        // Při vytvoření ViewModelu zkopírujeme hodnoty
        // z datového modelu do našich nových privátních polí.
        _firstName = contact.FirstName;
        _lastName = contact.LastName;
    }

    // --- PŘIDANÁ METODA ---
    // Tuto metodu využijeme později při ukládání.
    // Zkopíruje upravené hodnoty z ViewModelu zpět do datového modelu.
    public void UpdateModel()
    {
        _contactModel.FirstName = this.FirstName;
        _contactModel.LastName = this.LastName;
        // ... zde by se doplňovaly další vlastnosti (Email, Telefon atd.)
    }
}