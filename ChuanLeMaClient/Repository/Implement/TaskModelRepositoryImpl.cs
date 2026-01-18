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
            return await _sqliteHelper.InsertAsync(sql, parameters);
        }
    }
}
