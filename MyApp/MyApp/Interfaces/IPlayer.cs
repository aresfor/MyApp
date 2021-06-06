using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Interfaces
{
    public interface IPlayer
    { 
        void Play();
        void Pause();
        void Init(string source);
        bool isPlaying();
        void SeekTo(int msec);
    }
}
