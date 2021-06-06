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
    class SongListViewModelCL : BaseViewModel
    {
        public ObservableRangeCollection<Song> song { get; set; }
        public ObservableRangeCollection<Collection> collection { get; set; }
        public ObservableRangeCollection<Grouping<string, Song>> songGroups { get; }
        public AsyncCommand<object> SelectedCommand { get; }
       
        public AsyncCommand DragCompletedCommand { get; }
        public AsyncCommand<string> SetSongCommand { get; }
        public AsyncCommand InitRefreshCommand { get; }
        public AsyncCommand<int> AddSongToCollectionCommand { get; }
        public AsyncCommand AddCollectionToAccountCommand { get; }
        public AsyncCommand RefreshCommand { get; }


        public ICommand PlayOrPauseCommand { get; }
        public ICommand CollectionChangeCommand { get; }

        string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        //构造方法会调用两次，为什么？
        public SongListViewModelCL()
        {
            Title = "Song List";
            song = new ObservableRangeCollection<Song>();
            collection = new ObservableRangeCollection<Collection>();
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
           
            DragCompletedCommand = new AsyncCommand(DragCompleted);
            SetSongCommand = new AsyncCommand<string>(SetSong);
            InitRefreshCommand = new AsyncCommand(InitRefresh);
            AddSongToCollectionCommand = new AsyncCommand<int>(AddSongToCollection);
            AddCollectionToAccountCommand = new AsyncCommand(AddCollectionToAccount);
            RefreshCommand = new AsyncCommand(Refresh);
            //-------Command Init---------------
            PlayOrPauseCommand = new MvvmHelpers.Commands.Command(PlayOrPause);
            CollectionChangeCommand = new MvvmHelpers.Commands.Command(CollectionChange);
            //加载页面的时候就刷新一次列表
            InitRefreshCommand.ExecuteAsync();
        }

        async Task AddCollectionToAccount()
        {
            var res =await AccountService.UpdateAccount(LoginStates.account.AccountId, CollectionSelected.CollectionId, 1);
            if (res)
                await Application.Current.MainPage.DisplayAlert("成功", "添加了" + $"{CollectionSelected.Name}" + "歌单", "OK");
            else
                await Application.Current.MainPage.DisplayAlert("失败", "未添加" + $"{CollectionSelected.Name}" + "歌单", "Cancel");

        }
        async Task AddSongToCollection(int SongId)
        {
            var CollectionName = await Application.Current.MainPage.DisplayPromptAsync("加入歌单", "歌单名字");
            var collection = LoginStates.account.Collecitons.Find(c => c.Name == CollectionName);
            if (collection == null)
            {
                await Application.Current.MainPage.DisplayAlert("失败", "没有这个歌单", "OK");
                return;
            }
            await CollectionService.AddSongToCollection(collection.CollectionId, SongId);

        }
        public void CollectionChange()
        {
            song.Clear();
            song.AddRange(CollectionSelected.songs);
        }

        Collection collectionSelected;
        public Collection CollectionSelected
        {
            get => collectionSelected;
            set => SetProperty(ref collectionSelected, value);
        }

        double position = 0;
        public double Position
        {
            get => position;
            set => SetProperty(ref position, value);

        }
        async Task DragCompleted()
        {
            await PlayerService.SeekTo(Position);
        }
        public double Length
        {
            get => PlayerService.Length == 0 ? 1 : PlayerService.Length;
            set => SetProperty(ref PlayerService.Length, value);
        }
        async void PlayOrPause()
        {
            if (PlayerService.isPlaying())
                PlayerService.Pause();
            else
                await PlayerService.Play();
        }
        async Task SetSong(string name)
        {
            await PlayerService.SetSong(name);
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
            var songs =await InternetSongService.GetSongByCollectionId(CollectionSelected.CollectionId);
            //var songs = await SongService.GetSong();
            //var songs = await InternetSongService.GetSong();
            song.AddRange(songs);
            IsBusy = false;
        }
        async Task InitRefresh()
        {
            //Mode设置为twoWay会自动设置IsBusy=True
            IsBusy = true;
            //await Task.Delay(500);
            song.Clear();
            collection.Clear();
            //var songs = await SongService.GetSong();
            //var songs = await InternetSongService.GetSong();
            //song.AddRange(songs);
            var collections = await CollectionService.GetCollection();
            collection.AddRange(collections);
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
