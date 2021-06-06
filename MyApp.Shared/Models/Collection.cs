using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Shared.Models
{
    public class Collection
    {
        public List<Account> accounts { get; set; }
        public int CollectionId { get; set; }
        public List<Song> songs { get; set; }
        public string Name { get; set; }
    }
}
