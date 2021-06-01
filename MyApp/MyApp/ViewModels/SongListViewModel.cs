using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Input;
using Xamarin.Forms;
using MyApp.Services;
using System.Threading.Tasks;

namespace MyApp.ViewModels
{
    class SongListViewModel:BaseViewModel
    {
        public ObservableRangeCollection<Song> song { get; set; }
        public ObservableRangeCollection<Grouping<string, Song>> songGroups { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Song> FavouriteCommand { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand<Song> AddCommand { get; }
        public AsyncCommand<Song> DeleteCommand { get; }
        public ICommand PlayOrPauseCommand { get; }

        public MvvmHelpers.Commands.Command InitFreshCommand { get; }
        public SongListViewModel()
        {
            Title = "Song List";
            song = new ObservableRangeCollection<Song>();
            songGroups = new ObservableRangeCollection<Grouping<string, Song>>();
            //IncreaseCount = new Command(OnIncrease);
            string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";

            //List<Song> list = new List<Song> { new Song("All Along with you", "EGOIST", "3:44",imageURL),
            //new Song("My Drearest","SuperCell","4:22",imageURL)};
            //song.AddRange(list);

            //song.Add(new Song("All Along with you", "EGOIST", "3:44", imageURL));
            //song.Add(new Song("My Drearest", "SuperCell", "4:22", imageURL));
            //songGroups.Add(new Grouping<string, Song>("Favorate 1", new[] { song[1] }));
            //songGroups.Add(new Grouping<string, Song>("Favorate 2", new[] { song[0] }));

            //----AsyncCommand Init-----------
            RefreshCommand = new AsyncCommand(Refresh);
            FavouriteCommand = new AsyncCommand<Song>(Favourite);
            SelectedCommand = new AsyncCommand<object>(Selected);
            AddCommand = new AsyncCommand<Song>(AddSong);
            DeleteCommand = new AsyncCommand<Song>(DeleteSong);

            //-------Command Init---------------
            PlayOrPauseCommand = new MvvmHelpers.Commands.Command<string>(PlayOrPause);
            
            //加载页面的时候就刷新一次列表
            RefreshCommand.ExecuteAsync();
        }
        void  PlayOrPause(string name)
        {
            PlayerService.InitPlayer(name);
            PlayerService.PlayOrPause();
        }
        async Task DeleteSong(Song song)
        {
            await SongService.DeleteSong(song.Id);
            await Refresh();
        }
        async Task AddSong(Song song)
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("Name", "NameMessage");
            var singer = await Application.Current.MainPage.DisplayPromptAsync("Singer", "SingerMessage");
            var length = await Application.Current.MainPage.DisplayPromptAsync("Length", "length");

            await SongService.AddSong(name, singer, length);
            await Refresh();
        }
        async Task Favourite(Song song)
        {
            await Application.Current.MainPage.DisplayAlert("Favourate", song.Name, "OK");
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
            await Task.Delay(500);
            song.Clear();
            var songs = await SongService.GetSong();
            song.AddRange(songs);
            IsBusy = false;
        }
        Song selectedItem;
        Song previousSelectedItem;
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
        public ICommand IncreaseCount { get; }
        int count = 0;
        string countDisplay = "click me";
        public string CountDisplay
        {
            get => countDisplay;
            set => SetProperty(ref countDisplay, value);
        }
        void OnIncrease()
        {
            count++;
            CountDisplay = $"you click {count} time(s)";
        }
    }
}
