using MyApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SongListPageCV : ContentPage
    {
        public SongListPageCV()
        {
            InitializeComponent();
            BindingContext = new SongListViewModel();
        }
    }
}