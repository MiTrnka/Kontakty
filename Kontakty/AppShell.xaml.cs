using Microsoft.Maui.ApplicationModel.Communication;

namespace Kontakty
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ContactDetailPage), typeof(ContactDetailPage));
        }
    }
}
