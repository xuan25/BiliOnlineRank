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
            // Init
            Console.WriteLine("-------- Info --------");
            string username, password;
            if (File.Exists("login.txt"))
            {
                string[] lines = File.ReadAllLines("login.txt");
                username = lines[0];
                password = lines[1];
                Console.WriteLine($"username: {username}");
                Console.WriteLine($"password: {password}");
            }
            else
            {
                Console.Write("username: ");
                username = Console.ReadLine().Trim();
                Console.Write("password: ");
                password = Console.ReadLine().Trim();
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

            // Ranking list
            Console.WriteLine("-------- Ranking list --------");
            Console.WriteLine();
            while (true)
            {
                Console.WriteLine("排名\t贡献值\t用户名");
                Console.WriteLine("-------- 高能榜 --------");
                AnchorOnlineGoldRank onlineGoldRank = BiliLive.GetAnchorOnlineGoldRank(loginInfo.Token.AccessToken, "1", "50", roomInfo.RoomId.ToString(), loginInfo.Token.Mid.ToString());
                foreach (AnchorOnlineGoldRankItem item in onlineGoldRank.Items)
                {
                    Console.WriteLine($"{item.userRank}\t{item.Score}\t{item.Name}");
                }

                Console.WriteLine("-------- 在线用户 --------");
                OnlineRank onlineRank = BiliLive.GetOnlineRank(loginInfo.Token.AccessToken, "1", "50", roomInfo.RoomId.ToString(), loginInfo.Token.Mid.ToString());
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
        }
    }
}
