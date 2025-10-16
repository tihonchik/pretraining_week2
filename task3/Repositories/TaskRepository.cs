using System.Data;
using Dapper;

namespace task3;

public class TaskRepository : ITaskRepository
{
    private readonly IDbConnectionFactory _factory;

    public TaskRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }
    public async Task<bool> DeleteAsync(int Id)
    {
        using IDbConnection connection = _factory.CreateConnection();
        string sqlExecute = $"DELETE FROM Tasks WHERE Id = @Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("Id", Id);

        int affectedRows = await connection.ExecuteAsync(sqlExecute, parameters);
        return affectedRows > 0;
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync()
    {
        using IDbConnection connection = _factory.CreateConnection();
        string sqlQuery = $"SELECT * FROM Tasks";

        IEnumerable<TaskModel> tasks = await connection.QueryAsync<TaskModel>(sqlQuery);
        return tasks;
    }

    public async Task<TaskModel?> GetByIdAsync(int Id)
    {
        using IDbConnection connection = _factory.CreateConnection();
        string sqlQuery = $"SELECT * FROM Tasks WHERE Id = @Id";

        DynamicParameters parameters = new DynamicParameters();
        parameters.Add("Id", Id);

        TaskModel? task = await connection.QueryFirstOrDefaultAsync<TaskModel>(sqlQuery, parameters);
        return task;
    }

    public async Task<int> InsertAsync(TaskModel task)
    {
        using IDbConnection connection = _factory.CreateConnection();
        string sqlQuery = "INSERT INTO Tasks (Title, Description, IsCompleted, CreatedAt) OUTPUT INSERTED.Id VALUES (@Title, @Description, @IsCompleted, @CreatedAt)";

        int id = await connection.QuerySingleAsync<int>(sqlQuery, task);
        return id;
    }

    public async Task<bool> UpdateAsync(TaskModel task)
    {
        using IDbConnection connection = _factory.CreateConnection();
        string sqlQuery = "UPDATE Tasks SET IsCompleted = @IsCompleted WHERE Id = @Id";

        task.CreatedAt = DateTime.UtcNow;
        int affectedRows = await connection.ExecuteAsync(sqlQuery, task);
        return affectedRows > 0;
    }
}