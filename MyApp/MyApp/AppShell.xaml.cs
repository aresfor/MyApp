using MyApp.Views;
using System;
using Xamarin.Forms;

namespace MyApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(AdvancedPage), typeof(AdvancedPage));
            Routing.RegisterRoute(nameof(DetailPage), typeof(DetailPage));
            Routing.RegisterRoute(nameof(SongListPage), typeof(SongListPage));
            Routing.RegisterRoute(nameof(SelectCollectionPage), typeof(SelectCollectionPage));
        }

    }
}
