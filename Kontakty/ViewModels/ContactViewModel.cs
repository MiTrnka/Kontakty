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
    private DateTime _dateOfBirth;

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set => SetProperty(ref _dateOfBirth, value);
    }

    // Veřejné vlastnosti, na které se bude bindovat UI.
    // Nyní pracují s privátními poli této třídy.
    public int Id => _contactModel.Id;

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string LastName
    {
        get => _lastName;
        set =>  SetProperty(ref _lastName, value);
    }

    // Tato vlastnost je v pořádku, protože jen čte z ostatních vlastností.
    public string FullName => $"{FirstName} {LastName} {DateOfBirth}";


    public ContactViewModel(Contact contact)
    {
        _contactModel = contact;

        // --- ZMĚNA ZDE ---
        // Při vytvoření ViewModelu zkopírujeme hodnoty
        // z datového modelu do našich nových privátních polí.
        _firstName = contact.FirstName;
        _lastName = contact.LastName;
        _dateOfBirth = contact.DateOfBirth;
    }

    // --- PŘIDANÁ METODA ---
    // Tuto metodu využijeme později při ukládání.
    // Zkopíruje upravené hodnoty z ViewModelu zpět do datového modelu.
    public void UpdateModel()
    {
        _contactModel.FirstName = this.FirstName;
        _contactModel.LastName = this.LastName;
        _contactModel.DateOfBirth = this.DateOfBirth;
        // ... zde by se doplňovaly další vlastnosti (Email, Telefon atd.)
    }
}