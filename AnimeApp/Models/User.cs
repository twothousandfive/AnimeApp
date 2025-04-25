using SQLite;

namespace AnimeApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string ProfilePhotoPath { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
