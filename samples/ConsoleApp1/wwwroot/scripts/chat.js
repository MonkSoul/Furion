"use strict";

// 构建服务通讯连接对象
var connection = new signalR.HubConnectionBuilder()
    .withUrl("/hubs/chathub", {
        // 这里可以请求报文头
    })
    .withAutomaticReconnect()
    .build();

document.getElementById("send").disabled = true;

// 监听广播的消息
connection.on("OnBroadcast", function (user, message) {
    addMsg(user, message);
});

// 正式建立连接
connection.start().then(function () {
    document.getElementById("send").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// 发送广播消息
document.getElementById("send").addEventListener("click", function (event) {
    var user = document.getElementById("myname").value || "Furion";
    var message = document.getElementById("input-message").value;

    // 调用服务端广播方法
    connection.invoke("Broadcast", user, message).catch(function (err) {
        return console.error(err.toString());
    });

    document.getElementById("input-message").value = "";
    event.preventDefault();
});

// 重新连接监听
connection.onreconnecting(error => {
    console.assert(connection.state === signalR.HubConnectionState.Reconnecting);

    document.getElementById("input-message").disabled = true;

    addSysMsg("系统重新连接失败");
});

// 连接失败（建议刷新页面）
connection.onclose(error => {
    console.assert(connection.state === signalR.HubConnectionState.Disconnected);

    document.getElementById("input-message").disabled = true;

    addSysMsg("系统已端口连接，请刷新浏览器");
});

// 构建消息
function addMsg(user, message) {
    var yourName = document.getElementById("myname").value || "Furion";

    var isSender = yourName === user;

    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var pic = user.substr(0, 1);

    var div = document.createElement("div");
    div.classList.add("furion-messages-item");
    if (isSender == true) div.classList.add("send");

    div.innerHTML = `<div class="furion-messages-picture" style="${isSender ? "background:#ff006e;" : ""}">${pic}</div>
                    <div class="furion-message-content">
                        <div class="furion-messages-label">${user}</div>
                        <div class="furion-messages-msg">${msg}</div>
                    </div>`;

    document.getElementById("msgs").appendChild(div);
}

// 构建系统消息
function addSysMsg(message) {
    var div = document.createElement("div");
    div.classList.add("furion-system-msg");
    div.innerHTML = `<div>${message}</div>`;
    document.getElementById("msgs").appendChild(div);
}