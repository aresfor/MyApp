using MvvmHelpers;
using MvvmHelpers.Commands;
using MyApp.Global;
using MyApp.Services;
using MyApp.Shared.Models;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;

namespace MyApp.ViewModels
{
    class SelectCollectionViewModel : BaseViewModel, IQueryAttributable
    {
        public AsyncCommand<object> SelectedCommand { get; }
        public ObservableRangeCollection<Collection> collections { get; }
        public AsyncCommand RefreshCommand { get; }
        public AsyncCommand AddSongToCollectionCommand{get;}
        public int SongId;
        public SelectCollectionViewModel()
        {
            collections = new ObservableRangeCollection<Collection>();
            RefreshCommand = new AsyncCommand(Refresh);
            SelectedCommand = new AsyncCommand<object>(Selected);
        }
        async Task Refresh()
        {
            IsBusy = true;
            var c =  await CollectionService.GetCollectionByAccountId(LoginStates.account.AccountId);
            collections.AddRange(c);
            IsBusy = false;
        }

        void AddSongToCollection()
        {
            CollectionService.UpdateSongToCollection(SelectedItem.CollectionId, SongId,1);
            Application.Current.MainPage.DisplayAlert("成功", "添加歌曲到歌单","OK");
        }

        async Task Selected(object obj)
        {
            var collection = obj as Collection;
            if (collection == null)
                return;
            AddSongToCollection();
            SelectedItem = null;
            await Route.GoToPage($"///{nameof(SongListPage)}");
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            // The query parameter requires URL decoding.
            string name = HttpUtility.UrlDecode(query["SongId"]);
            if (name != null || name != string.Empty)
            {
                int.TryParse(name, out SongId);
            }
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
