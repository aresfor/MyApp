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
using MyApp.Views;

namespace MyApp.ViewModels
{
    class CollectionViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Collection> collection { set; get; }
        public AsyncCommand<object> SelectedCommand { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand<int> AddCollectionToAccountCommand { get; }
        public ICommand ChangeToMyCollectionsCommand { get; }
        public ICommand ChangeToAllCollectionsCommand { get; }

        string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        //构造方法会调用两次，为什么,因为xaml绑定了context就不用再具体代码中再调用一次
        public CollectionViewModel()
        {
            Title = "Collection List";
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
            RefreshCommand = new AsyncCommand(Refresh);
            ChangeToMyCollectionsCommand = new MvvmHelpers.Commands.Command(ChangeToMyCollections);
            ChangeToAllCollectionsCommand = new MvvmHelpers.Commands.Command(ChangeToAllCollections);
            AddCollectionToAccountCommand = new AsyncCommand<int>(CollectionToAccount);
            //-------Command Init---------------
            //加载页面的时候就刷新一次列表
            //RefreshCommand.ExecuteAsync();
        }
        public enum CollectionType
        {
            MyCollections = 0,
            AllCollections = 1
        }
        CollectionType type = CollectionType.AllCollections;
        void  ChangeToAllCollections()
        {
            type = CollectionType.AllCollections;
            

        }
        void ChangeToMyCollections()
        {
            type = CollectionType.MyCollections;

        }
        async Task CollectionToAccount(int CollectionId)
        {
            int add = 1;
            var array = await CollectionService.GetCollectionByAccountId(LoginStates.account.AccountId);
            foreach(var a in array)
            {
                if(a.CollectionId == CollectionId)
                {
                    add = 0;
                    break;
                }    
            }
           var res =  await AccountService.UpdateAccount(LoginStates.account.AccountId, CollectionId,add);
            if(res)
            {
                if(add == 1)
                    await Application.Current.MainPage.DisplayAlert("成功", "添加歌单成功","OK");
                else
                    await Application.Current.MainPage.DisplayAlert("成功", "删除歌单成功", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("失败", "绝对不是服务器君的错!","Cancel");

            }
        }
     
       
       
        async Task Selected(object obj)
        {
            var c = obj as Collection;
            if (c == null)
                return;
            var collectionId = SelectedItem.CollectionId;
            SelectedItem = null;
            await Shell.Current.GoToAsync($"/{nameof(SongListPage)}?collectionId={collectionId}");
            //await Application.Current.MainPage.DisplayAlert("SelectedItem", c.Name, "OK");
        }
        async Task Refresh()
        {
            //Mode设置为twoWay会自动设置IsBusy=True
            IsBusy = true;
            //await Task.Delay(500);
            collection.Clear();
            //var songs = await SongService.GetSong();
            IEnumerable<Collection> collections = null;
            if (type == CollectionType.AllCollections)
                collections = await CollectionService.GetCollection();
            else if(type == CollectionType.MyCollections)
                collections = await CollectionService.GetCollectionByAccountId(LoginStates.account.AccountId);
            collection.AddRange(collections);
            IsBusy = false;
        }

        Collection selectedItem;
        //Song previousSelectedItem;
        public Collection SelectedItem
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
