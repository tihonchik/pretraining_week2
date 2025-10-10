using System.Data;
namespace task3

{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
