using MyApp.Shared.Models;
using MyApp.ViewModels;
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
    public partial class SongListPage : ContentPage
    {
        public SongListPage()
        {
            InitializeComponent();
 
            BindingContext = new SongListViewModel();

        }

        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    var song = ((ListView)sender).SelectedItem as Song;
        //    if (song == null)
        //        return;
        //}

        //private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    ((ListView)sender).SelectedItem = null;
        //}

        //private void MenuItem_Clicked(object sender, EventArgs e)
        //{
        //    var song = ((MenuItem)sender).BindingContext as Song;
        //    if (song == null)
        //        return;
        //}
    }
}