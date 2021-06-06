using MyApp.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
		
		public LoginPage ()
		{
			InitializeComponent();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();
			if (LoginStates.isLogged)
				 await Route.GoToPage($"//{nameof(SongListPageCL)}");
        }
        public  void PlayAnim()
        {
			Random random = new Random();
			var start = random.NextDouble();
			var end = random.NextDouble();
			if(start > end)
            {
				var temp = start;
				start = end;
				end = temp;
            }
			 new Animation(callback: v => BackgroundColor = Color.FromHsla(v, 1, 0.5,0.5),
  start: 0,
  end: 1).Commit(this, "Animation", 16, 4000, Easing.Linear, (v, c) => BackgroundColor = Color.LightSeaGreen);

			 
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
			PlayAnim();
        }
    }
}