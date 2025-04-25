using SQLite;

namespace AnimeApp.Models
{
    public class Favorite
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int AnimeContentId { get; set; }
    }
}
