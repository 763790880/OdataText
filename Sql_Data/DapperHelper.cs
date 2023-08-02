using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;

public class DapperHelper
{
    private string connectionString;

    public DapperHelper(string connectionString="")
    {
        this.connectionString = connectionString;
    }

    public T Get<T>(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.QueryFirstOrDefault<T>(query, parameters);
        }
    }

    public IEnumerable<T> GetAll<T>(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Query<T>(query, parameters);
        }
    }

    public int Execute(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Execute(query, parameters);
        }
    }

    public T Insert<T>(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.ExecuteScalar<T>(query, parameters);
        }
    }

    public int Update(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Execute(query, parameters);
        }
    }

    public int Delete(string query, object parameters = null)
    {
        using (IDbConnection connection = new SqlConnection(connectionString))
        {
            return connection.Execute(query, parameters);
        }
    }
}
