using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MediaManager;
using MediaManager.Library;
using MyApp.Droid;
using MyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

[assembly:Xamarin.Forms.Dependency(typeof(Player))]
namespace MyApp.Droid
{
    class Player :  IPlayer
    {
        MediaPlayer mediaPlayer;
        
        public void Init(string source)
        {
            //if (mediaPlayer == null)
            //    mediaPlayer = new MediaPlayer();
            //mediaPlayer.Reset();
            //mediaPlayer.SetDataSource(source);
            //mediaPlayer.PrepareAsync();
            try
            {
                CrossMediaManager.Current.Play(source);
            }catch(Exception ex)
            {
                
            }
        }
        public void Play()
        {
            //mediaPlayer.Start();
            CrossMediaManager.Current.Play();
        }
        public void Pause()
        {
            //mediaPlayer.Pause();
            CrossMediaManager.Current.Pause();
        }
        public bool isPlaying()
        {
            //return mediaPlayer.IsPlaying;
            return CrossMediaManager.Current.IsPlaying();
        }
        
        public void SeekTo(int msec)
        {
            //mediaPlayer.SeekTo(msec);
        }
    }
}