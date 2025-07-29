namespace Kontakty;

// Pøidáme atribut QueryProperty. Øíká MAUI, aby hodnotu z query parametru "id"
// zapsal do naší veøejné vlastnosti "ContactId".
[QueryProperty(nameof(ContactId), "id")]
public partial class ContactDetailPage : ContentPage
{
    // Konstruktor si nyní vyžádá ContactDetailViewModel
    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();

        // Nastavíme ViewModel jako BindingContext stránky
        BindingContext = viewModel;
    }

    // Tato veøejná vlastnost s názvem "ContactId" (stejnì jako v atributu)
    // bude automaticky naplnìna hodnotou z navigaèní adresy (?id=...).
    // Hodnotu dostaneme jako string.
    public string ContactId
    {
        set
        {
            // Pøedáme pøijatou hodnotu (ID jako text) našemu ViewModelu,
            // který se postará o její zpracování (pøevedení na èíslo a naètení dat).
            // Používáme bezpeèné pøetypování pro pøípad, že by BindingContext nebyl správného typu.
            (BindingContext as ContactDetailViewModel)?.ApplyQueryAttributes(value);
        }
    }
}