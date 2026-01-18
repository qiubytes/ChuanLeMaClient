using ChuanLeMaClient.Repository.Implement;

namespace ChuanLeMaClientTestProject;

public class NUnitTestGetAllTask
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        TaskModelRepositoryImpl taskModelRepositoryImpl = new TaskModelRepositoryImpl(new ChuanLeMaClient.Repository.SQLiteHelper());
        var q = await taskModelRepositoryImpl.GetAllTaskModelsAsync();
        //Assert.Pass();
    }
}
