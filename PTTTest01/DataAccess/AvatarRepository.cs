using Microsoft.Data.Sqlite;

namespace PTTTest01.DataAccess
{
    public class AvatarRepository(string sqliteConnectionString, ILogger<AvatarRepository> logger) : IAvatarRepository
    {
        private readonly string _sqliteConnectionString = sqliteConnectionString;
        private readonly ILogger<AvatarRepository> _logger = logger;

        public async Task<string?> GetImageUrlById(char lastDigit)
        {
            try
            {
                using var connection = new SqliteConnection(_sqliteConnectionString);
                    connection.Open();
                    string query = "SELECT url FROM images WHERE id = @id";
                    using var command = new SqliteCommand(query, connection);
                        command.Parameters.AddWithValue("@id", lastDigit.ToString());
                        var result = await command.ExecuteScalarAsync();
                        var imageURL = result?.ToString();
                        if (string.IsNullOrEmpty(imageURL))
                        {
                            _logger.LogError("Image URL is empty after retrieving image URL from database for user identifier: {lastDigit}.", lastDigit);
                        }
                        return imageURL;
            }
            catch (SqliteException ex)
            {
                _logger.LogError(ex, message: "SQLite error occurred while retrieving image for id: {lastDigit}", lastDigit);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: "Unexpected error occurred while retrieving image URL from DB for id: {lastDigit}", lastDigit);
                throw;
            }
        }
    }
}
