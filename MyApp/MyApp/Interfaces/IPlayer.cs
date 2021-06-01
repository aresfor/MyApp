using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Interfaces
{
    public interface IPlayer
    {
        void PlayOrPause();
        void Init(string source);
    }
}
