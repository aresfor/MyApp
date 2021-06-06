using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Services;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyApp.Global;
namespace MyApp.ViewModels
{

    public class LoginViewModel:BaseViewModel
    {

        public AsyncCommand LogOutCommand { get; }
        public AsyncCommand AdvancedCommand { get; }
        public AsyncCommand<string> LoginCommand { get; }
        public AsyncCommand<string> RegisterCommand { get; }
        
        public LoginViewModel()
        {
            Icon = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";
            
            LoginCommand = new AsyncCommand<string>(Login);
            RegisterCommand = new AsyncCommand<string>(Register);
        }
        public async Task Register(string name)
        {
            if(await AccountService.Register(name))
            {
                
                await Application.Current.MainPage.DisplayAlert("成功","注册通过","确认");
            }else
            {
                await Application.Current.MainPage.DisplayAlert("失败", "有重名或其他错误","确认");
            }
        }
        
        
        
        public async Task Login(string name)
        {
            var account = await AccountService.GetAccount(name);
            if ( account != null)
            {
                LoginStates.LoginAccountName = name;
                LoginStates.isLogged = true;
                LoginStates.account = account;
                App.Current.Resources["AccountName"] = name;
                await Route.GoToPage($"//{nameof(SongListPageCL)}");
                    //$"AccountName = {LoginStates.LoginAccountName}" +
                    //$"&AccountAvatar={LoginStates.LoginAvatar}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("账号错误", "请重新输入正确账号","确认");
            }
        }
       
        
    }
}
