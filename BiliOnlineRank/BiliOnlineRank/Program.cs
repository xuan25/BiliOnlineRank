using Bili;
using Bili.Models;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace BiliOnlineRank
{
    class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            string defaultPrefix = "http://localhost:8000/";

            // User config
            string accessToken;
            string prefix;
            if (File.Exists("config.txt"))
            {
                // Has login info
                string[] lines = File.ReadAllLines("config.txt");
                accessToken = lines.Length > 0 ? lines[0] : string.Empty;
                prefix = lines.Length > 1 ? lines[1] : string.Empty;
                Console.WriteLine($"访问令牌 (Access Token): <隐藏>");
                Console.WriteLine($"本地服务前缀: {prefix}");
            }
            else
            {
                // Require login info
                Console.WriteLine("*注: 配置信息将会自动保存到 config.txt");
                Console.Write("访问令牌 (Access Token): ");
                accessToken = Console.ReadLine().Trim();
                Console.Write($"本地服务前缀 (默认为 {defaultPrefix}): ");
                prefix = Console.ReadLine().Trim();

                File.WriteAllLines("config.txt", new string[] { accessToken, prefix });
            }

            // validate inputs
            if (string.IsNullOrEmpty(accessToken))
            {
                Console.Error.WriteLine("访问令牌 (Access Token) 不得为空，登录中断。");
                Console.Write("按任意键退出...");
                Console.ReadKey();
                return;
            }
            if (string.IsNullOrEmpty(prefix))
            {
                prefix = defaultPrefix;
            }

            // Room Info
            Console.WriteLine("-------- 房间信息 --------");
            string mid = BiliLive.GetMid(accessToken).ToString();
            RoomInfo roomInfo = BiliLive.GetRoomInfo(accessToken, mid);
            Console.WriteLine($"房间号: {roomInfo.RoomId}");
            Console.WriteLine($"用户ID: {roomInfo.Uid}");
            Console.WriteLine($"用户名: {roomInfo.Uname}");
            Console.WriteLine($"标题: {roomInfo.Title}");
            Console.WriteLine($"分区名称: {roomInfo.ParentName} - {roomInfo.AreaV2Name}");
            Console.WriteLine($"粉丝牌名称: {roomInfo.MedalName}");
            Console.WriteLine();

            // Service
            Console.WriteLine("-------- 本地服务 --------");
            ApiProvider apiProvider = new ApiProvider(prefix);
            apiProvider.Start();
            Console.WriteLine($"服务运行在: {prefix}");
            Console.WriteLine($"  /data: 获取数据 ({prefix}data)");

            // Ranking list
            Console.WriteLine("-------- 排行榜 --------");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("排名\t贡献值\t用户名");
                Console.WriteLine("-------- 高能榜 --------");
                AnchorOnlineGoldRank onlineGoldRank = BiliLive.GetAnchorOnlineGoldRank(accessToken, "1", "50", roomInfo.RoomId.ToString(), mid);
                apiProvider.GoldRank = onlineGoldRank;
                foreach (AnchorOnlineGoldRankItem item in onlineGoldRank.Items)
                {
                    Console.WriteLine($"{item.UserRank}\t{item.Score}\t{item.Name}");
                }

                Console.WriteLine("-------- 在线用户 --------");
                OnlineRank onlineRank = BiliLive.GetOnlineRank(accessToken, "1", "50", roomInfo.RoomId.ToString(), mid);
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
