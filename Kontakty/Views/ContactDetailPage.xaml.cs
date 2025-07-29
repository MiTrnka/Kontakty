namespace Kontakty;

public partial class ContactDetailPage : ContentPage
{
    // Konstruktor si nyní vyžádá ContactDetailViewModel
    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();

        // Nastavíme ViewModel jako BindingContext stránky
        BindingContext = viewModel;
    }
}