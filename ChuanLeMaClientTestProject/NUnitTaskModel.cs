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
        Assert.Pass();
    }
}
