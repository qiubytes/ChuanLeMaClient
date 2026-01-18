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
    public class SQLiteHelper : IDisposable
    {
        private SqliteConnection? _connection;
        private readonly string _connectionString;
        private bool _disposed;

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
        /// 获取数据库连接（自动打开）
        /// </summary>
        private SqliteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(_connectionString);
            }

            if (_connection.State != ConnectionState.Open)
            {
                _connection.Open();
            }

            return _connection;
        }

        /// <summary>
        /// 创建参数
        /// </summary>
        public static SqliteParameter CreateParameter(string name, object? value)
        {
            return new SqliteParameter(name, value ?? DBNull.Value);
        }

        #region 增 - 插入数据

        /// <summary>
        /// 执行插入操作
        /// </summary>
        public int Insert(string sql, params SqliteParameter[] parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 执行插入操作（异步）
        /// </summary>
        public async Task<int> InsertAsync(string sql, params SqliteParameter[] parameters)
        {
            return await ExecuteNonQueryAsync(sql, parameters);
        }

        #endregion

        #region 删 - 删除数据

        /// <summary>
        /// 执行删除操作
        /// </summary>
        public int Delete(string sql, params SqliteParameter[] parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 执行删除操作（异步）
        /// </summary>
        public async Task<int> DeleteAsync(string sql, params SqliteParameter[] parameters)
        {
            return await ExecuteNonQueryAsync(sql, parameters);
        }

        #endregion

        #region 改 - 更新数据

        /// <summary>
        /// 执行更新操作
        /// </summary>
        public int Update(string sql, params SqliteParameter[] parameters)
        {
            return ExecuteNonQuery(sql, parameters);
        }

        /// <summary>
        /// 执行更新操作（异步）
        /// </summary>
        public async Task<int> UpdateAsync(string sql, params SqliteParameter[] parameters)
        {
            return await ExecuteNonQueryAsync(sql, parameters);
        }

        #endregion

        #region 查 - 查询数据

       

        /// <summary>
        /// 查询数据并返回 DataTable（异步）
        /// </summary>
        public async Task<DataTable> QueryAsync(string sql, params SqliteParameter[] parameters)
        {
            await using var connection = GetConnection();
            await using var command = CreateCommand(connection, sql, parameters);
            await using var reader = await command.ExecuteReaderAsync();

            var dataTable = new DataTable();
            dataTable.Load(reader);

            return dataTable;
        } 
        /// <summary>
        /// 查询单行数据（异步）
        /// </summary>
        public async Task<DataRow?> QuerySingleAsync(string sql, params SqliteParameter[] parameters)
        {
            var dataTable = await QueryAsync(sql, parameters);
            return dataTable.Rows.Count > 0 ? dataTable.Rows[0] : null;
        }

        /// <summary>
        /// 查询单个值
        /// </summary>
        public T? QueryScalar<T>(string sql, params SqliteParameter[] parameters)
        {
            var result = ExecuteScalar(sql, parameters);
            return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
        }

        /// <summary>
        /// 查询单个值（异步）
        /// </summary>
        public async Task<T?> QueryScalarAsync<T>(string sql, params SqliteParameter[] parameters)
        {
            var result = await ExecuteScalarAsync(sql, parameters);
            return result == null || result == DBNull.Value ? default : (T)Convert.ChangeType(result, typeof(T));
        }

        #endregion

        #region 基础执行方法（内部使用）

        /// <summary>
        /// 执行非查询 SQL
        /// </summary>
        private int ExecuteNonQuery(string sql, params SqliteParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteNonQuery();
        }

        /// <summary>
        /// 执行非查询 SQL（异步）
        /// </summary>
        private async Task<int> ExecuteNonQueryAsync(string sql, params SqliteParameter[] parameters)
        {
            await using var connection = GetConnection();
            await using var command = CreateCommand(connection, sql, parameters);
            return await command.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// 执行标量查询
        /// </summary>
        private object? ExecuteScalar(string sql, params SqliteParameter[] parameters)
        {
            using var connection = GetConnection();
            using var command = CreateCommand(connection, sql, parameters);
            return command.ExecuteScalar();
        }

        /// <summary>
        /// 执行标量查询（异步）
        /// </summary>
        private async Task<object?> ExecuteScalarAsync(string sql, params SqliteParameter[] parameters)
        {
            await using var connection = GetConnection();
            await using var command = CreateCommand(connection, sql, parameters);
            return await command.ExecuteScalarAsync();
        }

        /// <summary>
        /// 创建 SqliteCommand
        /// </summary>
        private SqliteCommand CreateCommand(SqliteConnection connection, string sql, params SqliteParameter[] parameters)
        {
            var command = connection.CreateCommand();
            command.CommandText = sql;

            if (parameters != null && parameters.Length > 0)
            {
                command.Parameters.AddRange(parameters);
            }

            return command;
        }

        #endregion
  
        #region IDisposable 实现

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _connection?.Close();
                    _connection?.Dispose();
                }
                _disposed = true;
            }
        }

        #endregion
    }
}
