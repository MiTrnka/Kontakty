namespace Kontakty;

public partial class ContactDetailPage : ContentPage
{
    // Konstruktor si nyn� vy��d� ContactDetailViewModel
    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();

        // Nastav�me ViewModel jako BindingContext str�nky
        BindingContext = viewModel;
    }
}