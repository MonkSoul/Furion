using Furion.TaskScheduler;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Furion
{
    /// <summary>
    /// 监听主机启动停止
    /// </summary>
    internal class AppHostedService : IHostedService
    {
        /// <summary>
        /// 主机启动监听
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 主机停止监听
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            // 清除所有任务
            SpareTime.CancelAll();

            return Task.CompletedTask;
        }
    }
}