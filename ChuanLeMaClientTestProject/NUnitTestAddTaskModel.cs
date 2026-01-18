using ChuanLeMaClient.Repository.Implement;

namespace ChuanLeMaClientTestProject;

public class NUnitTestAddTaskModel
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task Test1()
    {
        TaskModelRepositoryImpl taskModelRepositoryImpl = new TaskModelRepositoryImpl(new ChuanLeMaClient.Repository.SQLiteHelper());
        var model = new ChuanLeMaClient.Models.TaskModel()
        {
            TaskId = Guid.NewGuid().ToString(),
            CompletedSize = 0,
            FileSize = 100,
            RemotePath = "http://example.com/file.zip",
            LocalPath = "C:\\Downloads\\file.zip",
            Direction = "下载",
            Status = "进行中"
        };
        var type = model.GetType();
        // 获取生成的属性
        var property = type.GetProperty("CompletedSize");
        if (property == null)
            Assert.Fail("Generated property not found");

        // 设置属性值
        property.SetValue(model, 10);
        await taskModelRepositoryImpl.InsertTaskModelAsync(model);
        //Assert.Pass();
    }
}
