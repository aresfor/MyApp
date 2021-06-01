using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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
            if (mediaPlayer == null)
                mediaPlayer = new MediaPlayer();
            mediaPlayer.Reset();
            mediaPlayer.SetDataSource(source);
            mediaPlayer.Prepare();
        }
        public void PlayOrPause()
        {
            if (!mediaPlayer.IsPlaying)
                mediaPlayer.Start();
            else
                mediaPlayer.Pause();
        }
    }
}