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
using MediaManager;
using MyApp.Global;

namespace MyApp.ViewModels
{
    class CollectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Song> song { get; set; }
        public ObservableRangeCollection<Grouping<string, Song>> songGroups { get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand<int> AddSongToCollectionCommand { get; }
        public AsyncCommand<Song> DeleteCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Song> FavouriteCommand { get; }
        public AsyncCommand AddCommand { get; }
        string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        //构造方法会调用两次，为什么,因为xaml绑定了context就不用再具体代码中再调用一次
        public CollectionViewModel()
        {
            Title = "Song List";
            song = new ObservableRangeCollection<Song>();
            songGroups = new ObservableRangeCollection<Grouping<string, Song>>();
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
            AddSongToCollectionCommand = new AsyncCommand<int>(AddSongToCollection);
            RefreshCommand = new AsyncCommand(Refresh);
            DeleteCommand = new AsyncCommand<Song>(DeleteSong);
            FavouriteCommand = new AsyncCommand<Song>(Favourite);
            AddCommand = new AsyncCommand(AddSong);

            //-------Command Init---------------
            //加载页面的时候就刷新一次列表
            //RefreshCommand.ExecuteAsync();
        }
        async Task AddSong()
        {
            var name = await Application.Current.MainPage.DisplayPromptAsync("Name", "NameMessage");
            var singer = await Application.Current.MainPage.DisplayPromptAsync("Singer", "SingerMessage");
            var length = await Application.Current.MainPage.DisplayPromptAsync("Length", "length");

            await InternetSongService.AddSong(name, singer, length);
            await Refresh();
        }
        async Task Favourite(Song song)
        {
            await Application.Current.MainPage.DisplayAlert("Favourate", song.Name, "OK");
        }
        async Task DeleteSong(Song song)
        {
            await InternetSongService.DeleteSong(song.SongId);
            await Refresh();
        }
        async Task AddSongToCollection(int SongId)
        {
            var CollectionName = await Application.Current.MainPage.DisplayPromptAsync("加入歌单", "歌单名字");
            var collections = await CollectionService.GetCollectionByAccountId(LoginStates.account.AccountId);
            Collection collection = null;
            foreach (var c in collections)
            {
                if (c.Name == CollectionName)
                {
                    collection = c;
                    break;
                }
            }
            if (collection == null)
            {
                await Application.Current.MainPage.DisplayAlert("失败", "没有这个歌单", "OK");
                return;
            }
            else
            {
                await CollectionService.AddSongToCollection(collection.CollectionId, SongId);
                await Application.Current.MainPage.DisplayAlert("成功 ", "已添加到歌单", "OK");

            }

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
