using Bili;
using Bili.Models;
using BiliLogin;
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
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            string defaultPrefix = "http://localhost:7123/";
            string prefix = defaultPrefix;

            // User login
            MoblieLoginWindow moblieLoginWindow = new MoblieLoginWindow(null);
            moblieLoginWindow.LoggedIn += (MoblieLoginWindow sender, System.Net.CookieCollection cookies, uint uid) =>
            {
                BiliApi.Cookies.Add(cookies);
                moblieLoginWindow.Dispatcher.Invoke(() =>
                {
                    moblieLoginWindow.Close();
                });
            };
            moblieLoginWindow.Canceled += (MoblieLoginWindow sender) =>
            {
                Environment.Exit(1);
            };
            moblieLoginWindow.ShowDialog();

            // Room Info
            Console.WriteLine("-------- 房间信息 --------");
            RoomInfo roomInfo = BiliLive.GetRoomInfo();
            Console.WriteLine($"房间号: {roomInfo.RoomId}");
            Console.WriteLine($"用户ID: {roomInfo.Uid}");
            Console.WriteLine($"用户名: {roomInfo.Uname}");
            Console.WriteLine($"标题: {roomInfo.Title}");
            Console.WriteLine($"分区名称: {roomInfo.ParentName} - {roomInfo.AreaV2Name}");
            Console.WriteLine($"粉丝牌名称: {roomInfo.MedalName}");
            Console.WriteLine();

            //// Service
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
                AnchorOnlineGoldRank onlineGoldRank = BiliLive.GetAnchorOnlineGoldRank("1", "50", roomInfo.RoomId.ToString(), roomInfo.Uid.ToString());
                //apiProvider.GoldRank = onlineGoldRank;
                foreach (AnchorOnlineGoldRankItem item in onlineGoldRank.Items)
                {
                    Console.WriteLine($"{item.UserRank}\t{item.Score}\t{item.Name}");
                }

                Console.WriteLine("-------- 在线用户 --------");
                OnlineRank onlineRank = BiliLive.GetOnlineRank("1", "50", roomInfo.RoomId.ToString(), roomInfo.Uid.ToString());
                //apiProvider.Rank = onlineRank;
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
