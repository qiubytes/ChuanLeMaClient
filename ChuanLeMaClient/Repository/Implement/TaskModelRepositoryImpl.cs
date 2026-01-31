using ChuanLeMaClient.Models;
using ChuanLeMaClient.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChuanLeMaClient.Repository.Implement
{
    public class TaskModelRepositoryImpl : ITaskModelRepository
    {
        private readonly SQLiteHelper _sqliteHelper;
        public TaskModelRepositoryImpl(SQLiteHelper liteHelper)
        {
            _sqliteHelper = liteHelper;
        }

        public async Task<int> DeleteTaskModelAsync(string taskId)
        {
            string sql = "Delete  FROM TaskModel WHERE TaskId = @TaskId";
            Microsoft.Data.Sqlite.SqliteParameter[] parameters = new Microsoft.Data.Sqlite.SqliteParameter[]
            {
                new Microsoft.Data.Sqlite.SqliteParameter("@TaskId", taskId)
            };
            return await _sqliteHelper.ExecuteNonQueryAsync(sql, parameters);
        }

        /// <summary>
        /// 查询所有任务
        /// </summary>
        /// <returns></returns>
        public async Task<List<TaskModel>> GetAllTaskModelsAsync()
        {
            string sql = "SELECT * FROM TaskModel";
            DataTable dr = await _sqliteHelper.QueryAsync(sql, new Microsoft.Data.Sqlite.SqliteParameter[] { });
            List<TaskModel> list = new List<TaskModel>();
            foreach (DataRow row in dr.Rows)
            {
                TaskModel model = new TaskModel
                {
                    TaskId = (row["TaskId"]).ToString(),
                    LocalPath = row["LocalPath"].ToString(),
                    RemotePath = row["RemotePath"].ToString(),
                    FileSize = Convert.ToInt64(row["FileSize"]),
                    Status = row["Status"].ToString(),
                    CompletedSize = Convert.ToInt64(row["CompletedSize"]),
                    Direction = row["Direction"].ToString()
                };
                list.Add(model);
            }
            return list;
        }

        public async Task<TaskModel> GetModel(string TaskId)
        {
            string sql = "SELECT * FROM TaskModel WHERE TaskId = @TaskId";
            Microsoft.Data.Sqlite.SqliteParameter[] parameters = new Microsoft.Data.Sqlite.SqliteParameter[]
            {
                new Microsoft.Data.Sqlite.SqliteParameter("@TaskId", TaskId)
            };
            DataTable task = await _sqliteHelper.QueryAsync(sql, new Microsoft.Data.Sqlite.SqliteParameter[] {
             new Microsoft.Data.Sqlite.SqliteParameter("@TaskId", TaskId)
            });
            if (task.Rows.Count > 0)
            {
                DataRow row = task.Rows[0];
                TaskModel model = new TaskModel
                {
                    TaskId = (row["TaskId"]).ToString(),
                    LocalPath = row["LocalPath"].ToString(),
                    RemotePath = row["RemotePath"].ToString(),
                    FileSize = Convert.ToInt64(row["FileSize"]),
                    Status = row["Status"].ToString(),
                    CompletedSize = Convert.ToInt64(row["CompletedSize"]),
                    Direction = row["Direction"].ToString()
                };
                return model;
            }
            return null;
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<int> InsertTaskModelAsync(TaskModel model)
        {
            string sql = "INSERT INTO TaskModel (TaskId, LocalPath, RemotePath, FileSize, Status, CompletedSize, Direction) " +
               "VALUES (@TaskId, @LocalPath, @RemotePath, @FileSize, @Status, @CompletedSize, @Direction)";
            Microsoft.Data.Sqlite.SqliteParameter[] parameters = new Microsoft.Data.Sqlite.SqliteParameter[]
            {
                new Microsoft.Data.Sqlite.SqliteParameter("@TaskId", model.TaskId),
                new Microsoft.Data.Sqlite.SqliteParameter("@LocalPath", model.LocalPath),
                new Microsoft.Data.Sqlite.SqliteParameter("@RemotePath", model.RemotePath),
                new Microsoft.Data.Sqlite.SqliteParameter("@FileSize", model.FileSize),
                new Microsoft.Data.Sqlite.SqliteParameter("@Status", model.Status),
                new Microsoft.Data.Sqlite.SqliteParameter("@CompletedSize", model.CompletedSize),
                new Microsoft.Data.Sqlite.SqliteParameter("@Direction", model.Direction)
            };
            return await _sqliteHelper.ExecuteNonQueryAsync(sql, parameters);
        }

        public Task<int> UpdateTaskModelAsync(TaskModel model)
        {
            string sql = "UPDATE TaskModel SET LocalPath = @LocalPath, RemotePath = @RemotePath, FileSize = @FileSize, " +
              "Status = @Status, CompletedSize = @CompletedSize, Direction = @Direction WHERE TaskId = @TaskId";
            Microsoft.Data.Sqlite.SqliteParameter[] parameters = new Microsoft.Data.Sqlite.SqliteParameter[]
            {
                new Microsoft.Data.Sqlite.SqliteParameter("@LocalPath", model.LocalPath),
                new Microsoft.Data.Sqlite.SqliteParameter("@RemotePath", model.RemotePath),
                new Microsoft.Data.Sqlite.SqliteParameter("@FileSize", model.FileSize),
                new Microsoft.Data.Sqlite.SqliteParameter("@Status", model.Status),
                new Microsoft.Data.Sqlite.SqliteParameter("@CompletedSize", model.CompletedSize),
                new Microsoft.Data.Sqlite.SqliteParameter("@Direction", model.Direction),
                new Microsoft.Data.Sqlite.SqliteParameter("@TaskId", model.TaskId)
            };
            return _sqliteHelper.ExecuteNonQueryAsync(sql, parameters);
        }
    }
}
