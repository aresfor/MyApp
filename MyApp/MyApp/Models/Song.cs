using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Models
{
    class Song
    {
        public string Name { get; set; }
        public string Singer { get; set; }
        public string Length { get; set; }
        public string Image { get; set; }
        public Song(string name,string singer,string length,string image)
        {
            Name = name;
            Singer = singer;
            Length = length;
            Image = image;
        }
    }
}
