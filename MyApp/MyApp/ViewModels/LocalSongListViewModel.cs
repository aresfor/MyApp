﻿using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;
using Xamarin.Forms;
using MyApp.Services;
using System.Threading.Tasks;
using MediaManager;
using MyApp.Global;
using MyApp.Views;

namespace MyApp.ViewModels
{
    class LocalSongListViewModel:BaseViewModel
    {
        public ObservableRangeCollection<Song> song { get; set; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Song> DetailCommand { get; }

        string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        //构造方法会调用两次，为什么,因为xaml绑定了context就不用再具体代码中再调用一次
        public LocalSongListViewModel()
        {
            Title = "Song List";
            song = new ObservableRangeCollection<Song>();
            //IncreaseCount = new Command(OnIncrease);

            //List<Song> list = new List<Song> { new Song("All Along with you", "EGOIST", "3:44",imageURL),
            //new Song("My Drearest","SuperCell","4:22",imageURL)};
            //song.AddRange(list);

            //song.Add(new Song("All Along with you", "EGOIST", "3:44", imageURL));
            //song.Add(new Song("My Drearest", "SuperCell", "4:22", imageURL));
            //songGroups.Add(new Grouping<string, Song>("Favorate 1", new[] { song[1] }));
            //songGroups.Add(new Grouping<string, Song>("Favorate 2", new[] { song[0] }));

            //----AsyncCommand Init-----------
            SelectedCommand = new AsyncCommand<object>(Selected);
            RefreshCommand = new AsyncCommand(Refresh);
            DetailCommand = new AsyncCommand<Song>(Detail);
            //-------Command Init---------------
            //加载页面的时候就刷新一次列表
            //RefreshCommand.ExecuteAsync();
        }
       
        async Task Detail(Song song)
        {
            await Route.GoToSongDetailPage(song);
        }
      
        async Task Selected(object obj)
        {
            var song = obj as Song;
            if (song == null)
                return;
            SelectedItem = null;
            await Application.Current.MainPage.DisplayAlert("SelectedItem", song.Name, "OK");
        }
        async Task Refresh()
        {
            //Mode设置为twoWay会自动设置IsBusy=True
            IsBusy = true;
            //await Task.Delay(500);
            song.Clear();
            //var songs = await SongService.GetSong();
            var songs = await InternetSongService.GetSong();
            song.AddRange(songs);
            IsBusy = false;
        }

        Song selectedItem;
        //Song previousSelectedItem;
        public Song SelectedItem
        {
            get => selectedItem;
            set => SetProperty(ref selectedItem, value);
            //set
            //{
            //    if(value!=null)
            //    {
            //        Application.Current.MainPage.DisplayAlert("SelectedItem", value.Name, "OK");
            //        previousSelectedItem = value;
            //        value = null;
            //    }
            //    selectedItem = value;
            //    OnPropertyChanged();
            //}
        }
    }
}
