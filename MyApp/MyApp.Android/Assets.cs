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
using MyApp.Interfaces;
using MyApp.Droid;
using Android.Database;
using Android.Provider;
using MyApp.Shared.Models;
using System.IO;
using MediaManager;
using MediaManager.Library;
[assembly:Xamarin.Forms.Dependency(typeof(Assets))]
namespace MyApp.Droid
{
    public class Assets:IAssets
    {
        public string GetMusicPath()
        {
            //var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();
            return Android.OS.Environment.DirectoryMusic;
            //return Path.Combine(path, name);
        }
        public IList<Song> GetLocalSongs()
        {
            List<Song> songs = new List<Song>();
            string path = Android.OS.Environment.DirectoryMusic;
            var files = Directory.GetFiles(path);
            foreach(var f in files)
            {
                MediaItem m = new MediaItem(f);
                Song song = new Song
                {
                    Name = m.FileName,
                    Image = m.ImageUri,
                    DisplayName = m.DisplayTitle,
                    Singer = m.Artist,
                    Length = m.Duration.ToString()
                };
                songs.Add(song);
            }
            return songs;
        }
        public IList<Song> GetLocalSongs(Context context, string name)
        {
            List<Song> songs = new List<Song>();
            #region
            ContentResolver cr = context.ContentResolver;
            string filePath =Android.OS.Environment.DirectoryMusic;
            ICursor cursor = cr.Query(MediaStore.Audio.Media.GetContentUriForPath(filePath), null, null, null, MediaStore.Audio.Media.DefaultSortOrder);
            for (int i = 0; i < cursor.Count; i++)
            {
                cursor.MoveToNext();
                string singerName = cursor.GetString(cursor
                    .GetColumnIndex(MediaStore.Audio.ArtistColumns.Artist));
                string songName = cursor.GetString(cursor
                    .GetColumnIndex(MediaStore.Audio.AudioColumns.DisplayName));
                string songLength = cursor.GetString(cursor
                    .GetColumnIndex(MediaStore.Audio.AudioColumns.Duration));
                string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
                songs.Add(new Song { Name = songName, Singer = singerName, Length = songLength });
            }
            Console.WriteLine("目录在:" + filePath);
            Console.WriteLine("歌曲数目:" + songs.Count);
            #endregion
            return songs;
        }
    }
}