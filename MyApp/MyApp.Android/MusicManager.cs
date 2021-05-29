using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Media;
using Android.Database;
using Android.Provider;
using Xamarin.Essentials;
using MyApp.Views;
namespace MyApp.Droid
{
    static class MusicManager
    {
        static MediaPlayer mp;
        static bool isPrepared = false;
        static List<Song> songList;
        static SongListPage s;
        static string imageUrl = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
        static public void  Init()
        {
            if (mp != null)
                return;
            mp = new MediaPlayer(); 
        }
        static public void Play(string path)
        {
            Init();
            mp.Reset();
            mp.SetDataSource(path);
            mp.Prepare();
            mp.Start();

            isPrepared = true;
        }
        static public void PauseOrContinue()
        {
            Init();
            if (!isPrepared)
                return;
            if (mp.IsPlaying)
                mp.Pause();
            else
                mp.Start();
        }

        static public List<Song> GetLocalMusicList(Context context)
        {
            if(songList == null)
                songList = new List<Song>();
            songList.Clear();
            ContentResolver cr = context.ContentResolver;
            Android.Net.Uri MusicUri =MediaStore.Audio.Media.GetContentUri(Android.OS.Environment.DirectoryMusic);
            //string MusicFilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyMusic);
            ICursor cursor = cr.Query(MusicUri, null, null,null, MediaStore.Audio.Media.DefaultSortOrder);
            for(int i = 0;i<cursor.Count;i++)
            {
                cursor.MoveToNext();
                
                string songName = cursor.GetString(cursor.GetColumnIndex(MediaStore.ExtraMediaTitle));
                string singer = cursor.GetString(cursor.GetColumnIndex(MediaStore.ExtraMediaArtist));
                string length = cursor.GetString(cursor.GetColumnIndex(MediaStore.ExtraSizeLimit));

                songList.Add(new Song
                {
                    name = songName,
                    singer = singer,
                    length = length,
                    image = imageUrl,
                    path = MusicUri.Path + songName
                });
            }
            return songList;
        }
    }
}