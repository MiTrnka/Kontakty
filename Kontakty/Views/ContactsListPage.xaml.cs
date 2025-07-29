namespace Kontakty;

public partial class ContactsListPage : ContentPage
{
    // Pøidáme privátní promìnnou pro ViewModel
    private readonly ContactsListViewModel _viewModel;

    // Pomocí Dependency Injection si v konstruktoru vyžádáme náš ViewModel
    public ContactsListPage(ContactsListViewModel viewModel)
    {
        InitializeComponent();

        // Uložíme si instanci ViewModelu a nastavíme ji jako BindingContext stránky.
        // Tím se propojí XAML s daty a pøíkazy ve ViewModelu.
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // Tato metoda se automaticky zavolá vždy, když se stránka zobrazí na obrazovce.
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Zavoláme pøíkaz pro naètení kontaktù z našeho ViewModelu.
        _viewModel.LoadContactsCommand?.Execute(null);
    }
}