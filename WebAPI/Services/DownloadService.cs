using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WebAPI.Services
{
    public static class DownloadService
    {
        public  static FileStream GetReadFileStream(string Path)
        {
            var fileinfo = new FileInfo(Path);
            if(!fileinfo.Exists)
            {
                throw new FileNotFoundException();
            }
            return fileinfo.OpenRead();
        }
    }
}