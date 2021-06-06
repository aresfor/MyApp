using MyApp.Shared.Models;
using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FontAwesome;
using MediaManager;

namespace MyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongListPageCL : ContentPage
    {
        public SongListPageCL()
        {
            InitializeComponent();
            //BindingContext = new SongListViewModel();
            
        }

        private void PlayOrPauseButton_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null)
                button  = FindByName("PlayOrPauseButton") as Button;
            if(!CrossMediaManager.Current.IsPlaying())
                button.Text = FontAwesomeIcons.PlayCircle;
            else
                button.Text = FontAwesomeIcons.PauseCircle;
        }
    }
    public class SongDataTemplateSelector:DataTemplateSelector
    {
        public DataTemplate FavouriteSong { get; set; }
        public DataTemplate NormalSong { get; set; }
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return ((Song)item).Singer == "EGOIST" ? FavouriteSong : NormalSong;
        }
    }
}