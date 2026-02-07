using System;
using System.Threading.Tasks;
using MySqlConnector;

namespace SafeVault.Data
{
    public sealed class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<int> CreateUserAsync(string username, string email)
        {
            const string sql = @"
INSERT INTO Users (Username, Email)
VALUES (@username, @email);
SELECT LAST_INSERT_ID();";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@email", email);

            var result = await cmd.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<(int userId, string username, string email)?> GetUserByUsernameAsync(string username)
        {
            const string sql = @"
SELECT UserID, Username, Email
FROM Users
WHERE Username = @username
LIMIT 1;";

            await using var conn = new MySqlConnection(_connectionString);
            await conn.OpenAsync();

            await using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@username", username);

            await using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            return (
                reader.GetInt32("UserID"),
                reader.GetString("Username"),
                reader.GetString("Email")
            );
        }
    }
}
