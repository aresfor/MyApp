using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Global;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
    [QueryProperty(nameof(AccountAvatar), nameof(AccountAvatar))]
    [QueryProperty(nameof(AccountName), nameof(AccountName))]
    class AccountViewModel:BaseViewModel
    {
        public AsyncCommand LogOutCommand { get; }
        public AsyncCommand AdvancedCommand { get; }

        public AccountViewModel()
        {
            LogOutCommand = new AsyncCommand(Logout);
            AdvancedCommand = new AsyncCommand(Advanced);
            //AccountName = LoginStates.LoginAccountName;
            //AccountAvatar = LoginStates.LoginAvatar;
        }
        public async Task Logout()
        {
            LoginStates.isLogged = false;
            LoginStates.LoginAccountName = "请登录";

            await Route.GoToPage($"//{nameof(LoginPage)}");
        }
        public async Task Advanced()
            => await Route.GoToPage($"/{nameof(AdvancedPage)}");


        //-----------选择用的样式中的动态资源去设定AccountName，暂时不用这些----------
        ImageSource accountAvatar;
        public ImageSource AccountAvatar
        {
            get => accountAvatar;
            set => SetProperty(ref accountAvatar, value);
        }
        string accountName;
        public string AccountName
        {
            get => accountName;
            set => SetProperty(ref accountName, value);
        }
        //--------------------------------------------------------------------------------
    }
}
