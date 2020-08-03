using Fur.AppCore.Attributes;
using System;

namespace Fur.Unmanaged
{
    /// <summary>
    /// 非托管资源释放类
    /// </summary>
    [NonInflated]
    public class UnmanagedDispose : IDisposable
    {
        /// <summary>
        /// 释放标记
        /// </summary>
        protected bool disposed;

        /// <summary>
        /// 析构函数，为了防止忘记显式的调用Dispose方法
        /// </summary>
        ~UnmanagedDispose() => Dispose(false);

        /// <summary>
        /// 手动释放
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // 通知垃圾回收器不再调用终结器
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 手动关闭
        /// </summary>
        public void Close() => Dispose();

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;

            if (disposing)
            {
                // 清理托管资源
            }

            // 清理非托管资源
            { }

            // 通知已释放
            disposed = true;
        }
    }
}