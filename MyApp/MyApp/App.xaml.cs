using MonkeyCache.FileStore;
using MyApp.Global;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            Barrel.ApplicationId = AppInfo.PackageName;
            Client.Init();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
