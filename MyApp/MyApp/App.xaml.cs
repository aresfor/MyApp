using Android.Content;
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
            //Sharpnado Init
            Sharpnado.Tabs.Initializer.Initialize(loggerEnable: true,debugLogEnable:false);


            //MonkeyCache Init
            Barrel.ApplicationId = AppInfo.PackageName;


            //Single static client Init
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
