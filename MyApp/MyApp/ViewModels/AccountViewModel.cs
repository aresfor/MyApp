using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
    [QueryProperty(nameof(AccountIcon),nameof(AccountIcon))]
    [QueryProperty(nameof(AccountName),nameof(AccountName))]
    public class AccountViewModel:BaseViewModel
    {

        public AsyncCommand LogOutCommand { get; }
        public AsyncCommand AdvancedCommand { get; }
        public bool logged { get; set; }

        
        public AccountViewModel()
        {
            Icon = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";
            LogOutCommand = new AsyncCommand(LogOut);
            AdvancedCommand = new AsyncCommand(Advanced);
            
        }
        public async Task LogOut()
            => await GoToPage($"//{nameof(LoginPage)}");
        public async Task Advanced()
            => await GoToPage($"{nameof(AdvancedPage)}");
        
        public async Task GoToPage(string route)
        {
            await Shell.Current.GoToAsync(route);
        }
        ImageSource accountIcon;
        public ImageSource AccountIcon
        {
            get => accountIcon;
            set => SetProperty(ref accountIcon, value);
        }
        string accountName;
        public string AccountName
        {
            get => accountName;
            set => SetProperty(ref accountName, value);
        }
        
    }
}
