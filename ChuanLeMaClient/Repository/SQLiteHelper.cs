using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Repository
{
    public class SQLiteHelper
    {

        private readonly string _connectionString;
        public SQLiteHelper()
        {
            _connectionString = $"Data Source=DB/db.db;";
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dbPath">数据库文件路径</param>
        public SQLiteHelper(string dbPath)
        {
            if (string.IsNullOrWhiteSpace(dbPath))
                throw new ArgumentException("数据库路径不能为空", nameof(dbPath));

            // 确保目录存在
            var directory = Path.GetDirectoryName(dbPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            _connectionString = $"Data Source={dbPath};";
        }
        /// <summary>
        /// 创建参数
        /// </summary>
        public static SqliteParameter CreateParameter(string name, object? value)
        {
            return new SqliteParameter(name, value ?? DBNull.Value);
        }

        /// <summary>
        /// 查询数据并返回 DataTable（异步）
        /// </summary>
        public async Task<DataTable> QueryAsync(string sql, params SqliteParameter[] parameters)
        {

            await using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            await using var reader = await command.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            return dataTable;
        }


        /// <summary>
        /// 查询单个值（异步）
        /// </summary>
        public async Task<T?> QueryScalarAsync<T>(string sql, params SqliteParameter[] parameters)
        {
            var result = await ExecuteScalarAsync(sql, parameters);
            return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// 执行非查询 SQL
        /// </summary>
        public int ExecuteNonQuery(string sql, params SqliteParameter[] parameters)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行非查询 SQL（异步）
        /// </summary>
        public async Task<int> ExecuteNonQueryAsync(string sql, params SqliteParameter[] parameters)
        {
            await using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// 执行标量查询
        /// </summary>
        public object? ExecuteScalar(string sql, params SqliteParameter[] parameters)
        {
            using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return command.ExecuteScalar();
        }

        /// <summary>
        /// 执行标量查询（异步）
        /// </summary>
        public async Task<object?> ExecuteScalarAsync(string sql, params SqliteParameter[] parameters)
        {
            await using SqliteConnection connection = new SqliteConnection(_connectionString);
            connection.Open();
            await using var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }
            return await command.ExecuteScalarAsync();
        }

    }
}
