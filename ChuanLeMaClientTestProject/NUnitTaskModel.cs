using AtomUI.Desktop.Controls;
using ChuanLeMaClient.Repository;
using System.Data;
using System.Threading.Tasks;

namespace ChuanLeMaClientTestProject;

public class NUnitTaskModel
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        SQLiteHelper liteHelper = new SQLiteHelper();
        DataTable dt = await liteHelper.QueryAsync("select * from TaskModel;", new Microsoft.Data.Sqlite.SqliteParameter[] { });
        int i = await liteHelper.InsertAsync(@"
                        insert into TaskModel
                        (
                         taskid, localpath, remotepath, filesize, status, completedsize
                        ) values
                              (
                               '1',
                               '2',
                               '3',
                               4,
                               ""已完成"",
                            4
                              )
                        ", null);
        // Assert.Pass(); 
    }
}
