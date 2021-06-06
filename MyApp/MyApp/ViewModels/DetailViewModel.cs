using MvvmHelpers;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
    [QueryProperty(nameof(song),nameof(song))]
    class DetailViewModel: BaseViewModel
    {
        public Song song { get; set; }
        public DetailViewModel()
        {

        }
    }
    public class SongDetailViewModel:BaseViewModel
    {
        public Song song;
    }
}
