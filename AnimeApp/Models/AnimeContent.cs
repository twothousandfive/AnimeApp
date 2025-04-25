using SQLite;

namespace AnimeApp.Models
{
    public class AnimeContent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string Genre { get; set; }
        public string CoverImagePath { get; set; }
        public string VideoPath { get; set; }
        public int UploadedByUserId { get; set; }
    }
}
