namespace Kontakty;

public partial class ContactsListPage : ContentPage
{
    // P�id�me priv�tn� prom�nnou pro ViewModel
    private readonly ContactsListViewModel _viewModel;

    // Pomoc� Dependency Injection si v konstruktoru vy��d�me n� ViewModel
    public ContactsListPage(ContactsListViewModel viewModel)
    {
        InitializeComponent();

        // Ulo��me si instanci ViewModelu a nastav�me ji jako BindingContext str�nky.
        // T�m se propoj� XAML s daty a p��kazy ve ViewModelu.
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    // Tato metoda se automaticky zavol� v�dy, kdy� se str�nka zobraz� na obrazovce.
    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Zavol�me p��kaz pro na�ten� kontakt� z na�eho ViewModelu.
        _viewModel.LoadContactsCommand?.Execute(null);
    }
}