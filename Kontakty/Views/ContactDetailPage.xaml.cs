namespace Kontakty;

// P�id�me atribut QueryProperty. ��k� MAUI, aby hodnotu z query parametru "id"
// zapsal do na�� ve�ejn� vlastnosti "ContactId".
[QueryProperty(nameof(ContactId), "id")]
public partial class ContactDetailPage : ContentPage
{
    // Konstruktor si nyn� vy��d� ContactDetailViewModel
    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();

        // Nastav�me ViewModel jako BindingContext str�nky
        BindingContext = viewModel;
    }

    // Tato ve�ejn� vlastnost s n�zvem "ContactId" (stejn� jako v atributu)
    // bude automaticky napln�na hodnotou z naviga�n� adresy (?id=...).
    // Hodnotu dostaneme jako string.
    public string ContactId
    {
        set
        {
            // P�ed�me p�ijatou hodnotu (ID jako text) na�emu ViewModelu,
            // kter� se postar� o jej� zpracov�n� (p�eveden� na ��slo a na�ten� dat).
            // Pou��v�me bezpe�n� p�etypov�n� pro p��pad, �e by BindingContext nebyl spr�vn�ho typu.
            (BindingContext as ContactDetailViewModel)?.ApplyQueryAttributes(value);
        }
    }
}