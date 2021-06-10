using Bili.Models;
using JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BiliOnlineRank
{
    public class ApiProvider
    {
        HttpListener httpListener;
        Thread loopThread;
        public AnchorOnlineGoldRank GoldRank { get; set; }
        public OnlineRank Rank { get; set; }

        public ApiProvider(string prefix)
        {
            httpListener = new HttpListener();
            httpListener.Prefixes.Add(prefix);
        }

        public void Start()
        {
            httpListener.Start();

            loopThread = new Thread(ServerLoop);
            loopThread.Start();
        }

        public void Stop()
        {
            loopThread.Abort();
            httpListener.Stop();
        }

        public void ServerLoop()
        {
            while (httpListener.IsListening)
            {
                HttpListenerContext ctx = httpListener.GetContext();
                Task task = new Task(() => HandleContext(ctx));
                task.Start();
            }
        }

        private void HandleContext(HttpListenerContext ctx)
        {
            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse res = ctx.Response;

            switch (req.HttpMethod)
            {
                case "GET":
                    switch (req.Url.AbsolutePath)
                    {
                        case "/":
                            IndexController(req, res);
                            break;
                        case "/data":
                            DataController(req, res);
                            break;
                        default:
                            res.StatusCode = 404;
                            res.Close();
                            break;
                    }
                    break;
                default:
                    res.StatusCode = 500;
                    res.Close();
                    break;
            }
        }

        private void IndexController(HttpListenerRequest req, HttpListenerResponse res)
        {
            byte[] buffer = Encoding.UTF8.GetBytes("Listening...");
            res.ContentType = "text/plain";
            res.ContentEncoding = Encoding.UTF8;
            res.ContentLength64 = buffer.LongLength;

            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }

        private void DataController(HttpListenerRequest req, HttpListenerResponse res)
        {
            Json.Value.Array goldRank = new Json.Value.Array();
            if (GoldRank != null)
            {
                foreach (AnchorOnlineGoldRankItem item in GoldRank.Items)
                {
                    Json.Value.Object goldRankItem = new Json.Value.Object()
                    {
                        { "uid", item.Uid },
                        { "uname", item.Name },
                        { "face", item.Face },
                        { "rank", item.userRank },
                        { "score", item.Score },
                    };
                    goldRank.Add(goldRankItem);
                }
            }

            Json.Value.Array onlineRank = new Json.Value.Array();
            if (Rank != null)
            {
                foreach (OnlineRankItem item in Rank.Items)
                {
                    Json.Value.Object rankItem = new Json.Value.Object()
                    {
                        { "uid", item.Uid },
                        { "uname", item.Name },
                        { "face", item.Face },
                    };
                    onlineRank.Add(rankItem);
                }
            }

            Json.Value.Object resJson = new Json.Value.Object()
            {
                { "number", Rank.OnlineNum },
                { "gold_rank", goldRank },
                { "online_rank", onlineRank },
            };

            byte[] buffer = Encoding.UTF8.GetBytes(resJson.ToString());
            res.ContentType = "application/json";
            res.ContentEncoding = Encoding.UTF8;
            res.ContentLength64 = buffer.LongLength;

            res.OutputStream.Write(buffer, 0, buffer.Length);
            res.Close();
        }
    }
}
