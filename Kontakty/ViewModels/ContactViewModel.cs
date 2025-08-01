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

    // Veřejné vlastnosti, na které se bude bindovat UI.
    // Nyní pracují s privátními poli této třídy.
    public int Id => _contactModel.Id;

    private string _firstName;
    private string _lastName;
    private DateTime _dateOfBirth;
    private Gender _gender;

    public Gender Gender
    {
        get => _gender;
        set
        {
            _gender = value;
            OnPropertyChanged();
        }

    }

    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            OnPropertyChanged();
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
        }
    }

    // Tato vlastnost je v pořádku, protože jen čte z ostatních vlastností.
    public string FullContactInformation => $"{FirstName} {LastName} {DateOfBirth:d}";


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