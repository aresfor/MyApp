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
using Xamarin.Essentials;
using Android.Media;
using Android.Graphics;
using Xamarin.Forms;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[assembly:Xamarin.Forms.Dependency(typeof(Assets))]
namespace MyApp.Droid
{
    public class Assets:IAssets
    {
        public string GetMusicPath()
        {
            var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();
            return path;
            //return Path.Combine(path, name);
        }
        //public IList<Song> GetLocalSongs()
        //{
        //    List<Song> songs = new List<Song>();
        //    var path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();
        //    //string path = Android.OS.Environment.DirectoryMusic;
        //    var files = Directory.GetFiles(path);
        //    foreach(var f in files)
        //    {
        //        MediaItem m = new MediaItem(f);
        //        Song song = new Song
        //        {
        //            Name = m.FileName,
        //            Image = m.ImageUri,
        //            DisplayName = m.DisplayTitle,
        //            Singer = m.Artist,
        //            Length = m.Duration.ToString()
        //        };
        //        songs.Add(song);
        //    }
        //    return songs;
        //}
        public IList<Song> GetLocalSongs()
        {
            List<Song> songs = new List<Song>();
            FileInfo f = null;
            
            string filePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();
            foreach (string musicFilePath in Directory.EnumerateFiles(filePath, "*", SearchOption.AllDirectories))
            {
                //Log.Info("Music File", "Music file path is " + musicFilePath);
                // do whatever you want to with the file here
                MediaMetadataRetriever mmr = new MediaMetadataRetriever();
                mmr.SetDataSource(musicFilePath);
                f = new FileInfo(musicFilePath);
                //string imagePath = System.IO.Path.Combine(FileSystem.CacheDirectory, f.Name + ".png");
                songs.Add(new Song
                {
                    Name = f.Name,
                    DisplayName = mmr.ExtractMetadata(MetadataKey.Title),
                    Singer = mmr.ExtractMetadata(MetadataKey.Artist),
                    Length = mmr.ExtractMetadata(MetadataKey.Duration),
                    Image = GetImageSource(f.Name)
                }); 
                //Image = mmr.ExtractMetadata(MetadataKey.ImagePrimary) == null ? "IA.jpg" : mmr.ExtractMetadata(MetadataKey.ImagePrimary)

            }
            #region ICursor方法
            //ContentResolver cr = context.ContentResolver;
            //ICursor cursor = cr.Query(MediaStore.Audio.Media.GetContentUriForPath(filePath), null, null, null, MediaStore.Audio.Media.DefaultSortOrder);
            //for (int i = 0; i < cursor.Count; i++)
            //{
            //    cursor.MoveToNext();

            //    string singerName = cursor.GetString(cursor
            //        .GetColumnIndex(MediaStore.Audio.ArtistColumns.Artist));
            //    string songName = cursor.GetString(cursor
            //        .GetColumnIndex(MediaStore.Audio.AudioColumns.DisplayName));
            //    string songLength = cursor.GetString(cursor
            //        .GetColumnIndex(MediaStore.Audio.AudioColumns.Duration));
            //    string displayName = cursor.GetString(cursor
            //        .GetColumnIndex(MediaStore.Audio.AudioColumns.Title));
            //    string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";
            //    songs.Add(new Song { Name = songName,DisplayName = displayName,
            //        Singer = singerName, Length = songLength });
            //}
            //Console.WriteLine("目录在:" + filePath);
            //Console.WriteLine("歌曲数目:" + songs.Count);
            #endregion
            return songs;
        }

        public string GetImageSource(string name)
        {
            string imagePath = System.IO.Path.Combine(FileSystem.CacheDirectory, name + ".png");
            if (Directory.GetFiles(FileSystem.CacheDirectory, name + ".png").Length != 0)
            {
                return imagePath;
            }

            //var bitmap = CrossMediaManager.Current.Queue.Current.Image as Android.Graphics.Bitmap;
            //MediaMetadataRetriever mmr = new MediaMetadataRetriever();
            //mmr.SetDataSource();
            string filePath = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic).ToString();
            string musicPath = System.IO.Path.Combine(filePath, name);

            TagLib.File file = TagLib.File.Create(musicPath);
            var bytes = file.Tag.Pictures[0].Data.Data;
            File.WriteAllBytes(imagePath, bytes);
            //bitmap直接压缩成图片保存,文件流
            //var stream = new FileStream(imagePath, FileMode.Create);
            //bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);
            //stream.Close();



            //字节流方式写入,未检验
            //var pic = CrossMediaManager.Current.Queue.Current.Image;
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    IFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(ms, pic);
            //    File.WriteAllBytes(FileSystem.CacheDirectory + name + ".png", ms.GetBuffer());
            //}

            //return path + name + ".png";


            //转换为SKBitmap再用文件流存储SkData，其实也是用流
            //SKBitmap skBitmap = bitmap.ToSKBitmap();
            //SKImage image = SKImage.FromBitmap(skBitmap);
            //SKData encodedData = image.Encode(SKEncodedImageFormat.Png, 100);
            //var bitmapImageStream = File.Open(imagePath,
            //                              FileMode.Create,
            //                              FileAccess.Write,
            //                              FileShare.None);
            //encodedData.SaveTo(bitmapImageStream);
            //bitmapImageStream.Flush(true);
            //bitmapImageStream.Dispose();

            //ImageSource imgSrc;
            // or imgSrc = imagePath;
            return imagePath;
        }
    }
}