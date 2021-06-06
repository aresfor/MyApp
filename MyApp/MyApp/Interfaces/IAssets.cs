using Android.Content;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace MyApp.Interfaces
{
    public interface IAssets
    {
        string GetMusicPath();
        //为了查询本地歌曲的信息的必须用ICursor，而Context属于mono.Android，这里引入了这个DLL，不知道这样对不对
        IList<Song> GetLocalSongs(Context context, string name);

    }
}
