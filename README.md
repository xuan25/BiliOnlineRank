# BiliOnlineRank

用于获取直播在线人数和高能榜。~~账号登录组件见 [xuan525/BiliPassport](https://github.com/xuan525/BiliPassport)~~ 由于b站频繁更新登录接口，本项目将不再包含登录组件，请自行获取账户的 访问令牌 (Access token)。

包含使用样例`Program.cs`，输出如下（已做匿名化处理）：

```
-------- Room Info --------
房间号: <hide>
用户ID: <hide>
用户名: <hide>
标题: 测试接口中
分区名称: 学习 - 陪伴学习
粉丝牌名称:

-------- Ranking List --------

排名    贡献值  用户名
-------- 高能榜 --------
1       50      <hide>
-------- 在线用户 --------
-        0      <hide>
在线人数: 2


排名    贡献值  用户名
-------- 高能榜 --------
1       50      <hide>
-------- 在线用户 --------
-        0      <hide>
在线人数: 2

```

## 本地服务

支持通过本地 API 访问数据，默认地址为 `http://localhost:8000`

### /data

路径 `/data` 接收 `GET` 请求，无参数，返回 `json` 数据，示例如下：

```
{
    "number": 2,
    "gold_rank": [
        {
            "uid": <hide>,
            "uname": "<hide>",
            "face": "http://i2.hdslb.com/bfs/face/<hide>.jpg",
            "rank": 1,
            "score": 50
        }
    ],
    "online_rank": [
        {
            "uid": <hide>,
            "uname": "<hide>",
            "face": "http://i2.hdslb.com/bfs/face/<hide>.jpg",
        }
    ]
}
```
