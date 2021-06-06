using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyApp.WebAPI.Global
{
    public static class Path
    {
        public static string MusicPath = "/home/ubuntu/Resources/Music/";
        public static string ResourcePath = "/home/ubuntu/Resources/";
        public static string ImagePath = "/home/ubuntu/Resources/Image";
    }
}