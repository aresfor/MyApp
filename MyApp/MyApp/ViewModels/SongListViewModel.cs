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
using System.Web;
using MyApp.Views;

namespace MyApp.ViewModels
{
    //[QueryProperty(nameof(EnableRefresh),nameof(EnableRefresh))]
    
    class SongListViewModel:BaseViewModel, IQueryAttributable
    {
        
        public ObservableRangeCollection<Song> song { get; set; }
        public ObservableRangeCollection<Collection> collection { get; set; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand<int> AddSongToCollectionCommand { get; }
        public AsyncCommand<Song> DeleteCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<Song> FavouriteCommand { get; }
        public AsyncCommand AddCommand { get; }
        int collectionId;
        //public bool EnableRefresh { get; set; }
        string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        //构造方法会调用两次，为什么,因为xaml绑定了context就不用再具体代码中再调用一次
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // The query parameter requires URL decoding.
            if (!query.ContainsKey("collectionId"))
                return;
            string name = HttpUtility.UrlDecode(query["collectionId"]);
            if (name != null || name != string.Empty)
            {
                int.TryParse(name, out collectionId);
            }
            if(collectionId!=0)
            {
                ContentItem = ContentType.CollectionSongs;
            }
        }
        public SongListViewModel()
        {
            
            Title = "Song List";
            song = new ObservableRangeCollection<Song>();
            collection = new ObservableRangeCollection<Collection>();
            
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
        ContentType contentItem = ContentType.InternetSongs;
        public ContentType ContentItem
        {
            get => contentItem;
            set => SetProperty(ref contentItem, value);
        }
        public enum ContentType
        {
            //所有歌曲 = 0,
            InternetSongs = 1,
            CollectionSongs = 2,
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
            //var CollectionName = await Application.Current.MainPage.DisplayPromptAsync("加入歌单", "歌单名字");
            //var collections =await CollectionService.GetCollectionByAccountId(LoginStates.account.AccountId);
            //Collection collection = null;
            //foreach(var c in collections)
            //{
            //    if(c.Name == CollectionName)
            //    {
            //        collection = c;
            //        break;
            //    }
            //}
            await Route.GoToPage($"/{nameof(SelectCollectionPage)}?SongId={SongId}");
            

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
            
            switch(ContentItem)
            {
                case ContentType.InternetSongs:
                    {
                        var s = await InternetSongService.GetSong();
                        song.AddRange(s);
                        break;
                    }
                case ContentType.CollectionSongs:
                        {
                        var s = await InternetSongService.GetSongByCollectionId(collectionId);
                        song.AddRange(s);
                        break;
                    }
            }
            //var songs = await SongService.GetSong();
            
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
