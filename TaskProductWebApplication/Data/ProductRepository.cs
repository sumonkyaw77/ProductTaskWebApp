using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using TaskProductWebApplication.Models;

namespace TaskProductWebApplication.Data
{
    public class ProductRepository
    {
        private readonly IConfiguration _configuration;

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using var connection = GetConnection();
            return await connection.QueryAsync<Product>(
                "SELECT * FROM Products ORDER BY Created DESC");
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            using var connection = GetConnection();
            return await connection.QuerySingleOrDefaultAsync<Product>(
                "SELECT * FROM Products WHERE ProductID = @Id", new { Id = id });
        }

        public async Task<int> CreateAsync(Product product)
        {
            using var connection = GetConnection();
            var sql = @"INSERT INTO Products (ProductName, Description, Created)
                        VALUES (@ProductName, @Description, @Created);
                        SELECT CAST(SCOPE_IDENTITY() as int)";
            return await connection.QuerySingleAsync<int>(sql, product);
        }

        public async Task UpdateAsync(Product product)
        {
            using var connection = GetConnection();
            var sql = @"UPDATE Products SET ProductName = @ProductName, Description = @Description, Created = @Created
                        WHERE ProductID = @ProductID";
            await connection.ExecuteAsync(sql, product);
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = GetConnection();
            await connection.ExecuteAsync("DELETE FROM Products WHERE ProductID = @Id", new { Id = id });
        }

        public async Task<IEnumerable<Product>> SearchAsync(string searchTerm)
        {
            using var connection = GetConnection();
            var sql = @"SELECT * FROM Products
                        WHERE ProductName LIKE @Term OR Description LIKE @Term
                        ORDER BY Created DESC";
            return await connection.QueryAsync<Product>(sql, new { Term = $"%{searchTerm}%" });
        }
    }
}