using Furion.InstantMessaging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Furion.Core;

/// <summary>
/// 聊天集线器
/// </summary>
[MapHub("/hubs/chathub")]
public class ChatHub : Hub
{
    /// <summary>
    /// 广播消息，所有人都会收到
    /// </summary>
    /// <param name="user"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task Broadcast(string user, string message)
    {
        await Clients.All.SendAsync("OnBroadcast", user, message);
    }

    /// <summary>
    /// 用户加入连接时触发方法
    /// </summary>
    /// <returns></returns>
    public override Task OnConnectedAsync()
    {
        return base.OnConnectedAsync();
    }

    /// <summary>
    /// 用户端口断开连接时触发方法
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override Task OnDisconnectedAsync(Exception exception)
    {
        return base.OnDisconnectedAsync(exception);
    }
}
