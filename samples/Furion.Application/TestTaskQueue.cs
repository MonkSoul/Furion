using Furion.TaskQueue;

namespace Furion.Application;

public class TestTaskQueue : IDynamicApiController
{
    private readonly ITaskQueue _taskQueue;
    public TestTaskQueue(ITaskQueue taskQueue)
    {
        _taskQueue = taskQueue;
    }

    /// <summary>
    /// 同步入队
    /// </summary>
    public void SyncTask()
    {
        _taskQueue.Enqueue(provider =>
        {
            Console.WriteLine("我是同步的");
        });
    }

    /// <summary>
    /// 同步入队，延迟 3 秒触发
    /// </summary>
    public void SyncTask2()
    {
        _taskQueue.Enqueue(provider =>
        {
            Console.WriteLine("我是同步的，但我延迟了 3 秒");
        }, 3000);
    }

    /// <summary>
    /// 异步入队
    /// </summary>
    public async Task AsyncTask()
    {
        await _taskQueue.EnqueueAsync(async (provider, token) =>
        {
            Console.WriteLine("我是异步的");
            await ValueTask.CompletedTask;
        });
    }

    /// <summary>
    /// 异步入队
    /// </summary>
    public async Task AsyncTask2()
    {
        await _taskQueue.EnqueueAsync(async (_, _) =>
        {
            Console.WriteLine("我是异步的，但我延迟了 3 秒");
            await ValueTask.CompletedTask;
        }, 3000);
    }

    public void 测试异常()
    {
        _taskQueue.Enqueue(provider =>
        {
            throw new Exception("我出错了");
        });
    }
}