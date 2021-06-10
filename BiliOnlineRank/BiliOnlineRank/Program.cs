using Bili;
using Bili.Exceptions;
using Bili.Models;
using System;
using System.IO;
using System.Threading;

namespace BiliOnlineRank
{
    class Program
    {
        static void Main(string[] args)
        {
            string defaultPrefix = "http://localhost:8000/";
            // Init
            Console.WriteLine("-------- Info --------");
            string username, password, prefix;
            if (File.Exists("config.txt"))
            {
                string[] lines = File.ReadAllLines("config.txt");
                username = lines[0];
                password = lines[1];
                prefix = lines[2];
                Console.WriteLine($"username: <Hide>");
                Console.WriteLine($"password: <Hide>");
                Console.WriteLine($"prefix: {prefix}");
            }
            else
            {
                Console.Write("username: ");
                username = Console.ReadLine().Trim();
                Console.Write("password: ");
                password = Console.ReadLine().Trim();
                Console.Write($"service prefix ({defaultPrefix}): ");
                prefix = Console.ReadLine().Trim();

                File.WriteAllLines("config.txt", new string[] { username, password, prefix });
            }
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = defaultPrefix;
            }
            Console.WriteLine();

            // Login
            LoginInfo loginInfo = null;
            try
            {
                // Normal login
                Console.WriteLine("-------- Normal login --------");
                loginInfo = BiliLogin.Login(username, password);
                Console.WriteLine(loginInfo);
                Console.WriteLine();
            }
            catch (LoginFailedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (LoginStatusException ex)
            {
                Console.WriteLine(ex);
            }

            // Room Info
            Console.WriteLine("-------- Room Info --------");
            RoomInfo roomInfo = BiliLive.GetInfo(loginInfo.Token.AccessToken, loginInfo.Token.Mid.ToString());
            Console.WriteLine($"房间号: {roomInfo.RoomId}");
            Console.WriteLine($"用户ID: {roomInfo.Uid}");
            Console.WriteLine($"用户名: {roomInfo.Uname}");
            Console.WriteLine($"标题: {roomInfo.Title}");
            Console.WriteLine($"分区名称: {roomInfo.ParentName} - {roomInfo.AreaV2Name}");
            Console.WriteLine($"粉丝牌名称: {roomInfo.MedalName}");
            Console.WriteLine();

            // Service
            Console.WriteLine("-------- Service --------");
            ApiProvider apiProvider = new ApiProvider(prefix);
            apiProvider.Start();
            Console.WriteLine($"服务运行在: {prefix}");
            Console.WriteLine($"  /data: 获取数据");

            // Ranking list
            Console.WriteLine("-------- Ranking list --------");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("排名\t贡献值\t用户名");
                Console.WriteLine("-------- 高能榜 --------");
                AnchorOnlineGoldRank onlineGoldRank = BiliLive.GetAnchorOnlineGoldRank(loginInfo.Token.AccessToken, "1", "50", roomInfo.RoomId.ToString(), loginInfo.Token.Mid.ToString());
                apiProvider.GoldRank = onlineGoldRank;
                foreach (AnchorOnlineGoldRankItem item in onlineGoldRank.Items)
                {
                    Console.WriteLine($"{item.userRank}\t{item.Score}\t{item.Name}");
                }

                Console.WriteLine("-------- 在线用户 --------");
                OnlineRank onlineRank = BiliLive.GetOnlineRank(loginInfo.Token.AccessToken, "1", "50", roomInfo.RoomId.ToString(), loginInfo.Token.Mid.ToString());
                apiProvider.Rank = onlineRank;
                foreach (OnlineRankItem item in onlineRank.Items)
                {
                    Console.WriteLine($"-\t0\t{item.Name}");
                }
                Console.WriteLine($"在线人数: {onlineRank.OnlineNum}");
                Console.WriteLine();

                // Sleep for 20s
                for (int i = 20; i > 0; i--)
                {
                    Console.Write($"\r{i} ");
                    Thread.Sleep(1000);
                }
                Console.WriteLine($"\r  ");
            }

            apiProvider.Stop();
        }
    }
}
