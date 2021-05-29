using SQLite;

namespace MyApp.Shared.Models
{
    public class Song
    {
        [PrimaryKey,AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Singer { get; set; }
        
        public string Length { get; set; }
        public string Image { get; set; }
        //public string Path { get; set; }
        //public Song(int id,string name,string singer,string length,string image)
        //{
        //    Id = id;
        //    Name = name;
        //    Singer = singer;
        //    Length = length;
        //    Image = image;
        //    //Path = path;
        //}
    }
}
