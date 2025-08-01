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

    private void RefreshView_Refreshing(object sender, EventArgs e)
    {
        // Kdy� u�ivatel st�hne str�nku dol� pro obnoven�, spust�me p��kaz pro na�ten� kontakt�.
        _viewModel.LoadContactsCommand?.Execute(null);

        // Po dokon�en� obnoven� zastav�me animaci na��t�n�.
        if (sender is RefreshView refreshView)  //z�sk�me RefreshView z odes�latele ud�losti, nebo bychom si to RefreshView mohli pojmenovat v XAML a pou��t p��mo jeho n�zev
        {
            refreshView.IsRefreshing = false;
        }
    }
}