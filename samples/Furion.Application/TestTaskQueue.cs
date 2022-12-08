using Furion.TaskQueue;

namespace Furion.Application;

public class TestTaskQueue : IDynamicApiController
{
    private readonly ITaskQueue _taskQueue;
    public TestTaskQueue(ITaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    public void 测试同步入队()
    {
        _taskQueue.Enqueue(() =>
        {
            Console.WriteLine("我是同步的");
        });

        _taskQueue.Enqueue(() =>
        {
            Console.WriteLine("我是同步延迟3秒");
        }, 3000);
    }

    public async Task 测试异步入队()
    {
        await _taskQueue.EnqueueAsync(async (token) =>
        {
            Console.WriteLine("我是异步的");
            await ValueTask.CompletedTask;
        });

        await _taskQueue.EnqueueAsync(async (token) =>
        {
            Console.WriteLine("我是异步的延迟3秒");
            await ValueTask.CompletedTask;
        }, 3000);
    }
}