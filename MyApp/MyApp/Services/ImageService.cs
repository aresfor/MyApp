using MyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.Services
{
    public static class ImageService
    {
        public static ImageSource GetImageSource(string name)
        {
            return DependencyService.Get<IAssets>().GetImageSource(name);
        }
    }
}
