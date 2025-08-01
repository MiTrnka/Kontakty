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

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        // Když uživatel stáhne stránku dolù pro obnovení, spustíme pøíkaz pro naètení kontaktù.
        _viewModel.LoadContactsCommand?.Execute(null);

        // Po dokonèení obnovení zastavíme animaci naèítání.
        if (sender is RefreshView refreshView)  //získáme RefreshView z odesílatele události, nebo bychom si to RefreshView mohli pojmenovat v XAML a použít pøímo jeho název
        {
            refreshView.IsRefreshing = false;
        }
    }
}