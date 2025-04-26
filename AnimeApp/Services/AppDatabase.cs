using SQLite;
using AnimeApp.Models;

namespace AnimeApp.Services
{
    public class AppDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public AppDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<AnimeContent>().Wait();
            _database.CreateTableAsync<Favorite>().Wait();
        }

        // Примеры методов
        public Task<int> RegisterUserAsync(User user) =>
            _database.InsertAsync(user);

        public Task<User?> GetUserByEmailAsync(string email) =>
            _database.Table<User>().Where(u => u.Email == email).FirstOrDefaultAsync();

        public Task<List<AnimeContent>> GetAllAnimeAsync() =>
            _database.Table<AnimeContent>().ToListAsync();

        public Task<List<AnimeContent>> GetAnimeByUserIdAsync(int userId) => 
            _database.Table<AnimeContent>().Where(a => a.UploadedByUserId == userId).ToListAsync();

        public Task<int> InsertAnimeAsync(AnimeContent anime) =>
            _database.InsertAsync(anime);

        public async Task<int> SaveAnimeAsync(AnimeContent anime)
        {
            if (anime.Id != 0)
                return await _database.UpdateAsync(anime);
            else
                return await _database.InsertAsync(anime);
        }
    }
}
